using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// The Event namespace of FSM.
/// </summary>
namespace QuickUnity.FSM.Event
{
    /// <summary>
    /// The dispatcher of FSM event.
    /// </summary>
    public class FSMEventDispatcher
    {
        /// <summary>
        /// Triggers the specified event name.
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        public void Trigger(string eventName)
        {
            Call(eventName, null);
        }

        /// <summary>
        /// Dispatches the specified event.
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        public void DispatchEvent(string eventName)
        {
        }

        /// <summary>
        /// Calls the specified event name.
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="args">The arguments.</param>
        private void Call(string eventName, params object[] args)
        {
            List<int> listeners;

            if (mRegisteredEvents.TryGetValue(eventName, out listeners))
            {
                for (int i = mRegisteredListeners.Count - 1; i >= 0; --i)
                {
                    Listener listener;

                    if (mRegisteredListeners.TryGetValue(listeners[i], out listener))
                    {
                        if (!listener.ActionFunc(args))
                        {
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Class Listen.
        /// </summary>
        internal class Listener
        {
            /// <summary>
            /// The identifier
            /// </summary>
            public int ID;

            /// <summary>
            /// The action function
            /// </summary>
            public Func<object[], bool> ActionFunc;
        }

        /// <summary>
        /// Class Dispatched.
        /// </summary>
        internal class Dispatch
        {
            /// <summary>
            /// The event name
            /// </summary>
            private string mEventName;

            /// <summary>
            /// Gets the name of the event.
            /// </summary>
            /// <value>The name of the event.</value>
            public string EventName
            {
                get { return mEventName; }
            }

            /// <summary>
            /// The arguments
            /// </summary>
            private object[] mArgs;

            /// <summary>
            /// Gets the arguments.
            /// </summary>
            /// <value>The arguments.</value>
            public object[] Args
            {
                get { return mArgs; }
            }

            /// <summary>
            /// Sets the specified event name.
            /// </summary>
            /// <param name="eventName">Name of the event.</param>
            /// <param name="args">The arguments.</param>
            /// <returns>Dispatched.</returns>
            public Dispatch Set(string eventName, params object[] args)
            {
                mEventName = eventName;
                mArgs = args;
                return this;
            }
        }

        /// <summary>
        /// The dictionary of registered events.
        /// </summary>
        private Dictionary<string, List<int>> mRegisteredEvents = new Dictionary<string, List<int>>();

        /// <summary>
        /// The dictionary of registered listens.
        /// </summary>
        private Dictionary<int, Listener> mRegisteredListeners = new Dictionary<int, Listener>();

        /// <summary>
        /// The stack of free listens.
        /// </summary>
        private Stack<Listener> mFreeListeners = new Stack<Listener>();

        /// <summary>
        /// The stack of free dispatches.
        /// </summary>
        private Stack<Dispatch> mFreeDispatches = new Stack<Dispatch>();

        /// <summary>
        /// The queue of dispatches.
        /// </summary>
        private Queue<Dispatch> mDispatches = new Queue<Dispatch>();

        /// <summary>
        /// The next listen identifier.
        /// </summary>
        private int nextListenID = 4711;
    }
}