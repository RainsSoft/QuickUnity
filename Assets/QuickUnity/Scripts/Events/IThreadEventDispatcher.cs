using System;

namespace QuickUnity.Events
{
    /// <summary>
    /// The interface definition for a thread-security EventDispatcher.
    /// </summary>
    public interface IThreadEventDispatcher : IEventDispatcher
    {
        /// <summary>
        /// Updates.
        /// </summary>
        void Update();

        /// <summary>
        /// Determines whether [has pending event listener] [the specified type].
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="listener">The listener.</param>
        /// <returns><c>true</c> if [has pending event listener] [the specified type]; otherwise, <c>false</c>.</returns>
        bool HasPendingEventListener(string type, Action<Event> listener);
    }
}