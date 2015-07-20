using QuickUnity.Events;
using QuickUnity.Tasks;
using System;
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
        TextureUnreadable,
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
        private uint mBytesLoaded = 0;

        /// <summary>
        /// Gets or sets the bytes loaded.
        /// </summary>
        /// <value>The bytes loaded.</value>
        public uint bytesLoaded
        {
            get { return mBytesLoaded; }
        }

        /// <summary>
        /// Indicates the total number of bytes in the downloaded data.
        /// </summary>
        private uint mBytesTotal = 0;

        /// <summary>
        /// Gets the bytes total.
        /// </summary>
        /// <value>The bytes total.</value>
        public uint bytesTotal
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
            TaskManager.instance.AddTask("URLLoader.Request", mRequestTask);
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
                DispatchEvent(new HttpEvent(HttpEvent.OPEN, mRequest.url, mRequest.data));
            }
            else
            {
                if (mRequest.method == URLRequestMethod.Post)
                {
                    if (mRequest.requestHeaders != null)
                    {
                        www = new WWW(url, (byte[])mRequest.data, mRequest.requestHeaders);
                        DispatchEvent(new HttpEvent(HttpEvent.OPEN, mRequest.url, mRequest.data));
                    }
                    else
                    {
                        www = new WWW(url, (byte[])mRequest.data);
                        DispatchEvent(new HttpEvent(HttpEvent.OPEN, mRequest.url, mRequest.data));
                    }
                }
                else if (mRequest.method == URLRequestMethod.Get)
                {
                    www = new WWW(url);
                    DispatchEvent(new HttpEvent(HttpEvent.OPEN, mRequest.url, mRequest.data));
                }
            }

            yield return www;

            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.LogWarning("Http request error: " + www.error);
                DispatchEvent(new HttpEvent(HttpEvent.OPEN, mRequest.url, mRequest.data, www.error));
            }
            else
            {
            }
        }
    }
}