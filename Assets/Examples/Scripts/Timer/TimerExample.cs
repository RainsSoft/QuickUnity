using QuickUnity.Components;
using QuickUnity.Events;
using QuickUnity.Utilitys;
using System.Collections;
using UnityEngine;

/// <summary>
/// The Timer namespace.
/// </summary>
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
            TimerManager timerManager = TimerManager.Instance;
            timerManager.Delay = 0.6f;
            timerManager.Start();
            timerManager.OnTimer += OnGlobalTimer;

            QuickUnity.Components.Timer timer = new QuickUnity.Components.Timer(1.0f);
            timer.AddEventListener(TimerEvent.TIMER, OnTimerHandler);
            timerManager.AddTimer("test1", timer);

            //QuickUnity.Components.Timer timer = new QuickUnity.Components.Timer(1.0f);
            //TimerManager timerManager = TimerManager.Instance;
            //timerManager.OnTimer += OnTimer;
            //timerManager.AddTimer("test", timer);
        }

        /// <summary>
        /// Called when [global timer].
        /// </summary>
        /// <param name="deltaTime">The delta time.</param>
        private void OnGlobalTimer(float deltaTime)
        {
            TimerManager timerManager = TimerManager.Instance;
            Debug.Log("global timer count: " + timerManager.CurrentCount + ", delta time: " + deltaTime);
        }

        /// <summary>
        /// Called when [timer handler].
        /// </summary>
        /// <param name="eventObj">The event object.</param>
        private void OnTimerHandler(Events.Event eventObj)
        {
            TimerEvent timerEvent = eventObj as TimerEvent;
            QuickUnity.Components.Timer timer = timerEvent.Timer;
            float deltaTime = timerEvent.DeltaTime;

            Debug.Log("timer count: " + timer.CurrentCount + ", delta time: " + deltaTime);
        }
    }
}