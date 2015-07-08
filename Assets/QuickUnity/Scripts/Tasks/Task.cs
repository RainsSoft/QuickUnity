namespace QuickUnity.Tasks
{
    /// <summary>
    /// The enum state of task.
    /// </summary>
    public enum TaskState
    {
        Running,
        Pause,
        Stop
    }

    /// <summary>
    /// A base <c>ITask</c> implementation.
    /// </summary>
    public class Task : ITask
    {
        /// <summary>
        /// Whether this task is running or not.
        /// </summary>
        protected bool mRunning = false;

        /// <summary>
        /// Gets a value indicating whether this <see cref="ITask" /> is running.
        /// </summary>
        /// <value><c>true</c> if running; otherwise, <c>false</c>.</value>
        public bool running
        {
            get { return mRunning; }
        }

        /// <summary>
        /// The task state.
        /// </summary>
        protected TaskState mTaskState;

        /// <summary>
        /// Gets the state of the task.
        /// </summary>
        /// <value>The state of the task.</value>
        public TaskState taskState
        {
            get { return mTaskState; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Task"/> class.
        /// </summary>
        public Task()
        {
        }

        /// <summary>
        /// Starts this task.
        /// </summary>
        public void Start()
        {
            mRunning = true;
            mTaskState = TaskState.Running;
        }

        /// <summary>
        /// Stops this task.
        /// </summary>
        public void Stop()
        {
            mRunning = false;
            mTaskState = TaskState.Stop;
        }

        /// <summary>
        /// Pauses this task.
        /// </summary>
        public void Pause()
        {
            mRunning = false;
            mTaskState = TaskState.Pause;
        }

        /// <summary>
        /// Resumes this task.
        /// </summary>
        public void Resume()
        {
            mRunning = true;
            mTaskState = TaskState.Running;
        }
    }
}