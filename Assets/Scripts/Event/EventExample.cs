using QuickUnity.Events;
using System.Collections;
using UnityEngine;

namespace QuickUnity.Examples.Event
{
    /// <summary>
    /// Class EventExample.
    /// </summary>
    [AddComponentMenu("")]
    public class EventExample : MonoBehaviourEventDispatcher
    {
        /// <summary>
        /// The event for test
        /// </summary>
        public const string TEST_EVENT = "test";

        /// <summary>
        /// Starts this sInstance.
        /// </summary>
        private void Start()
        {
            Invoke("Dispatch", 2.0f);
        }

        /// <summary>
        /// Dispatches this sInstance.
        /// </summary>
        private void Dispatch()
        {
            DispatchEvent(new Events.Event(TEST_EVENT));
        }
    }
}