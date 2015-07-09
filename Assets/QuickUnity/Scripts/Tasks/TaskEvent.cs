namespace QuickUnity.Tasks
{
    /// <summary>
    /// When you use Task component, Task component will dispatch TaskEvent.
    /// </summary>
    public class TaskEvent : QuickUnity.Events.Event
    {
        /// <summary>
        /// When task start to run, it will dispatch this event.
        /// </summary>
        public const string TASK_START = "taskStart";

        /// <summary>
        /// When task stop to run, it will dispatch this event.
        /// </summary>
        public const string TASK_STOP = "taskStop";

        /// <summary>
        /// When task pause, it will dispatch this event.
        /// </summary>
        public const string TASK_PAUSE = "taskPause";

        /// <summary>
        /// When task resume, it will dispatch this event.
        /// </summary>
        public const string TASK_RESUME = "taskResume";

        /// <summary>
        /// The task object.
        /// </summary>
        private ITask mTask;

        /// <summary>
        /// Gets the task object.
        /// </summary>
        /// <value>The task.</value>
        public ITask task
        {
            get { return mTask; }
            set { mTask = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskEvent"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="task">The ITask object.</param>
        public TaskEvent(string type, ITask task)
            : base(type)
        {
            mTask = task;
        }
    }
}