using QuickUnity.Events;

namespace QuickUnity.Commands
{
    /// <summary>
    /// The event object for Command.
    /// </summary>
    public class CommandEvent : Event
    {
        /// <summary>
        /// When the command has executed, it will dispatch this event.
        /// </summary>
        public const string EXECUTED = "executed";

        /// <summary>
        /// When the command found error in executing, it will dispatch this event.
        /// </summary>
        public const string ERROR = "error";

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandEvent"/> class.
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="target">The target object of event.</param>
        public CommandEvent(string type, object target = null)
            : base(type, target)
        {
        }
    }
}