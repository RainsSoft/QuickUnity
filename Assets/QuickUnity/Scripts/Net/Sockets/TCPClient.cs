using System.Net.Sockets;

namespace QuickUnity.Net.Sockets
{
    /// <summary>
    /// Realize TCP Socket network communication client.
    /// </summary>
    public class TCPClient : SocketClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Socket" /> class.
        /// </summary>
        /// <param name="host">The host address.</param>
        /// <param name="port">The port number.</param>
        /// <param name="sendBufferSize">Size of the send buffer.</param>
        /// <param name="receiveBufferSize">Size of the receive buffer.</param>
        public TCPClient(string host, int port = 0, int sendBufferSize = 65536, int receiveBufferSize = 65536)
            : base(host, port, SocketType.Stream, ProtocolType.Tcp, sendBufferSize, receiveBufferSize)
        {
        }

        #region API

        /// <summary>
        /// Connects to socket server.
        /// </summary>
        public override void Connect()
        {
            base.Connect();

            if (mSocket != null)
                mSocket.LingerState = new LingerOption(true, 0);
        }

        #endregion API
    }
}