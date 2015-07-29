namespace QuickUnity.Net.Sockets
{
    /// <summary>
    /// When you use socket, socket will dispatch SocketEvent.
    /// </summary>
    public class SocketEvent : QuickUnity.Events.Event
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
        public const string CLOSED = "Closed";

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
        /// Initializes a new instance of the <see cref="SocketEvent"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="packet">The packet.</param>
        public SocketEvent(string type, IPacket packet = null)
            : base(type)
        {
            mPacket = packet;
        }
    }
}