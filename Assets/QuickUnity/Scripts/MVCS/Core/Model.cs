using QuickUnity.Events;

namespace QuickUnity.MVCS
{
    /// <summary>
    /// Abstract MVCS <c>IModel</c> implementation.
    /// </summary>
    public class Model : IModel
    {
        /// <summary>
        /// The context event dispatcher.
        /// </summary>
        protected IEventDispatcher mContextEventDispatcher;

        /// <summary>
        /// Sets the context event dispatcher.
        /// </summary>
        /// <value>
        /// The context event dispatcher.
        /// </value>
        public IEventDispatcher ContextEventDispatcher
        {
            set { mContextEventDispatcher = value; }
        }

        /// <summary>
        /// The module event dispatcher.
        /// </summary>
        protected IEventDispatcher mModuleEventDispatcher;

        /// <summary>
        /// Sets the module event dispatcher.
        /// </summary>
        /// <value>
        /// The module event dispatcher.
        /// </value>
        public IEventDispatcher ModuleEventDispatcher
        {
            set { mModuleEventDispatcher = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Model"/> class.
        /// </summary>
        public Model()
        {
        }

        #region Public Functions

        /// <summary>
        /// Dispatches the global event.
        /// </summary>
        /// <param name="evt">The event object.</param>
        public void Dispatch(Event evt)
        {
            if (mContextEventDispatcher != null)
                mContextEventDispatcher.DispatchEvent(evt);
        }

        /// <summary>
        /// Dispatches the module event.
        /// </summary>
        /// <param name="evt">The event object.</param>
        public void DispatchModuleEvent(Event evt)
        {
            if (mModuleEventDispatcher != null)
                mModuleEventDispatcher.DispatchEvent(evt);
        }

        #endregion Public Functions
    }
}