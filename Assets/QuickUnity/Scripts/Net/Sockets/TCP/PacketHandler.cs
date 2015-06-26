using System.Threading;

namespace QuickUnity.Net.Sockets.TCP
{
    /// <summary>
    /// Responsible for handling TCP socket packets.
    /// </summary>
    public abstract class PacketHandler
    {
        /// <summary>
        /// The TCP socket object.
        /// </summary>
        protected SocketTCP mSocketTCP;

        /// <summary>
        /// The thread object.
        /// </summary>
        protected Thread mThread;

        /// <summary>
        /// Initializes a new instance of the <see cref="PacketHandler"/> class.
        /// </summary>
        /// <param name="socketTCP">The socket TCP.</param>
        public PacketHandler(SocketTCP socketTCP)
        {
            mSocketTCP = socketTCP;
            mThread = new Thread(new ThreadStart(ThreadStartFunc));
        }

        /// <summary>
        /// Start to receive packet data.
        /// </summary>
        public void Start()
        {
            if (mThread != null && !mThread.IsAlive)
                mThread.Start();
        }

        /// <summary>
        /// Pause to receive packet data.
        /// </summary>
        public void Pause()
        {
            if (mThread != null)
                mThread.Suspend();
        }

        /// <summary>
        /// Resume to receive packet data.
        /// </summary>
        public void Resume()
        {
            if (mThread != null)
                mThread.Resume();
        }

        /// <summary>
        /// Stop receiving packet data.
        /// </summary>
        public void Stop()
        {
            if (mThread != null)
            {
                try
                {
                    mThread.Abort();
                }
                catch (ThreadStateException error)
                {
                    Resume();
                }
            }
        }

        /// <summary>
        /// Threads start function.
        /// </summary>
        protected abstract void ThreadStartFunc();
    }
}