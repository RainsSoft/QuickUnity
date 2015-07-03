using QuickUnity.Net.Sockets;
using System.IO;

namespace QuickUnity.Net.Sockets.TCP
{
    /// <summary>
    /// The interface definition for a Socket packet unpacker.
    /// </summary>
    public interface IPacketUnpacker
    {
        /// <summary>
        /// Unpacks data from read buffer of socket.
        /// </summary>
        /// <param name="readBuffer">The read buffer.</param>
        /// <param name="bytesRead">The bytes read.</param>
        /// <returns>IPacket[].</returns>
        IPacket[] Unpack(MemoryStream readBuffer, int bytesRead);
    }
}