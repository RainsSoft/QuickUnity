using QuickUnity.Editor.Tools;
using UnityEditor;
using UnityEngine;

namespace QuickUnity.Editor
{
    /// <summary>
    /// This script adds the QuinUnity/Tools menu options to the Unity Editor. This class cannot be inherited.
    /// </summary>
    public static class ToolsMenu
    {
        /// <summary>
        /// The automatic save editor window
        /// </summary>
        private static AutoSaveEditorWindow mAutoSaveEditorWindow;

        /// <summary>
        /// Gets the automatic save editor window.
        /// </summary>
        /// <value>The automatic save editor window.</value>
        public static AutoSaveEditorWindow autoSaveEditorWindow
        {
            get { return mAutoSaveEditorWindow; }
        }

        /// <summary>
        /// Shows AutoSave editor window.
        /// </summary>
        [MenuItem("QuickUnity/Tools/AutoSave")]
        private static void ShowAutoSaveEditorWindow()
        {
            if (mAutoSaveEditorWindow == null)
                mAutoSaveEditorWindow = EditorWindow.GetWindowWithRect<AutoSaveEditorWindow>(new Rect(0, 0, 256, 120), true, "AutoSave Editor");

            mAutoSaveEditorWindow.Show();
        }
    }
}