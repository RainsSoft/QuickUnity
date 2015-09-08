using System;
using System.Collections.Generic;

namespace QuickUnity.Events
{
    /// <summary>
    /// A base <c>IThreadEventDispatcher</c> implementation.
    /// </summary>
    public class ThreadEventDispatcher : EventDispatcher, IThreadEventDispatcher
    {
        /// <summary>
        /// The pending listeners.
        /// </summary>
        private Dictionary<string, List<Action<Event>>> mPendingListeners;

        /// <summary>
        /// The events list.
        /// </summary>
        private List<Event> mEvents;

        /// <summary>
        /// The pending events.
        /// </summary>
        private List<Event> mPendingEvents;

        /// <summary>
        /// If the listener is in pending.
        /// </summary>
        protected bool mPending = false;

        /// <summary>
        /// Gets a value indicating whether this <see cref="ThreadEventDispatcher"/> is pending.
        /// </summary>
        /// <value>
        ///   <c>true</c> if pending; otherwise, <c>false</c>.
        /// </value>
        public bool pending
        {
            get { return mPending; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadEventDispatcher"/> class.
        /// </summary>
        public ThreadEventDispatcher()
            : base()
        {
            mPendingListeners = new Dictionary<string, List<Action<Event>>>();
            mEvents = new List<Event>();
            mPendingEvents = new List<Event>();
        }

        /// <summary>
        /// Adds the event listener.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="listener">The listener.</param>
        public override void AddEventListener(string type, Action<Event> listener)
        {
            lock (this)
            {
                if (mPending)
                {
                    if (!mPendingListeners.ContainsKey(type))
                        mPendingListeners.Add(type, new List<Action<Event>>());

                    if (!mPendingListeners[type].Contains(listener))
                        mPendingListeners[type].Add(listener);

                    return;
                }

                base.AddEventListener(type, listener);
            }
        }

        /// <summary>
        /// Dispatches the event.
        /// </summary>
        /// <param name="evt">The evt.</param>
        public override void DispatchEvent(Event evt)
        {
            lock (this)
            {
                if (!mListeners.ContainsKey(evt.eventType))
                    return;

                bool running = true;

                do
                {
                    if (mPending)
                    {
                        if (!mPendingEvents.Contains(evt))
                            mPendingEvents.Add(evt);
                    }
                    else
                    {
                        foreach (Event pendingEvent in mPendingEvents)
                            mEvents.Add(pendingEvent);

                        mPendingEvents.Clear();
                        mEvents.Add(evt);
                        running = false;
                    }
                } while (running);
            }
        }

        /// <summary>
        /// Removes the event listener by event name.
        /// </summary>
        /// <param name="type">The type of event.</param>
        public override void RemoveEventListenerByName(string type)
        {
            lock (this)
            {
                if (!mListeners.ContainsKey(type))
                    return;

                bool running = true;

                do
                {
                    if (!mPending)
                    {
                        base.RemoveEventListenerByName(type);
                        running = false;
                    }
                } while (running);
            }
        }

        /// <summary>
        /// Removes the event listeners by target.
        /// </summary>
        /// <param name="target">The target object.</param>
        public override void RemoveEventListenersByTarget(object target)
        {
            lock (this)
            {
                bool running = true;

                do
                {
                    if (!mPending)
                    {
                        base.RemoveEventListenersByTarget(target);
                        running = false;
                    }
                } while (running);
            }
        }

        /// <summary>
        /// Removes the event listener.
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="listener">The listener.</param>
        public override void RemoveEventListener(string type, Action<Event> listener)
        {
            lock (this)
            {
                if (!mListeners.ContainsKey(type))
                    return;

                bool running = true;

                do
                {
                    if (!mPending)
                    {
                        base.RemoveEventListener(type, listener);
                        running = false;
                    }
                } while (running);
            }
        }

        /// <summary>
        /// Removes all event listeners.
        /// </summary>
        public override void RemoveAllEventListeners()
        {
            lock (this)
            {
                bool running = true;

                do
                {
                    if (!mPending)
                    {
                        base.RemoveAllEventListeners();
                        running = false;
                    }
                } while (running);
            }
        }

        /// <summary>
        /// Updates.
        /// </summary>
        public void Update()
        {
            lock (this)
            {
                if (mEvents.Count == 0)
                {
                    foreach (string eventType in mPendingListeners.Keys)
                    {
                        foreach (Action<Event> listener in mPendingListeners[eventType])
                            AddEventListener(eventType, listener);
                    }

                    mPendingListeners.Clear();
                    ShiftPendingEvents();
                    return;
                }

                mPending = true;

                foreach (Event evt in mEvents)
                {
                    if (mListeners.ContainsKey(evt.eventType))
                    {
                        List<Action<Event>> listeners = mListeners[evt.eventType];

                        foreach (Action<Event> listener in listeners)
                        {
                            if (listener != null)
                                listener(evt);
                        }
                    }
                }

                mEvents.Clear();
            }

            mPending = false;
        }

        /// <summary>
        /// Determines whether [has pending event listener] [the specified type].
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="listener">The listener.</param>
        /// <returns><c>true</c> if [has pending event listener] [the specified type]; otherwise, <c>false</c>.</returns>
        public bool HasPendingEventListener(string type, Action<Event> listener)
        {
            return mPendingListeners.ContainsKey(type) && mPendingListeners[type].Contains(listener);
        }

        /// <summary>
        /// Shifts the pending events.
        /// </summary>
        private void ShiftPendingEvents()
        {
            foreach (Event evt in mPendingEvents)
                mEvents.Add(evt);

            mPendingEvents.Clear();
        }
    }
}