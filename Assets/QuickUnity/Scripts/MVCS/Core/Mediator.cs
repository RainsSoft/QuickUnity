using QuickUnity.Events;
using System;

namespace QuickUnity.MVCS
{
    /// <summary>
    /// Abstract MVCS <c>IMediator</c> implementation.
    /// </summary>
    public class Mediator : IMediator
    {
        /// <summary>
        /// The mediator name.
        /// </summary>
        protected string mMediatorName;

        /// <summary>
        /// The mediator name.
        /// </summary>
        public string mediatorName
        {
            get { return mMediatorName; }
        }

        /// <summary>
        /// The view component.
        /// </summary>
        protected object mViewComponent;

        /// <summary>
        /// Gets the view component.
        /// </summary>
        /// <value>
        /// The view component.
        /// </value>
        public object viewComponent
        {
            get { return mViewComponent; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mediator" /> class.
        /// </summary>
        /// <param name="mediatorName">Name of the mediator.</param>
        /// <param name="viewComponent">The view component.</param>
        public Mediator(string mediatorName = null, object viewComponent = null)
        {
            mediatorName = string.IsNullOrEmpty(mediatorName) ? this.GetType().FullName : mediatorName;
            mViewComponent = viewComponent;
        }

        #region Public Functions

        #region IMediator Implementations

        /// <summary>
        /// Called when [register].
        /// </summary>
        public virtual void OnRegister()
        {
        }

        /// <summary>
        /// Called when [remove].
        /// </summary>
        public virtual void OnRemove()
        {
        }

        #endregion IMediator Implementations

        #endregion Public Functions

        #region Protected Functions

        /// <summary>
        /// Adds the context event listener.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="listener">The listener.</param>
        protected void AddContextEventListener(string type, Action<Event> listener)
        {
        }

        /// <summary>
        /// Adds the module event listener.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="listener">The listener.</param>
        protected void AddModuleEventListener(string type, Action<Event> listener)
        {
        }

        #endregion Protected Functions
    }
}