namespace QuickUnity.Timers
{
    /// <summary>
    /// When you use Timer component, Timer component will dispatch TimerEvent.
    /// </summary>
    public class TimerEvent : Events.Event
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
            get { return (ITimer)mTarget; }
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
            set { mDeltaTime = value; }
        }

        /// <summary>
        /// Initializes a new sInstance of the <see cref="TimerEvent"/> class.
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="target">The target object of event.</param>
        /// <param name="deltaTime">The delta time.</param>
        public TimerEvent(string type, object target = null, float deltaTime = 0.0f)
            : base(type, target)
        {
            mDeltaTime = deltaTime;
        }
    }
}