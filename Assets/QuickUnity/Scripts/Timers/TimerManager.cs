using QuickUnity.Patterns;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace QuickUnity.Timers
{
    /// <summary>
    /// Hold and manage all timer objects.
    /// </summary>
    public class TimerManager : SingletonMonoBehaviour<TimerManager>
    {
        /// <summary>
        /// The timer dictionary.
        /// </summary>
        private Dictionary<string, ITimer> mTimers;

        /// <summary>
        /// Whether the application paused.
        /// </summary>
        private bool mApplicationPaused = false;

        /// <summary>
        /// Whether paused manually.
        /// </summary>
        private bool mPaused = false;

        #region Messages

        /// <summary>
        /// Awake this script.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();

            // Initialize timer list.
            mTimers = new Dictionary<string, ITimer>();
        }

        /// <summary>
        /// Update in fixed time.
        /// </summary>
        private void FixedUpdate()
        {
            if (enabled && !mApplicationPaused && !mPaused)
            {
                float deltaTime = Time.fixedDeltaTime;

                if (mTimers != null && mTimers.Count > 0)
                {
                    try
                    {
                        foreach (KeyValuePair<string, ITimer> kvp in mTimers)
                        {
                            ITimer timer = kvp.Value;
                            timer.Tick(deltaTime);
                        }
                    }
                    catch (InvalidOperationException exception)
                    {
                        Debug.Log(exception.StackTrace);
                    }
                }
            }
        }

        /// <summary>
        /// This function is called when the MonoBehaviour will be destroyed.
        /// </summary>
        protected override void OnDestroy()
        {
            base.OnDestroy();

            // Remove all timers.
            RemoveAllTimers();
            mTimers = null;
        }

        /// <summary>
        /// Sent to all game objects when the player pauses.
        /// </summary>
        /// <param name="pauseStatus">if set to <c>true</c> [pause status].</param>
        private void OnApplicationPause(bool pauseStatus)
        {
            mApplicationPaused = pauseStatus;
        }

        #endregion Messages

        #region API

        /// <summary>
        /// Pause all timers.
        /// </summary>
        public void Pause()
        {
            mPaused = true;
        }

        /// <summary>
        /// Resume all timers.
        /// </summary>
        public void Resume()
        {
            mPaused = false;
        }

        /// <summary>
        /// Gets the timer.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Timer.</returns>
        public ITimer GetTimer(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            if (mTimers.ContainsKey(name))
                return mTimers[name];

            return null;
        }

        /// <summary>
        /// If owns the timer.
        /// </summary>
        /// <param name="timer">The timer.</param>
        /// <returns>System.Boolean.</returns>
        public bool OwnTimer(ITimer timer)
        {
            return mTimers.ContainsValue(timer);
        }

        /// <summary>
        /// Adds the timer object.
        /// </summary>
        /// <param name="name">The name of timer.</param>
        /// <param name="timer">The timer.</param>
        /// <param name="autoStart">if set to <c>true</c> [automatic start timer].</param>
        public void AddTimer(string name, ITimer timer, bool autoStart = true)
        {
            if (GetTimer(name) != null)
            {
                Debug.LogWarning("Already got a timer with the same name, please change the name or remove the timer of TimerManager already own!");
                return;
            }

            if (!string.IsNullOrEmpty(name))
            {
                mTimers.Add(name, timer);

                if (autoStart)
                    timer.Start();
            }
        }

        /// <summary>
        /// Removes the timer by timer name.
        /// </summary>
        /// <param name="name">The name of timer.</param>
        /// <param name="autoStart">if set to <c>true</c> [automatic stop timer].</param>
        public void RemoveTimer(string name, bool autoStop = true)
        {
            ITimer timer = GetTimer(name);

            if (timer != null)
            {
                if (autoStop)
                    timer.Stop();

                RemoveTimer(timer);
            }
        }

        /// <summary>
        /// Removes the timer.
        /// </summary>
        /// <param name="timer">The timer.</param>
        public void RemoveTimer(ITimer timer)
        {
            if (OwnTimer(timer) && mTimers != null && mTimers.Count > 0)
            {
                foreach (KeyValuePair<string, ITimer> kvp in mTimers)
                {
                    if (kvp.Value.Equals(timer))
                    {
                        mTimers.Remove(kvp.Key);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Removes all timers.
        /// </summary>
        /// <param name="autoStart">if set to <c>true</c> [automatic stop all timers].</param>
        public void RemoveAllTimers(bool autoStop = true)
        {
            if (autoStop)
                StopAllTimers();

            if (mTimers != null)
                mTimers.Clear();
        }

        /// <summary>
        /// Starts all timers.
        /// </summary>
        public void StartAllTimers()
        {
            if (mTimers != null && mTimers.Count > 0)
            {
                foreach (KeyValuePair<string, ITimer> kvp in mTimers)
                {
                    ITimer timer = kvp.Value;
                    timer.Start();
                }
            }
        }

        /// <summary>
        /// Resets all timers.
        /// </summary>
        public void ResetAllTimers()
        {
            if (mTimers != null && mTimers.Count > 0)
            {
                foreach (KeyValuePair<string, ITimer> kvp in mTimers)
                {
                    ITimer timer = kvp.Value;
                    timer.Reset();
                }
            }
        }

        /// <summary>
        /// Stops all timers.
        /// </summary>
        public void StopAllTimers()
        {
            if (mTimers != null && mTimers.Count > 0)
            {
                foreach (KeyValuePair<string, ITimer> kvp in mTimers)
                {
                    ITimer timer = kvp.Value;
                    timer.Stop();
                }
            }
        }

        #endregion API
    }
}