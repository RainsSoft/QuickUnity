using QuickUnity.Events;

namespace QuickUnity.MVCS
{
    /// <summary>
    /// The MVCS Model contract.
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// Sets the context event dispatcher.
        /// </summary>
        /// <value>
        /// The context event dispatcher.
        /// </value>
        IEventDispatcher ContextEventDispatcher
        {
            set;
        }

        /// <summary>
        /// Sets the module event dispatcher.
        /// </summary>
        /// <value>
        /// The module event dispatcher.
        /// </value>
        IEventDispatcher ModuleEventDispatcher
        {
            set;
        }

        /// <summary>
        /// Dispatches the global event.
        /// </summary>
        /// <param name="evt">The event object.</param>
        void Dispatch(Event evt);

        /// <summary>
        /// Dispatches the module event.
        /// </summary>
        /// <param name="evt">The event object.</param>
        void DispatchModuleEvent(Event evt);
    }
}