using QuickUnity.Events;
using System.Collections.Generic;

namespace QuickUnity.Net.Http
{
    /// <summary>
    /// The application dispatches HTTPStatusEvent objects when a network request returns an HTTP status code.
    /// </summary>
    public class HTTPStatusEvent : Event
    {
        /// <summary>
        /// The HttpStatusEvent.HTTP_STATUS constant defines the value of the type property of a httpStatus event object.
        /// </summary>
        public const string HTTP_STATUS = "httpStatus";

        /// <summary>
        /// Unlike the httpStatus event, the httpResponseStatus event is delivered before any response data.
        /// </summary>
        public const string HTTP_RESPONSE_STATUS = "httpResponseStatus";

        /// <summary>
        /// The HTTP status code returned by the server.
        /// </summary>
        private int mStatus = 0;

        /// <summary>
        /// Gets or sets the HTTP status code returned by the server.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public int status
        {
            get { return mStatus; }
            set { mStatus = value; }
        }

        /// <summary>
        /// The response headers that the response returned, as an dictionary of WWW objects.
        /// </summary>
        private Dictionary<string, string> mResponseHeaders;

        /// <summary>
        /// Gets or sets the response headers.
        /// </summary>
        /// <value>
        /// The response headers.
        /// </value>
        public Dictionary<string, string> responseHeaders
        {
            get { return mResponseHeaders; }
            set { mResponseHeaders = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpStatusEvent"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="target">The target of event.</param>
        /// <param name="status">The status.</param>
        public HTTPStatusEvent(string type, object target = null, int status = 0)
            : base(type, target)
        {
            mStatus = status;
        }
    }
}