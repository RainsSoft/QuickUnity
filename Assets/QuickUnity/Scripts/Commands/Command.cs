using QuickUnity.Events;

namespace QuickUnity.Commands
{
    /// <summary>
    /// A base <c>ICommand</c> implementation.
    /// </summary>
    public abstract class Command : EventDispatcher, ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        public Command()
            : base()
        {
        }

        #region API

        /// <summary>
        /// Executes this command.
        /// </summary>
        public virtual void Execute()
        {
            Executed();
        }

        #endregion API

        #region Protected Functions

        /// <summary>
        /// When the command finish executing, invoke this function.
        /// </summary>
        protected virtual void Executed()
        {
            DispatchEvent(new CommandEvent(CommandEvent.EXECUTED, this));
        }

        #endregion Protected Functions
    }
}