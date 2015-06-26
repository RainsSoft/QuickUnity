namespace QuickUnity.Net.Sockets.TCP
{
    /// <summary>
    /// Responsible for sending TCP socket packets.
    /// </summary>
    public class PacketSender : PacketHandler, IPacketSender
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PacketHandler" /> class.
        /// </summary>
        /// <param name="socketTCP">The TCP socket.</param>
        public PacketSender(SocketTCP socketTCP)
            : base(socketTCP)
        {
        }

        /// <summary>
        /// Threads start function.
        /// </summary>
        protected override void ThreadStartFunc()
        {
        }
    }
}