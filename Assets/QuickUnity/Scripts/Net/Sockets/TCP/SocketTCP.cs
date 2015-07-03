using QuickUnity.Events;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace QuickUnity.Net.Sockets.TCP
{
    /// <summary>
    /// Realize TCP Socket network communication.
    /// </summary>
    public class SocketTCP : ThreadEventDispatcher
    {
        /// <summary>
        /// The socket object.
        /// </summary>
        private Socket mSocket = null;

        /// <summary>
        /// The bytes of socket received bytes.
        /// </summary>
        private byte[] mReceivedBytes;

        /// <summary>
        /// The sign of sending data.
        /// </summary>
        private bool mSendingData = false;

        /// <summary>
        /// The send asynchronous callback.
        /// </summary>
        private AsyncCallback mSendAsyncCallback;

        /// <summary>
        /// The receive asynchronous callback.
        /// </summary>
        private AsyncCallback mReceiveAsyncCallback;

        /// <summary>
        /// The send packet buffer.
        /// </summary>
        private Queue mSendPacketBuffer;

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
        /// The host address.
        /// </summary>
        private string mHost;

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
        private int mPort;

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
        /// The time of sending delay.
        /// </summary>
        private int mSendDelay = 0;

        /// <summary>
        /// The packet packer.
        /// </summary>
        private IPacketPacker mPacketPacker;

        /// <summary>
        /// Gets or sets the packet packer.
        /// </summary>
        /// <value>The packet packer.</value>
        public IPacketPacker packetPacker
        {
            get { return mPacketPacker; }
            set { mPacketPacker = value; }
        }

        /// <summary>
        /// The packet unpacker.
        /// </summary>
        private IPacketUnpacker mPacketUnpacker;

        /// <summary>
        /// Gets or sets the packet unpacker.
        /// </summary>
        /// <value>The packet unpacker.</value>
        public IPacketUnpacker packetUnpacker
        {
            get { return mPacketUnpacker; }
            set { mPacketUnpacker = value; }
        }

        /// <summary>
        /// The read buffer of socket connection.
        /// </summary>
        private MemoryStream mReadBuffer;

        /// <summary>
        /// The write buffer of socket connection.
        /// </summary>
        private MemoryStream mWriteBuffer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Socket"/> class.
        /// </summary>
        /// <param name="host">The host address.</param>
        /// <param name="port">The port number.</param>
        /// <param name="receivedByteSize">Size of the received byte.</param>
        /// <param name="sendDelay">The send delay time.</param>
        public SocketTCP(string host, int port = 0, int receivedByteSize = 1024, int sendDelay = 16)
            : base()
        {
            mHost = host;
            mPort = port;
            mSendDelay = sendDelay;

            // Initialize received byte array.
            mReceivedBytes = new byte[receivedByteSize];

            // Initialize send and receive data callback.
            mSendAsyncCallback = new AsyncCallback(SendDataAsync);
            mReceiveAsyncCallback = new AsyncCallback(ReceiveDataAsync);

            // Initialize read buffer and write buffer.
            mReadBuffer = new MemoryStream();
            mWriteBuffer = new MemoryStream();

            // Initialize send packet buffer.
            mSendPacketBuffer = new Queue();
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
                        DispatchEvent(new SocketEvent(SocketEvent.CONNECTED));
                        BeginReceive();
                        BeginSend();
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        /// <summary>
        /// Sends data.
        /// </summary>
        /// <param name="data">The data.</param>
        public void Send(object data)
        {
            if (data == null)
                return;

            if (mPacketPacker != null)
            {
                IPacket packet = mPacketPacker.Pack(data);

                if (packet != null && mSendPacketBuffer != null)
                    mSendPacketBuffer.Enqueue(packet);
            }

            if (!mSendingData)
                BeginSend();
        }

        /// <summary>
        /// Closes socket connect.
        /// </summary>
        public virtual void Close()
        {
            if (mSocket != null)
            {
                mSocket.Shutdown(SocketShutdown.Both);
                mSocket.Close();
            }
        }

        /// <summary>
        /// Begins send data asynchronously.
        /// </summary>
        private void BeginSend()
        {
            if (mSocket != null && connected)
            {
                if (mSendPacketBuffer != null && mSendPacketBuffer.Count > 0)
                {
                    IPacket packet = (IPacket)mSendPacketBuffer.Dequeue();

                    if (packet != null && packet.bytes != null)
                    {
                        mWriteBuffer.Position = 0;
                        packet.Write(mWriteBuffer);
                        int size = (int)mWriteBuffer.Position;
                        mSendingData = true;
                        mWriteBuffer.Position = 0;
                        mSocket.BeginSend(mWriteBuffer.GetBuffer(), 0, size, SocketFlags.None, mSendAsyncCallback, mSocket);
                    }
                }
            }
        }

        /// <summary>
        /// Sends the data asynchronously.
        /// </summary>
        /// <param name="ar">The ar.</param>
        private void SendDataAsync(IAsyncResult ar)
        {
            if (mSocket != null)
            {
                int byteSend = mSocket.EndSend(ar);
                mSendingData = false;
                Thread.Sleep(mSendDelay);
                BeginSend();
            }
        }

        /// <summary>
        /// Begins receive data asynchronously.
        /// </summary>
        private void BeginReceive()
        {
            if (mSocket != null && connected)
                mSocket.BeginReceive(mReceivedBytes, 0, mReceivedBytes.Length, SocketFlags.None, mReceiveAsyncCallback, mSocket);
        }

        /// <summary>
        /// Receives the data asynchronously.
        /// </summary>
        /// <param name="ar">The ar.</param>
        private void ReceiveDataAsync(IAsyncResult ar)
        {
            if (mSocket != null)
            {
                try
                {
                    int bytesRead = mSocket.EndReceive(ar);
                    mReadBuffer.Write(mReceivedBytes, 0, bytesRead);
                    UnpackPacket(bytesRead);
                    BeginReceive();
                }
                catch (Exception ex)
                {
                    System.Console.Write(ex.Message);
                }
            }
        }

        /// <summary>
        /// Unpacks the packet.
        /// </summary>
        private void UnpackPacket(int bytesRead)
        {
            if (mPacketUnpacker != null)
            {
                IPacket[] packets = mPacketUnpacker.Unpack(mReadBuffer, bytesRead);

                if (packets != null && packets.Length > 0)
                {
                    foreach (IPacket packet in packets)
                    {
                        DispatchEvent(new SocketEvent(SocketEvent.DATA, packet));
                    }
                }
            }
        }
    }
}