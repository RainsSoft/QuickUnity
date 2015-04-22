using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Events namespace.
/// </summary>
namespace QuickUnity.Events
{
    /// <summary>
    /// Class EventDispatcher.
    /// </summary>
    public class EventDispatcher : MonoBehaviour, IEventDispatcher
    {
        /// <summary>
        /// The listeners dictionary.
        /// </summary>
        private Dictionary<string, Action<Event>> mListeners = new Dictionary<string, Action<Event>>();

        /// <summary>
        /// Adds the event listener.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="listener">The listener.</param>
        public void AddEventListener(string type, Action<Event> listener)
        {
            if (HasEventListener(type))
                return;

            mListeners.Add(type, listener);
        }

        /// <summary>
        /// Dispatches the event.
        /// </summary>
        /// <param name="eventObj">The event object.</param>
        public void DispatchEvent(Event eventObj)
        {
            string type = eventObj.EventType;

            if (mListeners.ContainsKey(type))
            {
                Action<Event> listener = mListeners[type];

                if (listener != null)
                    listener(eventObj);
            }
        }

        /// <summary>
        /// Determines whether [has event listener] [the specified type].
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if [has event listener] [the specified type]; otherwise, <c>false</c>.</returns>
        public bool HasEventListener(string type)
        {
            foreach (KeyValuePair<string, Action<Event>> kvp in mListeners)
            {
                if (kvp.Key == type)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Removes the event listener.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="listener">The listener.</param>
        public void RemoveEventListener(string type, Action<Event> listener)
        {
            foreach (KeyValuePair<string, Action<Event>> kvp in mListeners)
            {
                if (kvp.Key == type)
                {
                    mListeners.Remove(type);
                    return;
                }
            }
        }

        /// <summary>
        /// Removes all event listeners.
        /// </summary>
        public void RemoveAllEventListeners()
        {
            mListeners.Clear();
        }
    }
}