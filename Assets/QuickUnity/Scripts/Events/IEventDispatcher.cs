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
        /// <param name="type">The type of event.</param>
        /// <param name="listener">The listener.</param>
        void AddEventListener(string type, Action<Event> listener);

        /// <summary>
        /// Dispatches the event.
        /// </summary>
        /// <param name="event">The event.</param>
        void DispatchEvent(Event evt);

        /// <summary>
        /// Determines whether [has event listener] [the specified type].
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="listener">The listener.</param>
        /// <returns><c>true</c> if [has event listener] [the specified type]; otherwise, <c>false</c>.</returns>
        bool HasEventListener(string type, Action<Event> listener);

        /// <summary>
        /// Removes the event listener by event name.
        /// </summary>
        /// <param name="type">The type of event.</param>
        void RemoveEventListenerByName(string type);

        /// <summary>
        /// Removes the event listeners by target.
        /// </summary>
        /// <param name="target">The target object.</param>
        void RemoveEventListenersByTarget(object target);

        /// <summary>
        /// Removes the event listener.
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="listener">The listener.</param>
        void RemoveEventListener(string type, Action<Event> listener);

        /// <summary>
        /// Removes all event listeners.
        /// </summary>
        void RemoveAllEventListeners();
    }
}