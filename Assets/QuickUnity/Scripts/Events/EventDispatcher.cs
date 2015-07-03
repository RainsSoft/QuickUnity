using System;
using System.Collections;
using System.Collections.Generic;

namespace QuickUnity.Events
{
    /// <summary>
    /// Class EventDispatcher.
    /// </summary>
    public class EventDispatcher : IEventDispatcher
    {
        /// <summary>
        /// The listeners dictionary.
        /// </summary>
        protected Dictionary<string, List<Action<Event>>> mListeners;

        /// <summary>
        /// Initializes a new sInstance of the <see cref="EventDispatcher"/> class.
        /// </summary>
        public EventDispatcher()
        {
            mListeners = new Dictionary<string, List<Action<Event>>>();
        }

        /// <summary>
        /// Adds the event listener.
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="listener">The listener.</param>
        public virtual void AddEventListener(string type, Action<Event> listener)
        {
            if (!mListeners.ContainsKey(type))
                mListeners.Add(type, new List<Action<Event>>());

            if (!mListeners[type].Contains(listener))
                mListeners[type].Add(listener);
        }

        /// <summary>
        /// Dispatches the event.
        /// </summary>
        /// <param name="event">The event.</param>
        public virtual void DispatchEvent(Event evt)
        {
            string type = evt.eventType;

            if (mListeners.ContainsKey(type))
            {
                List<Action<Event>> listeners = mListeners[type];

                foreach (Action<Event> listener in listeners)
                {
                    if (listener != null)
                        listener(evt);
                }
            }
        }

        /// <summary>
        /// Determines whether [has event listener] [the specified type].
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="listener">The listener.</param>
        public virtual bool HasEventListener(string type, Action<Event> listener)
        {
            return mListeners.ContainsKey(type) && mListeners[type].Contains(listener);
        }

        /// <summary>
        /// Removes the event listener by event name.
        /// </summary>
        /// <param name="type">The type of event.</param>
        public virtual void RemoveEventListenerByName(string type)
        {
            if (mListeners.ContainsKey(type))
                mListeners.Remove(type);
        }

        /// <summary>
        /// Removes the event listener.
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="listener">The listener.</param>
        public virtual void RemoveEventListener(string type, Action<Event> listener)
        {
            if (mListeners.ContainsKey(type))
            {
                List<Action<Event>> listeners = mListeners[type];

                if (listeners.Contains(listener))
                    listeners.Remove(listener);
            }
        }

        /// <summary>
        /// Removes all event listeners.
        /// </summary>
        public virtual void RemoveAllEventListeners()
        {
            mListeners.Clear();
        }
    }
}