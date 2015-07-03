using QuickUnity.Components;

namespace QuickUnity.Events
{
    /// <summary>
    /// When you use Timer component, Timer component will dispatch TimerEvent.
    /// </summary>
    public class TimerEvent : QuickUnity.Events.Event
    {
        /// <summary>
        /// When timer reach the time by delay set, this event will be dispatched.
        /// </summary>
        public const string TIMER = "timer";

        /// <summary>
        /// When timer complete the repeat count, this event will be dispatched.
        /// </summary>
        public const string TIMER_COMPLETE = "timerComplete";

        /// <summary>
        /// The timer object.
        /// </summary>
        private ITimer mTimer;

        /// <summary>
        /// Gets the timer object.
        /// </summary>
        /// <value>The timer.</value>
        public ITimer timer
        {
            get { return mTimer; }
        }

        /// <summary>
        /// The delta time.
        /// </summary>
        private float mDeltaTime;

        /// <summary>
        /// Gets the delta time.
        /// </summary>
        /// <value>The delta time.</value>
        public float deltaTime
        {
            get { return mDeltaTime; }
        }

        /// <summary>
        /// Initializes a new sInstance of the <see cref="TimerEvent"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="timer">The timer.</param>
        /// <param name="deltaTime">The delta time.</param>
        public TimerEvent(string type, ITimer timer, float deltaTime)
            : base(type)
        {
            mTimer = timer;
            mDeltaTime = deltaTime;
        }
    }
}