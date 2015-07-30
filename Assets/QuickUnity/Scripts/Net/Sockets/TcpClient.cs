using QuickUnity.Events;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace QuickUnity.Net.Sockets
{
    /// <summary>
    /// Realize TCP Socket network communication client.
    /// </summary>
    public class TcpClient : ThreadEventDispatcher
    {
        /// <summary>
        /// The send asynchronous callback.
        /// </summary>
        private AsyncCallback mSendAsyncCallback;

        /// <summary>
        /// The receive asynchronous callback.
        /// </summary>
        private AsyncCallback mReceiveAsyncCallback;

        /// <summary>
        /// The socket object.
        /// </summary>
        protected Socket mSocket = null;

        /// <summary>
        /// The bytes of socket received bytes.
        /// </summary>
        protected byte[] mReceivedBytes;

        /// <summary>
        /// The sign of sending data.
        /// </summary>
        protected bool mSendingData = false;

        /// <summary>
        /// The send buffer size.
        /// </summary>
        protected int mSendBufferSize = 0;

        /// <summary>
        /// The receive buffer size.
        /// </summary>
        protected int mReceiveBufferSize = 0;

        /// <summary>
        /// The send packet buffer.
        /// </summary>
        protected Queue mSendPacketQueue;

        /// <summary>
        /// The read buffer of socket connection.
        /// </summary>
        protected MemoryStream mReadBuffer;

        /// <summary>
        /// Gets a value indicating whether this <see cref="TcpClient"/> is connected.
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
        }

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
        /// Whether set delay time when send packet data.
        /// </summary>
        private bool mNoSendDelay = true;

        /// <summary>
        /// Gets or sets a value indicating whether [no send delay].
        /// </summary>
        /// <value><c>true</c> if [no send delay]; otherwise, <c>false</c>.</value>
        public bool noSendDelay
        {
            get { return mNoSendDelay; }
            set { mNoSendDelay = value; }
        }

        /// <summary>
        /// Delay time when send packet data.
        /// </summary>
        private int mSendDelayTime = 16;

        /// <summary>
        /// Gets or sets the send delay time.
        /// </summary>
        /// <value>The send delay time.</value>
        public int sendDelayTime
        {
            get { return mSendDelayTime; }
            set { mSendDelayTime = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Socket"/> class.
        /// </summary>
        /// <param name="host">The host address.</param>
        /// <param name="port">The port number.</param>
        /// <param name="sendBufferSize">Size of the send buffer.</param>
        /// <param name="receiveBufferSize">Size of the receive buffer.</param>
        public TcpClient(string host, int port = 0, int sendBufferSize = 65536, int receiveBufferSize = 65536)
            : base()
        {
            mHost = host;
            mPort = port;

            mSendBufferSize = sendBufferSize;
            mReceiveBufferSize = receiveBufferSize;

            // Initialize received byte array.
            mReceivedBytes = new byte[mReceiveBufferSize];

            // Initialize send and receive data callback.
            mSendAsyncCallback = new AsyncCallback(SendDataAsync);
            mReceiveAsyncCallback = new AsyncCallback(ReceiveDataAsync);

            // Initialize read buffer and write buffer.
            mReadBuffer = new MemoryStream(mSendBufferSize);

            // Initialize send packet buffer.
            mSendPacketQueue = new Queue();
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
                        mSocket.SendBufferSize = mSendBufferSize;
                        mSocket.ReceiveBufferSize = mReceiveBufferSize;
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

                if (packet != null && mSendPacketQueue != null)
                    mSendPacketQueue.Enqueue(packet);
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
                if (connected)
                {
                    try
                    {
                        mSocket.Shutdown(SocketShutdown.Both);
                    }
                    catch (Exception exception)
                    {
                        DispatchErrorEvent("TcpClient.Close() Error: " + exception.Message + exception.StackTrace);
                    }
                }

                mSocket.Close();
                DispatchEvent(new SocketEvent(SocketEvent.CLOSED));
            }
        }

        /// <summary>
        /// Begins send data asynchronously.
        /// </summary>
        private void BeginSend()
        {
            if (mSocket != null && connected)
            {
                if (mSendPacketQueue != null && mSendPacketQueue.Count > 0)
                {
                    IPacket packet = (IPacket)mSendPacketQueue.Dequeue();

                    if (packet != null && packet.bytes != null)
                    {
                        mSendingData = true;

                        try
                        {
                            mSocket.BeginSend(packet.bytes, 0, packet.bytes.Length, SocketFlags.None, mSendAsyncCallback, mSocket);
                        }
                        catch (Exception exception)
                        {
                            DispatchErrorEvent("TcpClient.BeginSend() Error: " + exception.Message + exception.StackTrace);
                        }
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
                try
                {
                    SocketError errorCode;
                    int sendSize = mSocket.EndSend(ar, out errorCode);
                }
                catch (Exception exception)
                {
                    DispatchErrorEvent("TcpClient.SendDataAsync() Error: " + exception.Message + exception.StackTrace);
                }

                // Send packet after delay time.
                if (!mNoSendDelay)
                    Thread.Sleep(mSendDelayTime);

                mSendingData = false;
                BeginSend();
            }
        }

        /// <summary>
        /// Begins receive data asynchronously.
        /// </summary>
        private void BeginReceive()
        {
            try
            {
                if (mSocket != null && connected)
                    mSocket.BeginReceive(mReceivedBytes, 0, mReceivedBytes.Length, SocketFlags.None, mReceiveAsyncCallback, mSocket);
            }
            catch (Exception exception)
            {
                DispatchErrorEvent("TcpClient.BeginReceive() Error: " + exception.Message + exception.StackTrace);
            }
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
                    SocketError errorCode;
                    int bytesRead = mSocket.EndReceive(ar, out errorCode);
                    mReadBuffer.Write(mReceivedBytes, 0, bytesRead);
                    UnpackPacket(bytesRead);
                    BeginReceive();
                }
                catch (Exception exception)
                {
                    DispatchErrorEvent("TcpClient.ReceiveDataAsync() Error: " + exception.Message + exception.StackTrace);
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
                        DispatchEvent(new SocketEvent(SocketEvent.DATA, packet));
                }
            }
        }

        /// <summary>
        /// Dispatches the error event.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        private void DispatchErrorEvent(string errorMessage)
        {
            SocketEvent evt = new SocketEvent(SocketEvent.ERROR);
            evt.errorMessage = errorMessage;
            DispatchEvent(evt);
        }
    }
}