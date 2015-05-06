using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// The Events namespace.
/// </summary>
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
        /// Awakes this script.
        /// </summary>
        protected virtual void Awake()
        {
            mDispatcher = new EventDispatcher();
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
        /// <param name="eventObj">The event object.</param>
        public void DispatchEvent(Event eventObj)
        {
            if (mDispatcher != null)
                mDispatcher.DispatchEvent(eventObj);
        }

        /// <summary>
        /// Determines whether [has event listener] [the specified type].
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if [has event listener] [the specified type]; otherwise, <c>false</c>.</returns>
        public bool HasEventListener(string type)
        {
            if (mDispatcher != null)
                return mDispatcher.HasEventListener(type);

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