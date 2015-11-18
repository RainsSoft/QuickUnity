using System;
using UnityEngine;

namespace QuickUnity.Events
{
    /// <summary>
    /// Class BehaviourEventDispatcher.
    /// </summary>
    public class BehaviourEventDispatcher : MonoBehaviour, IEventDispatcher
    {
        /// <summary>
        /// The event dispatcher.
        /// </summary>
        private EventDispatcher mDispatcher;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            mDispatcher = new EventDispatcher();
        }

        /// <summary>
        /// Called when [destroy].
        /// </summary>
        protected virtual void OnDestroy()
        {
            if (mDispatcher != null)
            {
                mDispatcher.RemoveAllEventListeners();
                mDispatcher = null;
            }
        }

        /// <summary>
        /// Adds the event listener.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="listener">The listener.</param>
        public void AddEventListener(string type, Action<Event> listener)
        {
            if (mDispatcher != null)
                mDispatcher.AddEventListener(type, listener);
        }

        /// <summary>
        /// Dispatches the event.
        /// </summary>
        /// <param name="event">The event.</param>
        public void DispatchEvent(Event evt)
        {
            if (mDispatcher != null)
                mDispatcher.DispatchEvent(evt);
        }

        /// <summary>
        /// Determines whether [has event listener] [the specified type].
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="listener">The listener.</param>
        /// <returns><c>true</c> if [has event listener] [the specified type]; otherwise, <c>false</c>.</returns>
        public bool HasEventListener(string type, Action<Event> listener)
        {
            if (mDispatcher != null)
                return mDispatcher.HasEventListener(type, listener);

            return false;
        }

        /// <summary>
        /// Removes the event listener by event name.
        /// </summary>
        /// <param name="type">The type.</param>
        public void RemoveEventListenerByName(string type)
        {
            if (mDispatcher != null)
                mDispatcher.RemoveEventListenerByName(type);
        }

        /// <summary>
        /// Removes the event listeners by target.
        /// </summary>
        /// <param name="target">The target object.</param>
        public virtual void RemoveEventListenersByTarget(object target)
        {
            if (mDispatcher != null)
                mDispatcher.RemoveEventListenersByTarget(target);
        }

        /// <summary>
        /// Removes the event listener.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="listener">The listener.</param>
        public void RemoveEventListener(string type, Action<Event> listener)
        {
            if (mDispatcher != null)
                mDispatcher.RemoveEventListener(type, listener);
        }

        /// <summary>
        /// Removes all event listeners.
        /// </summary>
        public void RemoveAllEventListeners()
        {
            if (mDispatcher != null)
                mDispatcher.RemoveAllEventListeners();
        }
    }
}