using System.IO;

namespace QuickUnity.Net.Sockets
{
    /// <summary>
    /// A base <c>IPacket</c> implementation.
    /// </summary>
    public class Packet : IPacket
    {
        /// <summary>
        /// The data of packet.
        /// </summary>
        protected object mData;

        /// <summary>
        /// Gets the data of packet.
        /// </summary>
        /// <value>The data.</value>
        public object data
        {
            get { return mData; }
        }

        /// <summary>
        /// The bytes of data.
        /// </summary>
        protected byte[] mBytes;

        /// <summary>
        /// Gets the bytes of data.
        /// </summary>
        /// <value>The bytes.</value>
        public byte[] bytes
        {
            get { return mBytes; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Packet"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        public Packet(object data = null)
        {
            mData = data;
        }

        /// <summary>
        /// Writes to stream buffer.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        public virtual void Write(MemoryStream buffer)
        {
            BinaryWriter writer = new BinaryWriter(buffer);

            if (mBytes != null)
                writer.Write(mBytes);
        }
    }
}