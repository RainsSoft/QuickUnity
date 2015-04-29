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
            QuickUnity.Components.Timer timer = new QuickUnity.Components.Timer("test", 1.0f);
            TimerManager timerManager = TimerManager.Instance;
            timerManager.OnTimer += OnTimer;
            timerManager.AddTimer(timer);
        }

        /// <summary>
        /// Called when [timer].
        /// </summary>
        /// <param name="timer">The timer.</param>
        private void OnTimer(QuickUnity.Components.Timer timer)
        {
            Debug.Log("Timer count: " + timer.CurrentCount);
        }
    }
}