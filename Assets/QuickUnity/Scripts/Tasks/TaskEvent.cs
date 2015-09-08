namespace QuickUnity.Tasks
{
    /// <summary>
    /// When you use Task component, Task component will dispatch TaskEvent.
    /// </summary>
    public class TaskEvent : Events.Event
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
        /// Gets the task object.
        /// </summary>
        /// <value>The task.</value>
        public ITask task
        {
            get { return (ITask)mTarget; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskEvent"/> class.
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="target">The target of event.</param>
        public TaskEvent(string type, object target = null)
            : base(type, target)
        {
        }
    }
}