using QuickUnity.Editor.Config;
using UnityEditor;
using UnityEngine;

namespace QuickUnity.Editor
{
    /// <summary>
    /// This script adds the QuinUnity/Config menu options to the Unity Editor. This class cannot be inherited.
    /// </summary>
    public static class ConfigMenu
    {
        /// <summary>
        /// The configuration editor window object.
        /// </summary>
        private static ConfigEditorWindow mConfigEditorWindow;

        /// <summary>
        /// Gets the configuration editor window.
        /// </summary>
        /// <value>
        /// The configuration editor window.
        /// </value>
        public static ConfigEditorWindow configEditorWindow
        {
            get { return mConfigEditorWindow; }
        }

        /// <summary>
        /// Generates the configuration metadata files.
        /// </summary>
        [MenuItem("QuickUnity/Config/Generate Configuration Metadata")]
        public static void GenerateConfigMetadata()
        {
            if (mConfigEditorWindow == null)
                mConfigEditorWindow = EditorWindow.GetWindowWithRect<ConfigEditorWindow>(new Rect(0, 0, 580, 220),
                    true,
                    "Configuration Metadata Editor");

            mConfigEditorWindow.Show();
        }
    }
}