using System.IO;

namespace QuickUnity.Net.Sockets
{
    /// <summary>
    /// The interface definition for a TCP Socket packet.
    /// </summary>
    public interface IPacket
    {
        /// <summary>
        /// Gets the data of packet.
        /// </summary>
        /// <value>The data.</value>
        object data
        {
            get;
        }

        /// <summary>
        /// Gets the bytes of data.
        /// </summary>
        /// <value>The bytes.</value>
        byte[] bytes
        {
            get;
        }

        /// <summary>
        /// Writes to stream buffer.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <param name="buffer">The buffer.</param>
        void Write(MemoryStream buffer);
    }
}