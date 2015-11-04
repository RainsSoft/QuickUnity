using QuickUnity.Events;

namespace QuickUnity.Timers
{
    /// <summary>
    /// The interface definition for the Timer component.
    /// </summary>
    public interface ITimer : IEventDispatcher
    {
        /// <summary>
        /// Gets or sets the delay time of timer.
        /// </summary>
        /// <value>The delay.</value>
        float delay { get; set; }

        /// <summary>
        /// Gets the time of timer.
        /// </summary>
        /// <value>
        /// The time.
        /// </value>
        float time { get; }

        /// <summary>
        /// Gets the current count of timer.
        /// </summary>
        /// <value>The current count.</value>
        int currentCount { get; }

        /// <summary>
        /// Gets or sets the repeat count of timer.
        /// </summary>
        /// <value>The repeat count.</value>
        int repeatCount { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="ITimer"/> is running.
        /// </summary>
        /// <value><c>true</c> if running; otherwise, <c>false</c>.</value>
        bool running { get; }

        /// <summary>
        /// This timer start timing.
        /// </summary>
        void Start();

        /// <summary>
        /// This timer pause timing.
        /// </summary>
        void Pause();

        /// <summary>
        /// This timer resume timing.
        /// </summary>
        void Resume();

        /// <summary>
        /// This timer resets timing. Set currentCount to 0.
        /// </summary>
        void Reset();

        /// <summary>
        /// This timer stop timing.
        /// </summary>
        void Stop();

        /// <summary>
        /// Tick.
        /// </summary>
        /// <param name="deltaTime">The delta time.</param>
        void Tick(float deltaTime);
    }
}