using QuickUnity.Events;
using QuickUnity.Tasks;
using QuickUnity.Utilitys;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuickUnity.Net.Http
{
    /// <summary>
    /// Data format enum of url loader.
    /// </summary>
    public enum URLLoaderDataFormat
    {
        Text,
        Texture,
        TextureNonreadable,
        Movie,
        Binary,
        AssetBundle,
        AudioClip
    }

    /// <summary>
    /// The URLLoader class downloads data from a URL as text, binary data, or URL-encoded variables.
    /// </summary>
    public class URLLoader : EventDispatcher
    {
        /// <summary>
        /// The data format binding properties.
        /// </summary>
        private static Dictionary<URLLoaderDataFormat, string> mDataFormatBindingProperties = new Dictionary<URLLoaderDataFormat, string>()
        {
            { URLLoaderDataFormat.Text, "text" },
            { URLLoaderDataFormat.Texture, "texture" },
            { URLLoaderDataFormat.TextureNonreadable, "textureNonReadable" },
            { URLLoaderDataFormat.Movie, "movie" },
            { URLLoaderDataFormat.Binary, "bytes" },
            { URLLoaderDataFormat.AssetBundle, "assetBundle" },
            { URLLoaderDataFormat.AudioClip, "audioClip" }
        };

        /// <summary>
        /// A URLRequest object specifying the URL to download.
        /// </summary>
        private URLRequest mRequest;

        /// <summary>
        /// The request task.
        /// </summary>
        private ITask mRequestTask;

        /// <summary>
        /// Indicates the number of bytes that have been loaded thus far during the load operation.
        /// </summary>
        private int mBytesLoaded = 0;

        /// <summary>
        /// Gets or sets the bytes loaded.
        /// </summary>
        /// <value>The bytes loaded.</value>
        public int bytesLoaded
        {
            get { return mBytesLoaded; }
        }

        /// <summary>
        /// Indicates the total number of bytes in the downloaded data.
        /// </summary>
        private int mBytesTotal = 0;

        /// <summary>
        /// Gets the total number of bytes in the downloaded data.
        /// </summary>
        /// <value>
        /// The bytes total.
        /// </value>
        public int bytesTotal
        {
            get { return mBytesTotal; }
        }

        /// <summary>
        /// The data received from the load operation.
        /// </summary>
        private object mData;

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public object data
        {
            get { return mData; }
            set { mData = value; }
        }

        /// <summary>
        /// Controls whether the downloaded data is received as text or other type.
        /// </summary>
        private URLLoaderDataFormat mDataFormat = URLLoaderDataFormat.Text;

        /// <summary>
        /// Gets or sets the data format.
        /// </summary>
        /// <value>The data format.</value>
        public URLLoaderDataFormat dataFormat
        {
            get { return mDataFormat; }
            set { mDataFormat = value; }
        }

        /// <summary>
        /// Parses the HTTP status code.
        /// </summary>
        /// <param name="responseHeaders">The response headers.</param>
        /// <returns></returns>
        public static int ParseHttpStatusCode(Dictionary<string, string> responseHeaders)
        {
            string statusStr = responseHeaders["STATUS"];
            string[] statusStrArr = statusStr.Split(' ');

            if (statusStrArr.Length > 1)
                return int.Parse(statusStrArr[1]);

            return 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="URLLoader"/> class.
        /// </summary>
        /// <param name="request">The request.</param>
        public URLLoader(URLRequest request = null)
        {
            if (request != null)
                Load(request);
        }

        /// <summary>
        /// Closes the load operation in progress.
        /// </summary>
        public void Close()
        {
            if (mRequestTask != null)
            {
                mRequestTask.Stop();
                TaskManager.instance.RemoveTask(mRequestTask);
                mRequestTask = null;
            }
        }

        /// <summary>
        /// Loads the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        public void Load(URLRequest request)
        {
            mRequest = request;

            if (mRequest == null || string.IsNullOrEmpty(mRequest.url))
                return;

            mRequestTask = new Task(Request());
            TaskManager.instance.AddTask("URLLoader.Request: " + request.url, mRequestTask);
            mRequestTask.Start();
        }

        /// <summary>
        /// Request to server.
        /// </summary>
        /// <returns>IEnumerator.</returns>
        private IEnumerator Request()
        {
            WWW www = null;
            WWWForm form = null;
            string url = mRequest.url;

            if (mRequest.data is byte[])
            {
                // Post data.
                mRequest.method = URLRequestMethod.Post;
            }
            else if (mRequest.data is URLVariables)
            {
                // If transform data by get method, then add variables to the end of url.
                if (mRequest.method == URLRequestMethod.Get)
                {
                    url += ((URLVariables)mRequest.data).ToString();
                }
                else if (mRequest.method == URLRequestMethod.Post)
                {
                    form = new WWWForm();
                    URLVariables variables = (URLVariables)mRequest.data;

                    foreach (KeyValuePair<string, string> kvp in variables.variables)
                    {
                        form.AddField(kvp.Key, kvp.Value);
                    }
                }
            }

            if (form != null)
            {
                www = new WWW(url, form);
            }
            else
            {
                if (mRequest.method == URLRequestMethod.Post)
                {
                    www = (mRequest.requestHeaders != null) ?
                        new WWW(url, (byte[])mRequest.data, mRequest.requestHeaders) :
                        new WWW(url, (byte[])mRequest.data);
                }
                else if (mRequest.method == URLRequestMethod.Get)
                {
                    www = new WWW(url);
                }
            }

            DispatchEvent(new HTTPEvent(HTTPEvent.OPEN, this, mRequest.url, mRequest.data));

            yield return www;

            // Remove task from TaskManager.
            if (mRequestTask != null)
                TaskManager.instance.RemoveTask(mRequestTask);

            if (www.responseHeaders.Count > 0)
            {
                int statusCode = ParseHttpStatusCode(www.responseHeaders);
                DispatchEvent(new HTTPStatusEvent(HTTPStatusEvent.HTTP_STATUS, this, statusCode));
            }

            // Handle response data from server.
            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.LogWarning("Http request error: " + www.error);
                DispatchEvent(new HTTPEvent(HTTPEvent.ERROR, this, mRequest.url, mRequest.data, www.error));
                yield return null;
            }
            else
            {
                HTTPEvent httpEvent = null;
                mBytesLoaded = www.bytesDownloaded;
                mBytesTotal = www.size;

                if (!www.isDone)
                {
                    // In progress of downloading.
                    httpEvent = new HTTPEvent(HTTPEvent.PROGRESS, this, mRequest.url);
                    httpEvent.progress = www.progress;
                    DispatchEvent(httpEvent);
                }
                else
                {
                    // Dispatch event HttpStatusEvent.HTTP_STATUS.
                    if (www.responseHeaders.Count > 0)
                    {
                        int statusCode = ParseHttpStatusCode(www.responseHeaders);

                        if (statusCode > 0)
                        {
                            HTTPStatusEvent httpStatusEvent = new HTTPStatusEvent(HTTPStatusEvent.HTTP_RESPONSE_STATUS, this, statusCode);
                            httpStatusEvent.responseHeaders = www.responseHeaders;
                            DispatchEvent(httpStatusEvent);
                        }
                    }

                    // Dispatch event HttpEvent.COMPLETE.
                    object data = ReflectionUtility.GetObjectPropertyValue(www, mDataFormatBindingProperties[mDataFormat]);
                    httpEvent = new HTTPEvent(HTTPEvent.COMPLETE, this, mRequest.url, data);
                    DispatchEvent(httpEvent);
                }
            }
        }
    }
}