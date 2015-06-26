namespace QuickUnity.Net.Sockets.TCP
{
    /// <summary>
    /// Responsible for receiving TCP socket packets.
    /// </summary>
    public class PacketReceiver : PacketHandler, IPacketReceiver
    {
        /// <summary>
        /// The TCP socket.
        /// </summary>
        protected SocketTCP mSocketTCP;

        /// <summary>
        /// The interval time of receiving packet data.
        /// </summary>
        protected int mInterval = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="PacketReceiver"/> class.
        /// </summary>
        /// <param name="socketTCP">The TCP socket.</param>
        /// <param name="interval">The interval time of receving packet data.</param>
        public PacketReceiver(SocketTCP socketTCP, int interval = 16)
            : base(socketTCP)
        {
            mInterval = interval;
        }

        /// <summary>
        /// Threads start function.
        /// </summary>
        protected override void ThreadStartFunc()
        {
        }
    }
}