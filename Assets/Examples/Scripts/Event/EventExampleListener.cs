using QuickUnity.Events;
using System.Collections;
using UnityEngine;

/// <summary>
/// The Event namespace.
/// </summary>
namespace QuickUnity.Examples.Event
{
    /// <summary>
    /// Class EventExampleListener.
    /// </summary>
    public class EventExampleListener : MonoBehaviour
    {
        // Use this for initialization
        private void Start()
        {
            EventExample example = GetComponent<EventExample>();
            example.AddEventListener(EventExample.TEST_EVENT, TestEventHandler);
        }

        /// <summary>
        /// Tests the event handler.
        /// </summary>
        /// <param name="eventObj">The event object.</param>
        private void TestEventHandler(Events.Event eventObj)
        {
            UnityEngine.Debug.Log(eventObj.Data);
            EventExample example = GetComponent<EventExample>();
            example.RemoveEventListener(EventExample.TEST_EVENT, TestEventHandler);
        }
    }
}