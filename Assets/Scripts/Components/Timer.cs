using QuickUnity.Components;
using QuickUnity.Events;
using System.Collections;
using UnityEngine;

/// <summary>
/// The Events namespace.
/// </summary>
namespace QuickUnity.Events
{
    /// <summary>
    /// Class TimerEvent.
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
        /// Gets the timer object.
        /// </summary>
        /// <value>The timer.</value>
        public Timer Timer
        {
            get { return mData as Timer; }
        }

        /// <summary>
        /// The delta time.
        /// </summary>
        private float mDeltaTime;

        /// <summary>
        /// Gets the delta time.
        /// </summary>
        /// <value>The delta time.</value>
        public float DeltaTime
        {
            get { return mDeltaTime; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerEvent"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="timer">The timer.</param>
        /// <param name="deltaTime">The delta time.</param>
        public TimerEvent(string type, Timer timer, float deltaTime)
            : base(type, timer)
        {
            mDeltaTime = deltaTime;
        }
    }
}

/// <summary>
/// The Components namespace.
/// </summary>
namespace QuickUnity.Components
{
    /// <summary>
    /// Class Timer.
    /// </summary>
    public class Timer : EventDispatcher
    {
        /// <summary>
        /// The current count of timer.
        /// </summary>
        private int mCurrentCount = 0;

        /// <summary>
        /// Gets the current count of timer.
        /// </summary>
        /// <value>The current count.</value>
        public int CurrentCount
        {
            get { return mCurrentCount; }
        }

        /// <summary>
        /// The delay time of timer.
        /// </summary>
        private float mDelay;

        /// <summary>
        /// Gets or sets the delay time of timer.
        /// </summary>
        /// <value>The delay.</value>
        public float Delay
        {
            get { return mDelay; }
            set { mDelay = value; }
        }

        /// <summary>
        /// The repeat count of timer.
        /// </summary>
        private int mRepeatCount;

        /// <summary>
        /// Gets or sets the repeat count of timer.
        /// </summary>
        /// <value>The repeat count.</value>
        public int RepeatCount
        {
            get { return mRepeatCount; }
            set { mRepeatCount = value; }
        }

        /// <summary>
        /// The state of timer. If the timer is running, it is true, or false.
        /// </summary>
        private bool mRunning;

        /// <summary>
        /// Gets a value indicating whether this <see cref="Timer"/> is running.
        /// </summary>
        /// <value><c>true</c> if running; otherwise, <c>false</c>.</value>
        public bool Running
        {
            get { return mRunning; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Timer"/> class.
        /// </summary>
        /// <param name="delay">The delay time. Unit is second.</param>
        /// <param name="repeatCount">The repeat count.</param>
        public Timer(float delay, int repeatCount = 0)
            : base()
        {
            mDelay = delay;
            mRepeatCount = repeatCount;
        }

        /// <summary>
        /// The time of timer timing.
        /// </summary>
        private float time = 0.0f;

        /// <summary>
        /// Updates the timer timing.
        /// </summary>
        /// <param name="deltaTime">The delta time.</param>
        public void Update(float deltaTime)
        {
            if (mRunning)
            {
                time += deltaTime;

                if (time >= mDelay)
                {
                    // Dispatch timer event.
                    mCurrentCount++;

                    if (mCurrentCount == int.MaxValue)
                        mCurrentCount = 0;

                    DispatchEvent(new TimerEvent(TimerEvent.TIMER, this, time));

                    // If reach the repeat count number, stop timing.
                    if (mRepeatCount != 0 && mCurrentCount >= mRepeatCount)
                    {
                        Stop();
                        DispatchEvent(new TimerEvent(TimerEvent.TIMER_COMPLETE, this, time));
                    }

                    time = 0.0f;
                }
            }
        }

        /// <summary>
        /// This timer start timing.
        /// </summary>
        public void Start()
        {
            mRunning = true;
        }

        /// <summary>
        /// This timer resets timing. Set currentCount to 0.
        /// </summary>
        public void Reset()
        {
            if (mRunning)
            {
                Stop();
                mCurrentCount = 0;
                time = 0.0f;
            }
        }

        /// <summary>
        /// This timer stop timing.
        /// </summary>
        public void Stop()
        {
            mRunning = false;
        }
    }
}