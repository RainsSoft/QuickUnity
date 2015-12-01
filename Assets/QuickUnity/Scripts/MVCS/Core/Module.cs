using QuickUnity.Events;
using System;

namespace QuickUnity.MVCS
{
    /// <summary>
    /// Abstract MVCS <c>IModule</c> implementation.
    /// </summary>
    public class Module : IModule, IModelMap, IMediatorMap
    {
        /// <summary>
        /// The event dispatcher.
        /// </summary>
        protected IEventDispatcher mEventDispatcher;

        /// <summary>
        /// Gets the event dispatcher.
        /// </summary>
        /// <value>
        /// The event dispatcher.
        /// </value>
        public IEventDispatcher eventDispatcher
        {
            get { return mEventDispatcher; }
        }

        /// <summary>
        /// The model map.
        /// </summary>
        protected IModelMap mModelMap;

        /// <summary>
        /// The mediator map.
        /// </summary>
        protected IMediatorMap mMediatorMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="Context"/> class.
        /// </summary>
        public Module()
        {
            mEventDispatcher = new EventDispatcher();
            mModelMap = new ModelMap();
            mMediatorMap = new MediatorMap();
        }

        #region Public Functions

        #region IEventDispatcher Implementations

        /// <summary>
        /// Adds the event listener.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="listener">The listener.</param>
        public void AddEventListener(string type, Action<Event> listener)
        {
            mEventDispatcher.AddEventListener(type, listener);
        }

        /// <summary>
        /// Dispatches the event.
        /// </summary>
        /// <param name="evt"></param>
        public void DispatchEvent(Event evt)
        {
            mEventDispatcher.DispatchEvent(evt);
        }

        /// <summary>
        /// Determines whether [has event listener] [the specified type].
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="listener">The listener.</param>
        /// <returns>
        ///   <c>true</c> if [has event listener] [the specified type]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasEventListener(string type, Action<Event> listener)
        {
            return mEventDispatcher.HasEventListener(type, listener);
        }

        /// <summary>
        /// Removes the event listener by event name.
        /// </summary>
        /// <param name="type">The type of event.</param>
        public void RemoveEventListenerByName(string type)
        {
            mEventDispatcher.RemoveEventListenerByName(type);
        }

        /// <summary>
        /// Removes the event listeners by target.
        /// </summary>
        /// <param name="target">The target object.</param>
        public void RemoveEventListenersByTarget(object target)
        {
            mEventDispatcher.RemoveEventListenersByTarget(target);
        }

        /// <summary>
        /// Removes the event listener.
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="listener">The listener.</param>
        public void RemoveEventListener(string type, Action<Event> listener)
        {
            mEventDispatcher.RemoveEventListener(type, listener);
        }

        /// <summary>
        /// Removes all event listeners.
        /// </summary>
        public void RemoveAllEventListeners()
        {
            mEventDispatcher.RemoveAllEventListeners();
        }

        #endregion IEventDispatcher Implementations

        #region IModelMap Implementations

        /// <summary>
        /// Registers the model.
        /// </summary>
        /// <param name="model">The model object.</param>
        public virtual void RegisterModel(IModel model)
        {
            mModelMap.RegisterModel(model);
            model.ModuleEventDispatcher = this;
        }

        /// <summary>
        /// Retrieves the model.
        /// </summary>
        /// <param name="modelType">Type of the model.</param>
        /// <returns>
        /// The model object.
        /// </returns>
        public IModel RetrieveModel(Type modelType)
        {
            return mModelMap.RetrieveModel(modelType);
        }

        /// <summary>
        /// Removes the model.
        /// </summary>
        /// <param name="modelType">Type of the model object.</param>
        public void RemoveModel(Type modelType)
        {
            mModelMap.RemoveModel(modelType);
        }

        /// <summary>
        /// Removes the model.
        /// </summary>
        /// <param name="model">The model object.</param>
        public void RemoveModel(IModel model)
        {
            mModelMap.RemoveModel(model);
        }

        #endregion IModelMap Implementations

        #region IMediatorMap Implementations

        /// <summary>
        /// Registers the mediator.
        /// </summary>
        /// <param name="viewComponent">The view component.</param>
        /// <param name="mediator">The mediator.</param>
        public void RegisterMediator(IMediator mediator)
        {
            mMediatorMap.RegisterMediator(mediator);
        }

        /// <summary>
        /// Retrieves the mediator.
        /// </summary>
        /// <param name="mediatorName">Name of the mediator.</param>
        /// <returns>The mediator object.</returns>
        public IMediator RetrieveMediator(string mediatorName)
        {
            return mMediatorMap.RetrieveMediator(mediatorName);
        }

        /// <summary>
        /// Removes the mediator.
        /// </summary>
        /// <param name="mediatorName">Name of the mediator.</param>
        public void RemoveMediator(string mediatorName)
        {
            mMediatorMap.RemoveMediator(mediatorName);
        }

        #endregion IMediatorMap Implementations

        #endregion Public Functions
    }
}