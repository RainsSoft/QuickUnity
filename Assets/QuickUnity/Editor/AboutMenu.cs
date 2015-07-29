using QuickUnity.Editor.About;
using UnityEditor;
using UnityEngine;

namespace QuickUnity.Editor
{
    /// <summary>
    /// This script adds the QuinUnity/About menu options to the Unity Editor. This class cannot be inherited.
    /// </summary>
    public sealed class AboutMenu
    {
        /// <summary>
        /// The about information window.
        /// </summary>
        private static AboutWindow mAboutWindow;

        /// <summary>
        /// Gets the about information window.
        /// </summary>
        /// <value>
        /// The about window.
        /// </value>
        public AboutWindow aboutWindow
        {
            get { return mAboutWindow; }
        }

        /// <summary>
        /// Shows the about.
        /// </summary>
        [MenuItem("QuickUnity/About")]
        public static void ShowAbout()
        {
            if (mAboutWindow == null)
                mAboutWindow = EditorWindow.GetWindowWithRect<AboutWindow>(new Rect(0, 0, 256, 120), true, "About");

            mAboutWindow.Show();
        }
    }
}