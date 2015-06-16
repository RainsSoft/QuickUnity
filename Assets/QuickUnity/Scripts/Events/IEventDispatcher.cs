using System;

namespace QuickUnity.Events
{
    /// <summary>
    /// Interface IEventDispatcher
    /// </summary>
    public interface IEventDispatcher
    {
        /// <summary>
        /// Adds the event listener.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="listener">The listener.</param>
        void AddEventListener(string type, Action<Event> listener);

        /// <summary>
        /// Dispatches the event.
        /// </summary>
        /// <param name="eventObj">The event object.</param>
        void DispatchEvent(Event eventObj);

        /// <summary>
        /// Determines whether [has event listener] [the specified type].
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if [has event listener] [the specified type]; otherwise, <c>false</c>.</returns>
        bool HasEventListener(string type);

        /// <summary>
        /// Removes the event listener by event name.
        /// </summary>
        /// <param name="type">The type.</param>
        void RemoveEventListenerByName(string type);

        /// <summary>
        /// Removes the event listener.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="listener">The listener.</param>
        void RemoveEventListener(string type, Action<Event> listener);

        /// <summary>
        /// Removes all event listeners.
        /// </summary>
        void RemoveAllEventListeners();
    }
}