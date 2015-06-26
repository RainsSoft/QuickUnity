using System.Net;
using System.Net.Sockets;

namespace QuickUnity.Net.Sockets.TCP
{
    /// <summary>
    /// Realize TCP Socket network communication.
    /// </summary>
    public class SocketTCP
    {
        /// <summary>
        /// The socket object.
        /// </summary>
        protected Socket mSocket = null;

        /// <summary>
        /// Gets a value indicating whether this <see cref="SocketTCP"/> is connected.
        /// </summary>
        /// <value><c>true</c> if connected; otherwise, <c>false</c>.</value>
        public bool connected
        {
            get
            {
                if (mSocket != null)
                    return mSocket.Connected;

                return false;
            }
        }

        /// <summary>
        /// The packet sender.
        /// </summary>
        protected IPacketSender mPacketSender;

        /// <summary>
        /// Gets or sets the packet sender.
        /// </summary>
        /// <value>The packet sender.</value>
        public IPacketSender packetSender
        {
            get { return mPacketSender; }
            set { mPacketSender = value; }
        }

        /// <summary>
        /// The packet receiver.
        /// </summary>
        protected IPacketReceiver mPacketReceiver;

        /// <summary>
        /// Gets or sets the packet receiver.
        /// </summary>
        /// <value>The packet receiver.</value>
        public IPacketReceiver PacketReceiver
        {
            get { return mPacketReceiver; }
            set { mPacketReceiver = value; }
        }

        /// <summary>
        /// The host address.
        /// </summary>
        protected string mHost;

        /// <summary>
        /// Gets or sets the host address.
        /// </summary>
        /// <value>The host.</value>
        public string host
        {
            get { return mHost; }
            set { mHost = value; }
        }

        /// <summary>
        /// The port number.
        /// </summary>
        protected int mPort;

        /// <summary>
        /// Gets or sets the port number.
        /// </summary>
        /// <value>The port.</value>
        public int port
        {
            get { return mPort; }
            set { mPort = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Socket"/> class.
        /// </summary>
        /// <param name="host">The host address.</param>
        /// <param name="port">The port number.</param>
        public SocketTCP(string host, int port)
        {
            mHost = host;
            port = mPort;
        }

        /// <summary>
        /// Connects to socket server.
        /// </summary>
        public void Connect()
        {
            if (!string.IsNullOrEmpty(mHost))
            {
                // Get host related information.
                IPHostEntry hostEntry = Dns.GetHostEntry(mHost);

                // Loop through the AddressList to obtain the supported AddressFamily. This is to avoid
                // an exception that occurs when the host IP Address is not compatible with the address family
                // (typical in the IPv6 case).
                foreach (IPAddress address in hostEntry.AddressList)
                {
                    IPEndPoint endPoint = new IPEndPoint(address, mPort);
                    Socket tempSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    tempSocket.Connect(endPoint);

                    if (tempSocket.Connected)
                    {
                        mSocket = tempSocket;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Receives the TCP socket data.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int ReceiveData()
        {
            if(mSocket != null)
            {
                mSocket.Receive()
            }
        }
    }
}