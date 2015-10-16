using QuickUnity.Events;

namespace QuickUnity.Commands
{
    /// <summary>
    /// The interface definition for the Command component.
    /// </summary>
    public interface ICommand : IEventDispatcher
    {
        /// <summary>
        /// Executes this command.
        /// </summary>
        void Execute();
    }
}