﻿using QuickUnity.Events;

namespace QuickUnity.Net.Http
{
    /// <summary>
    /// When you use URLLoader, it will dispatch HTTPEvent.
    /// </summary>
    public class HTTPEvent : Event
    {
        /// <summary>
        /// Dispatched when a load operation start.
        /// </summary>
        public const string OPEN = "open";

        /// <summary>
        /// Dispatched when a load operation in progress.
        /// </summary>
        public const string PROGRESS = "progress";

        /// <summary>
        /// Dispatched when a load operation is complete.
        /// </summary>
        public const string COMPLETE = "complete";

        /// <summary>
        /// Dispatched when a load operation get error message.
        /// </summary>
        public const string ERROR = "error";

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
        /// The progress of downloading.
        /// </summary>
        private float mProgress = 0.0f;

        /// <summary>
        /// Gets or sets the progress of downloading.
        /// </summary>
        /// <value>
        /// The progress.
        /// </value>
        public float progress
        {
            get { return mProgress; }
            set { mProgress = value; }
        }

        /// <summary>
        /// An object containing data to be transmitted with the URL request or the data received from the load operation.
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
        /// Returns an error message if there was an error during the download.
        /// </summary>
        private string mError;

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>The error.</value>
        public string error
        {
            get { return mError; }
            set { mError = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpEvent"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="target">The target of event.</param>
        /// <param name="url">The URL of HTTP request.</param>
        /// <param name="data">The data.</param>
        /// <param name="error">The error.</param>
        public HTTPEvent(string type, object target = null, string url = null, object data = null, string error = null)
            : base(type, target)
        {
            mUrl = url;
            mData = data;
            mError = error;
        }
    }
}