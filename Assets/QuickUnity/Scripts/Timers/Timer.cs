﻿using QuickUnity.Events;

namespace QuickUnity.Timers
{
    /// <summary>
    /// A base <c>ITimer</c> implementation.
    /// </summary>
    public class Timer : EventDispatcher, ITimer
    {
        /// <summary>
        /// The current count of timer.
        /// </summary>
        protected int mCurrentCount = 0;

        /// <summary>
        /// Gets the current count of timer.
        /// </summary>
        /// <value>The current count.</value>
        public int currentCount
        {
            get { return mCurrentCount; }
        }

        /// <summary>
        /// The delay time of timer.
        /// </summary>
        protected float mDelay;

        /// <summary>
        /// Gets or sets the delay time of timer.
        /// </summary>
        /// <value>The delay.</value>
        public float delay
        {
            get { return mDelay; }
            set { mDelay = value; }
        }

        /// <summary>
        /// The repeat count of timer.
        /// </summary>
        protected int mRepeatCount;

        /// <summary>
        /// Gets or sets the repeat count of timer.
        /// </summary>
        /// <value>The repeat count.</value>
        public int repeatCount
        {
            get { return mRepeatCount; }
            set { mRepeatCount = value; }
        }

        /// <summary>
        /// The state of timer. If the timer is running, it is true, or false.
        /// </summary>
        protected bool mRunning;

        /// <summary>
        /// Gets a value indicating whether this <see cref="Timer"/> is running.
        /// </summary>
        /// <value><c>true</c> if running; otherwise, <c>false</c>.</value>
        public bool running
        {
            get { return mRunning; }
        }

        /// <summary>
        /// The time of timer timing.
        /// </summary>
        protected float mTime = 0.0f;

        /// <summary>
        /// Gets the time of timer.
        /// </summary>
        /// <value>
        /// The time.
        /// </value>
        public float time
        {
            get { return mTime; }
        }

        /// <summary>
        /// Initializes a new sInstance of the <see cref="Timer"/> class.
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
        /// Tick.
        /// </summary>
        /// <param name="deltaTime">The delta time.</param>
        public virtual void Tick(float deltaTime)
        {
            if (mRunning)
            {
                mTime += deltaTime;

                if (mTime >= mDelay)
                {
                    // Dispatch timer event.
                    mCurrentCount++;

                    if (mCurrentCount == int.MaxValue)
                        mCurrentCount = 0;

                    DispatchEvent(new TimerEvent(TimerEvent.TIMER, this, mTime));

                    // If reach the repeat count number, stop timing.
                    if (mRepeatCount != 0 && mCurrentCount >= mRepeatCount)
                    {
                        Stop();
                        DispatchEvent(new TimerEvent(TimerEvent.TIMER_COMPLETE, this, mTime));
                    }

                    mTime = 0.0f;
                }
            }
        }

        /// <summary>
        /// This timer start timing.
        /// </summary>
        public void Start()
        {
            mRunning = true;
            DispatchEvent(new TimerEvent(TimerEvent.TIMER_START, this, mTime));
        }

        /// <summary>
        /// This timer pause timing.
        /// </summary>
        public void Pause()
        {
            mRunning = false;
        }

        /// <summary>
        /// This timer resume timing.
        /// </summary>
        public void Resume()
        {
            mRunning = true;
        }

        /// <summary>
        /// This timer resets timing. Set currentCount to 0.
        /// </summary>
        public void Reset()
        {
            if (mRunning)
                Stop();

            mCurrentCount = 0;
            mTime = 0.0f;
        }

        public void Reset(float delay, int repeatCount = 0)
        {
            mDelay = delay;
            mRepeatCount = repeatCount;
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