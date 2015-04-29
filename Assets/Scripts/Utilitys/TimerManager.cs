using QuickUnity.Components;
using QuickUnity.DesignPattern;
using QuickUnity.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Utilitys namespace.
/// </summary>
namespace QuickUnity.Utilitys
{
    /// <summary>
    /// Hold and manage all timer objects.
    /// </summary>
    public class TimerManager : BehaviourSingleton<TimerManager>
    {
        /// <summary>
        /// Delegate OnGlobalTimerDelegate
        /// </summary>
        public delegate void OnGlobalTimerDelegate();

        /// <summary>
        /// Occurs when [on global timer].
        /// </summary>
        public event OnGlobalTimerDelegate OnGlobalTimer;

        /// <summary>
        /// Delegate OnTimerDelegate
        /// </summary>
        /// <param name="time">The time.</param>
        public delegate void OnTimerDelegate(Timer time);

        /// <summary>
        /// Occurs when [on timer].
        /// </summary>
        public event OnTimerDelegate OnTimer;

        /// <summary>
        /// Delegate OnTimerCompleteDelegate
        /// </summary>
        /// <param name="timer">The timer.</param>
        public delegate void OnTimerCompleteDelegate(Timer timer);

        /// <summary>
        /// Occurs when [on timer complete].
        /// </summary>
        public event OnTimerCompleteDelegate OnTimerComplete;

        /// <summary>
        /// The timer dictionary.
        /// </summary>
        private Dictionary<string, Timer> mTimers;

        /// <summary>
        /// Awake this script.
        /// </summary>
        private void Awake()
        {
            mTimers = new Dictionary<string, Timer>();
        }

        /// <summary>
        /// Update in fixed time.
        /// </summary>
        private void FixedUpdate()
        {
            Delegate[] delegates = OnGlobalTimer.GetInvocationList();

            if (delegates.Length > 0)
                OnGlobalTimer();

            foreach (KeyValuePair<string, Timer> kvp in mTimers)
            {
                Timer timer = kvp.Value;
                timer.Update(Time.deltaTime);
            }
        }

        /// <summary>
        /// Gets the timer.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Timer.</returns>
        public Timer GetTimer(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            if (mTimers.ContainsKey(name))
                return mTimers[name];

            return null;
        }

        /// <summary>
        /// Adds the timer object.
        /// </summary>
        /// <param name="timer">The timer.</param>
        /// <param name="autoStart">if set to <c>true</c> [automatic start timer].</param>
        public void AddTimer(Timer timer, bool autoStart = true)
        {
            if (string.IsNullOrEmpty(timer.Name) || GetTimer(timer.Name) != null)
                return;

            mTimers.Add(timer.Name, timer);
            timer.AddEventListener(TimerEvent.TIMER, TimerHandler);
            timer.AddEventListener(TimerEvent.TIMER_COMPLETE, TimerCompleteHandler);

            if (autoStart)
                timer.Start();
        }

        /// <summary>
        /// Removes the timer by timer name.
        /// </summary>
        /// <param name="name">The name of timer.</param>
        public void RemoveTimer(string name)
        {
            Timer timer = GetTimer(name);
            RemoveTimer(timer);
        }

        /// <summary>
        /// Removes the timer.
        /// </summary>
        /// <param name="timer">The timer.</param>
        public void RemoveTimer(Timer timer)
        {
            if (string.IsNullOrEmpty(timer.Name) || GetTimer(timer.Name) != null)
                return;

            mTimers.Remove(timer.Name);
            timer.RemoveEventListener(TimerEvent.TIMER, TimerHandler);
            timer.RemoveEventListener(TimerEvent.TIMER_COMPLETE, TimerCompleteHandler);
        }

        /// <summary>
        /// Removes all timers.
        /// </summary>
        public void RemoveAllTimers()
        {
            foreach (KeyValuePair<string, Timer> kvp in mTimers)
            {
                Timer timer = kvp.Value;
                timer.RemoveEventListener(TimerEvent.TIMER, TimerHandler);
                timer.RemoveEventListener(TimerEvent.TIMER_COMPLETE, TimerCompleteHandler);
            }

            mTimers.Clear();
        }

        /// <summary>
        /// The timer timer event handler.
        /// </summary>
        /// <param name="eventObj">The event object.</param>
        private void TimerHandler(Events.Event eventObj)
        {
            TimerEvent timerEvent = eventObj as TimerEvent;
            Timer timer = timerEvent.Timer;

            Delegate[] delegates = OnTimer.GetInvocationList();

            if (delegates.Length > 0)
                OnTimer(timer);
        }

        /// <summary>
        /// The timer complete event handler.
        /// </summary>
        /// <param name="eventObj">The event object.</param>
        private void TimerCompleteHandler(Events.Event eventObj)
        {
            TimerEvent timerEvent = eventObj as TimerEvent;
            Timer timer = timerEvent.Timer;

            Delegate[] delegates = OnTimerComplete.GetInvocationList();

            if (delegates.Length > 0)
                OnTimerComplete(timer);
        }
    }
}