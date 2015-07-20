using System;
using System.Collections.Generic;

namespace QuickUnity.Net.Http
{
    /// <summary>
    /// Request method enum of url request.
    /// </summary>
    public enum URLRequestMethod
    {
        Get,
        Post
    }

    /// <summary>
    /// The URLRequest class captures all of the information in a single HTTP request.
    /// </summary>
    public class URLRequest
    {
        /// <summary>
        /// Controls the HTTP form submission method.
        /// </summary>
        private URLRequestMethod mMethod = URLRequestMethod.Get;

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        /// <value>The method.</value>
        public URLRequestMethod method
        {
            get { return mMethod; }
            set { mMethod = value; }
        }

        /// <summary>
        /// An object containing data to be transmitted with the URL request.
        /// </summary>
        private object mData;

        /// <summary>
        /// Gets or sets an object containing data to be transmitted with the URL request.
        /// </summary>
        /// <value>The data.</value>
        public object data
        {
            get { return mData; }
            set { mData = value; }
        }

        /// <summary>
        /// The URL to be requested.
        /// </summary>
        private string mUrl;

        /// <summary>
        /// Gets or sets the URL to be requested.
        /// </summary>
        /// <value>The URL.</value>
        public string url
        {
            get { return mUrl; }
            set { mUrl = value; }
        }

        /// <summary>
        /// The callback response function.
        /// </summary>
        private Action<object> mCallback;

        /// <summary>
        /// Gets or sets the response callback.
        /// </summary>
        /// <value>The callback.</value>
        public Action<object> callback
        {
            get { return mCallback; }
            set { mCallback = value; }
        }

        /// <summary>
        /// The dictionary of HTTP request headers to be appended to the HTTP request.
        /// </summary>
        private Dictionary<string, string> mRequestHeaders;

        /// <summary>
        /// Gets or sets the request headers.
        /// </summary>
        /// <value>The request headers.</value>
        public Dictionary<string, string> requestHeaders
        {
            get { return mRequestHeaders; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="URLRequest"/> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        public URLRequest(string url, Action<object> callback)
        {
            mUrl = url;
            mCallback = callback;
        }

        /// <summary>
        /// Adds the header to be appended to the HTTP request.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void AddHeader(string key, string value)
        {
            if (mRequestHeaders == null)
                mRequestHeaders = new Dictionary<string, string>();

            mRequestHeaders.Add(key, value);
        }
    }
}