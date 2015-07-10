using UnityEditor;
using UnityEngine;

namespace QuickUnity.Editor.Tools
{
    /// <summary>
    /// Detector of auto-save.
    /// </summary>
    [InitializeOnLoad]
    public static class AutoSaveDetector
    {
        /// <summary>
        /// The next save time.
        /// </summary>
        private static double mNextSaveTime = 0.0d;

        /// <summary>
        /// Initializes static members of the <see cref="AutoSaveDetector"/> class.
        /// </summary>
        static AutoSaveDetector()
        {
            mNextSaveTime = EditorApplication.timeSinceStartup;
            EditorApplication.update += OnUpdate;
        }

        /// <summary>
        /// Resets the save time.
        /// </summary>
        public static void ResetSaveTime()
        {
            mNextSaveTime = EditorApplication.timeSinceStartup;
        }

        /// <summary>
        /// Called when [ editor application update].
        /// </summary>
        private static void OnUpdate()
        {
            if (AutoSave.autoSaveEnabled)
            {
                if (EditorApplication.timeSinceStartup >= mNextSaveTime)
                {
                    // Automatically save current scene.
                    if (AutoSave.saveCurrentSceneEnabled && !string.IsNullOrEmpty(EditorApplication.currentScene))
                        EditorApplication.SaveScene(EditorApplication.currentScene);

                    // Automatically save project.
                    if (AutoSave.saveProjectEnabled)
                        EditorApplication.SaveAssets();

                    mNextSaveTime = EditorApplication.timeSinceStartup + AutoSave.autoSaveInterval;
                }
            }
        }
    }
}