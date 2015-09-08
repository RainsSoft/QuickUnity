using System;
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
        /// Finalizes an instance of the <see cref="EventDispatcher"/> class.
        /// </summary>
        ~EventDispatcher()
        {
            RemoveAllEventListeners();
            mListeners = null;
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
                int count = listeners.Count;
                for (int i = 0; i < count; i++)
                {
                    if (listeners[i] != null)
                    {
                        listeners[i](evt);
                    }
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
        /// Removes the event listeners by target.
        /// </summary>
        /// <param name="target">The target object.</param>
        public virtual void RemoveEventListenersByTarget(object target)
        {
            Dictionary<string, List<Action<Event>>> listeners = new Dictionary<string, List<Action<Event>>>();

            // 记录需要删除的Listener
            foreach (KeyValuePair<string, List<Action<Event>>> kvp in mListeners)
            {
                string eventType = kvp.Key;
                List<Action<Event>> list = kvp.Value;

                foreach (Action<Event> listner in list)
                {
                    if (listner.Target == target)
                    {
                        if (!listeners.ContainsKey(eventType))
                            listeners[eventType] = new List<Action<Event>>();

                        listeners[eventType].Add(listner);
                    }
                }
            }

            // 实际删除Listener
            if (listeners.Count > 0)
            {
                foreach (KeyValuePair<string, List<Action<Event>>> kvp in listeners)
                {
                    string eventType = kvp.Key;
                    List<Action<Event>> list = kvp.Value;

                    foreach (Action<Event> listener in list)
                        RemoveEventListener(eventType, listener);
                }
            }
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
            if (mListeners != null)
                mListeners.Clear();
        }
    }
}