namespace QuickUnity.Net.Sockets
{
    /// <summary>
    /// The interface definition for a Socket packet packer.
    /// </summary>
    public interface IPacketPacker
    {
        /// <summary>
        /// Packs the specified data to packet.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>IPacket.</returns>
        IPacket Pack(object data);
    }
}