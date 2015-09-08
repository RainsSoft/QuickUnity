using QuickUnity.Editor.Tools;
using UnityEditor;
using UnityEngine;

namespace QuickUnity.Editor
{
    /// <summary>
    /// This script adds the QuinUnity menu options to the Unity Editor. This class cannot be inherited.
    /// </summary>
    public sealed class QuickUnityMenu
    {
        /// <summary>
        /// The automatic save window
        /// </summary>
        private static AutoSaveWindow mAutoSaveWindow;

        /// <summary>
        /// Gets the automatic save window.
        /// </summary>
        /// <value>The automatic save window.</value>
        public static AutoSaveWindow autoSaveWindow
        {
            get { return mAutoSaveWindow; }
        }

        /// <summary>
        /// Shows AutoSave editor window.
        /// </summary>
        [MenuItem("QuickUnity/Tools/AutoSave")]
        private static void ShowAutoSave()
        {
            if (mAutoSaveWindow == null)
                mAutoSaveWindow = EditorWindow.GetWindowWithRect<AutoSaveWindow>(new Rect(0, 0, 256, 120), true, "AutoSave");

            mAutoSaveWindow.Show();
        }
    }
}