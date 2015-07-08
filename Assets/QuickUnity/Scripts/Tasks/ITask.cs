namespace QuickUnity.Tasks
{
    /// <summary>
    /// The interface definition for the Task component.
    /// </summary>
    public interface ITask
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="ITask"/> is running.
        /// </summary>
        /// <value><c>true</c> if running; otherwise, <c>false</c>.</value>
        bool running
        {
            get;
        }

        /// <summary>
        /// Gets the state of the task.
        /// </summary>
        /// <value>The state of the task.</value>
        TaskState taskState
        {
            get;
        }

        /// <summary>
        /// Starts this task.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops this task.
        /// </summary>
        void Stop();

        /// <summary>
        /// Pauses this task.
        /// </summary>
        void Pause();

        /// <summary>
        /// Resumes this task.
        /// </summary>
        void Resume();
    }
}