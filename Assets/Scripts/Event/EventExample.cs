using QuickUnity.Events;
using System.Collections;
using UnityEngine;

/// <summary>
/// The Event namespace.
/// </summary>
namespace QuickUnity.Examples.Event
{
    /// <summary>
    /// Class EventExample.
    /// </summary>
    public class EventExample : BehaviourEventDispatcher
    {
        /// <summary>
        /// The event for test
        /// </summary>
        public const string TEST_EVENT = "test";

        /// <summary>
        /// Starts this instance.
        /// </summary>
        private void Start()
        {
            Invoke("Dispatch", 2.0f);
        }

        /// <summary>
        /// Dispatches this instance.
        /// </summary>
        private void Dispatch()
        {
            DispatchEvent(new Events.Event(TEST_EVENT, "Hello World!"));
        }
    }
}