using QuickUnity.Events;

namespace QuickUnity.Commands
{
    /// <summary>
    /// The event object for CommandQueue.
    /// </summary>
    public class CommandQueueEvent : Event
    {
        /// <summary>
        /// When CommandQueue start to execute commands, it will dispatch this event.
        /// </summary>
        public const string START = "start";

        /// <summary>
        /// When CommandQueue in the progress of executing commands, it will dispatch this event.
        /// </summary>
        public const string PROGRESS = "progress";

        /// <summary>
        /// When CommandQueue interrupt to execute commands, it will dispatch this event.
        /// </summary>
        public const string INTERRUPT = "interrupt";

        /// <summary>
        /// When CommandQueue complete executing commands, it will dispatch this event.
        /// </summary>
        public const string COMPLETE = "complete";

        /// <summary>
        /// Gets or sets the progress of command queue executing.
        /// </summary>
        /// <value>
        /// The progress of command queue executing.
        /// </value>
        public float progress
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandQueueEvent"/> class.
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="target">The target object of event.</param>
        public CommandQueueEvent(string type, object target = null)
            : base(type, target)
        {
        }
    }
}