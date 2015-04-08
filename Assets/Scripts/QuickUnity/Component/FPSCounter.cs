﻿using System.Collections;
using UnityEngine;

/// <summary>
/// The Component namespace.
/// </summary>
namespace QuickUnity.Component
{
    /// <summary>
    /// The FPS counter tool.
    /// </summary>
    [AddComponentMenu("QuickUnity/Tools/FPS Counter")]
    [RequireComponent(typeof(GUIText))]
    public class FPSCounter : MonoBehaviour
    {
        /// <summary>
        /// The frequency of counter running.
        /// </summary>
        public float Frequency = 0.5f;

        // Use this for initialization
        private void Start()
        {
            Application.targetFrameRate = 60;
            DisplayFPS(60);
            StartCoroutine(CalculateFPS());
        }

        /// <summary>
        /// Calculate the FPS number.
        /// </summary>
        /// <returns>IEnumerator.</returns>
        private IEnumerator CalculateFPS()
        {
            for (; ; )
            {
                // Capture frame percent second.
                int lastFrameCount = Time.frameCount;
                float lastTime = Time.realtimeSinceStartup;
                yield return new WaitForSeconds(Frequency);

                float timeSpan = Time.realtimeSinceStartup - lastTime;
                int frameCount = Time.frameCount - lastFrameCount;

                // Display it.
                int fps = Mathf.RoundToInt(frameCount / timeSpan);
                DisplayFPS(fps);
            }
        }

        /// <summary>
        /// Displays the FPS number.
        /// </summary>
        /// <param name="fps">The FPS.</param>
        private void DisplayFPS(int fps)
        {
            gameObject.guiText.text = fps.ToString() + " FPS";
        }
    }
}