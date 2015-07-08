using QuickUnity.Timers;
using QuickUnity.Utilitys;
using System.Collections;
using UnityEngine;

namespace QuickUnity.Examples.Timer
{
    /// <summary>
    /// Class TimerExample.
    /// </summary>
    public class TimerExample : MonoBehaviour
    {
        // Use this for initialization
        private void Start()
        {
            TimerManager timerManager = TimerManager.instance;
            timerManager.Start();
            timerManager.OnTimer += OnGlobalTimer;

            QuickUnity.Timers.Timer timer1 = new QuickUnity.Timers.Timer(0.6f);
            timer1.AddEventListener(TimerEvent.TIMER, OnTimerHandler);
            timerManager.AddTimer("test1", timer1);

            QuickUnity.Timers.Timer timer2 = new QuickUnity.Timers.Timer(2.5f, 10);
            timer2.AddEventListener(TimerEvent.TIMER_COMPLETE, OnTimerCompleteHandler);
            timerManager.AddTimer("test2", timer2);
        }

        /// <summary>
        /// Called when [global timer].
        /// </summary>
        /// <param name="deltaTime">The delta time.</param>
        private void OnGlobalTimer(float deltaTime)
        {
            TimerManager timerManager = TimerManager.instance;
            Debug.Log("global timer count: " + timerManager.currentCount + ", delta time: " + deltaTime);
        }

        /// <summary>
        /// Called when [timer handler].
        /// </summary>
        /// <param name="eventObj">The event object.</param>
        private void OnTimerHandler(Events.Event eventObj)
        {
            TimerEvent timerEvent = eventObj as TimerEvent;
            QuickUnity.Timers.ITimer timer = timerEvent.timer;
            float deltaTime = timerEvent.deltaTime;

            Debug.Log("timer1 count: " + timer.currentCount + ", delta time: " + deltaTime);
        }

        /// <summary>
        /// Called when [timer complete handler].
        /// </summary>
        /// <param name="eventObj">The event object.</param>
        private void OnTimerCompleteHandler(Events.Event eventObj)
        {
            TimerEvent timerEvent = eventObj as TimerEvent;
            QuickUnity.Timers.ITimer timer = timerEvent.timer;
            float deltaTime = timerEvent.deltaTime;
            Debug.Log("timer2 count: " + timer.currentCount + ", delta time: " + deltaTime);
            TimerManager timerManager = TimerManager.instance;
            timerManager.RemoveTimer("test1");
            timerManager.RemoveTimer("test2");
        }
    }
}