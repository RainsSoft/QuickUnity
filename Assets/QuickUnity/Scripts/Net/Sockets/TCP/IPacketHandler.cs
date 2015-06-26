namespace QuickUnity.Net.Sockets.TCP
{
    /// <summary>
    /// Interface IPacketHandler
    /// </summary>
    public interface IPacketHandler
    {
        /// <summary>
        /// Start to receive packet data.
        /// </summary>
        void Start();

        /// <summary>
        /// Pause to receive packet data.
        /// </summary>
        void Pause();

        /// <summary>
        /// Resume to receive packet data.
        /// </summary>
        void Resume();

        /// <summary>
        /// Stop receiving packet data.
        /// </summary>
        void Stop();
    }
}