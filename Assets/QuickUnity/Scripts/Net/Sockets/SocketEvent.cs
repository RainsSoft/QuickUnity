namespace QuickUnity.Net.Sockets
{
    /// <summary>
    /// When you use socket, socket will dispatch SocketEvent.
    /// </summary>
    public class SocketEvent : Events.Event
    {
        /// <summary>
        /// When socket was connected to server, it will dispatch CONNECTED event.
        /// </summary>
        public const string CONNECTED = "connected";

        /// <summary>
        /// When socket received data from server, it will dispatch DATA event after unpacker unpack packet.
        /// </summary>
        public const string DATA = "data";

        /// <summary>
        /// When socket was closed, it will dispatch CLOSED event.
        /// </summary>
        public const string CLOSED = "closed";

        /// <summary>
        /// When socket got error, it will dispatch ERROR event.
        /// </summary>
        public const string ERROR = "Error";

        /// <summary>
        /// The packet data.
        /// </summary>
        private IPacket mPacket;

        /// <summary>
        /// Gets the packet.
        /// </summary>
        /// <value>The packet.</value>
        public IPacket packet
        {
            get { return mPacket; }
        }

        /// <summary>
        /// The error message.
        /// </summary>
        private string mErrorMessage;

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public string errorMessage
        {
            get { return mErrorMessage; }
            set { mErrorMessage = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SocketEvent"/> class.
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="target">The target of event.</param>
        /// <param name="packet">The packet.</param>
        public SocketEvent(string type, object target = null, IPacket packet = null)
            : base(type, target)
        {
            mPacket = packet;
        }
    }
}