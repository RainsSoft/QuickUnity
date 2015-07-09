﻿using QuickUnity.Events;
using QuickUnity.Patterns;
using System;
using System.Collections;
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
        /// Awake this script.
        /// </summary>
        private void Awake()
        {
            mTimers = new Dictionary<string, ITimer>();
        }

        /// <summary>
        /// Update in fixed time.
        /// </summary>
        private void FixedUpdate()
        {
            if (enabled)
            {
                float deltaTime = Time.fixedDeltaTime;

                foreach (KeyValuePair<string, ITimer> kvp in mTimers)
                {
                    ITimer timer = kvp.Value;
                    timer.Tick(deltaTime);
                }
            }
        }

        /// <summary>
        /// This function is called when the MonoBehaviour will be destroyed.
        /// </summary>
        private void OnDestroy()
        {
            RemoveAllTimers();
            mTimers = null;
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
            if (OwnTimer(timer))
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
                StopAllTimers(false);

            mTimers.Clear();
        }

        /// <summary>
        /// Starts all timers.
        /// </summary>
        /// <param name="includeGlobalTimer">if set to <c>true</c> [include global timer].</param>
        public void StartAllTimers(bool includeGlobalTimer = true)
        {
            foreach (KeyValuePair<string, ITimer> kvp in mTimers)
            {
                ITimer timer = kvp.Value;
                timer.Start();
            }
        }

        /// <summary>
        /// Resets all timers.
        /// </summary>
        /// <param name="includeGloablTimer">if set to <c>true</c> [include gloabl timer].</param>
        public void ResetAllTimers(bool includeGlobalTimer = true)
        {
            foreach (KeyValuePair<string, ITimer> kvp in mTimers)
            {
                ITimer timer = kvp.Value;
                timer.Reset();
            }
        }

        /// <summary>
        /// Stops all timers.
        /// </summary>
        /// <param name="includeGlobalTimer">if set to <c>true</c> [include global timer].</param>
        public void StopAllTimers(bool includeGlobalTimer = true)
        {
            foreach (KeyValuePair<string, ITimer> kvp in mTimers)
            {
                ITimer timer = kvp.Value;
                timer.Stop();
            }
        }
    }
}