﻿using QuickUnity.Events;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace QuickUnity.Net.Sockets
{
    /// <summary>
    /// Realize Socket network communication client.
    /// </summary>
    public class SocketClient : ThreadEventDispatcher
    {
        /// <summary>
        /// The sign of sending data.
        /// </summary>
        protected bool mSendingData = false;

        /// <summary>
        /// The packet packer.
        /// </summary>
        protected IPacketPacker mPacketPacker;

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
        protected IPacketUnpacker mPacketUnpacker;

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
        protected bool mNoSendDelay = true;

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
        protected int mSendDelayTime = 16;

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
        protected Queue mSendPacketQueue;

        /// <summary>
        /// The read buffer of socket connection.
        /// </summary>
        protected MemoryStream mReadBuffer;

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
        }

        /// <summary>
        /// The send buffer size.
        /// </summary>
        protected int mSendBufferSize = 0;

        /// <summary>
        /// The receive buffer size.
        /// </summary>
        protected int mReceiveBufferSize = 0;

        /// <summary>
        /// The socket object.
        /// </summary>
        protected Socket mSocket = null;

        /// <summary>
        /// The bytes of socket received bytes.
        /// </summary>
        protected byte[] mReceivedBytes;

        /// <summary>
        /// The type of socket.
        /// </summary>
        protected SocketType mSocketType = SocketType.Unknown;

        /// <summary>
        /// The type of protocol.
        /// </summary>
        protected ProtocolType mProto = ProtocolType.Unknown;

        /// <summary>
        /// The timeout of sending data.
        /// </summary>
        protected int mSendTimeout = 1000;

        /// <summary>
        /// Gets or sets the timeout of sending data.
        /// </summary>
        /// <value>
        /// The timeout of sending data.
        /// </value>
        public int sendTimeout
        {
            get { return mSendTimeout; }
            set
            {
                mSendTimeout = value;

                if (mSocket != null)
                    mSocket.SendTimeout = value;
            }
        }

        /// <summary>
        /// The timeout of receiving data.
        /// </summary>
        protected int mReceiveTimeout = 1000;

        /// <summary>
        /// Gets or sets the timeout of receiving data.
        /// </summary>
        /// <value>
        /// The timeout of receiving data.
        /// </value>
        public int receiveTimeout
        {
            get { return mReceiveTimeout; }
            set
            {
                mReceiveTimeout = value;

                if (mSocket != null)
                    mSocket.ReceiveTimeout = value;
            }
        }

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
        /// Initializes a new instance of the <see cref="SocketClient" /> class.
        /// </summary>
        /// <param name="host">The host address.</param>
        /// <param name="port">The port number.</param>
        /// <param name="socketType">Type of the socket.</param>
        /// <param name="proto">The proto type.</param>
        /// <param name="sendBufferSize">Size of the send buffer.</param>
        /// <param name="receiveBufferSize">Size of the receive buffer.</param>
        public SocketClient(string host, int port, SocketType socketType, ProtocolType proto = ProtocolType.Tcp,
            int sendBufferSize = 65536, int receiveBufferSize = 65536)
            : base()
        {
            mHost = host;
            mPort = port;
            mSocketType = socketType;
            mProto = proto;

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

        #region API

        /// <summary>
        /// Connects to socket server.
        /// </summary>
        public virtual void Connect()
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
                    Socket tempSocket = new Socket(endPoint.AddressFamily, mSocketType, mProto);
                    tempSocket.Ttl = 42;
                    tempSocket.Connect(endPoint);

                    if (tempSocket.Connected)
                    {
                        mSocket = tempSocket;
                        mSocket.SendTimeout = mSendTimeout;
                        mSocket.ReceiveTimeout = mReceiveTimeout;
                        mSocket.SendBufferSize = mSendBufferSize;
                        mSocket.ReceiveBufferSize = mReceiveBufferSize;
                        DispatchEvent(new SocketEvent(SocketEvent.CONNECTED, this));
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
                DispatchEvent(new SocketEvent(SocketEvent.CLOSED, this));
            }
        }

        #endregion API

        #region Protected Functions

        /// <summary>
        /// Sends the data asynchronously.
        /// </summary>
        /// <param name="ar">The ar.</param>
        protected void SendDataAsync(IAsyncResult ar)
        {
            if (mSocket != null && connected)
            {
                try
                {
                    SocketError errorCode;
                    mSocket.EndSend(ar, out errorCode);
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
        /// Receives the data asynchronously.
        /// </summary>
        /// <param name="ar">The ar.</param>
        protected void ReceiveDataAsync(IAsyncResult ar)
        {
            if (mSocket != null && connected)
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
        /// Begins send data asynchronously.
        /// </summary>
        protected void BeginSend()
        {
            if (mSocket != null && connected)
            {
                if (mSendPacketQueue != null && mSendPacketQueue.Count > 0)
                {
                    try
                    {
                        IPacket packet = (IPacket)mSendPacketQueue.Dequeue();

                        if (packet != null && packet.bytes != null)
                        {
                            mSendingData = true;

                            mSocket.BeginSend(packet.bytes, 0, packet.bytes.Length, SocketFlags.None, mSendAsyncCallback, mSocket);
                        }
                    }
                    catch (Exception exception)
                    {
                        DispatchErrorEvent("TcpClient.BeginSend() Error: " + exception.Message + exception.StackTrace);
                    }
                }
            }
        }

        /// <summary>
        /// Begins receive data asynchronously.
        /// </summary>
        protected void BeginReceive()
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
        /// Unpacks the packet.
        /// </summary>
        protected void UnpackPacket(int bytesRead)
        {
            if (mPacketUnpacker != null)
            {
                IPacket[] packets = mPacketUnpacker.Unpack(mReadBuffer, bytesRead);

                if (packets != null && packets.Length > 0)
                {
                    foreach (IPacket packet in packets)
                        DispatchEvent(new SocketEvent(SocketEvent.DATA, this, packet));
                }
            }
        }

        /// <summary>
        /// Dispatches the error event.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        protected void DispatchErrorEvent(string errorMessage)
        {
            SocketEvent evt = new SocketEvent(SocketEvent.ERROR, this);
            evt.errorMessage = errorMessage;
            DispatchEvent(evt);
        }

        #endregion Protected Functions
    }
}