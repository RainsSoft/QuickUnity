using System.Net.Sockets;

namespace QuickUnity.Net.Sockets
{
    /// <summary>
    /// Realize UDP Socket network communication client.
    /// </summary>
    public class UDPClient : SocketClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UDPClient"/> class.
        /// </summary>
        /// <param name="host">The host address.</param>
        /// <param name="port">The port number.</param>
        /// <param name="sendBufferSize">Size of the send buffer.</param>
        /// <param name="receiveBufferSize">Size of the receive buffer.</param>
        public UDPClient(string host, int port = 0, int sendBufferSize = 65536, int receiveBufferSize = 65536)
            : base(host, port, SocketType.Dgram, ProtocolType.Udp, sendBufferSize, receiveBufferSize)
        {
        }
    }
}