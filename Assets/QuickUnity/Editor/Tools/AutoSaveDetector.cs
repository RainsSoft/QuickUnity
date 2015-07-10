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
        /// Called when [ editor application update].
        /// </summary>
        private static void OnUpdate()
        {
            if (EditorApplication.timeSinceStartup >= mNextSaveTime)
            {
                if (AutoSave.autoSaveEnabled)
                {
                    // Automatically save project.
                    if (AutoSave.saveProjectEnabled)
                        EditorApplication.SaveAssets();
                }

                mNextSaveTime = EditorApplication.timeSinceStartup + AutoSave.autoSaveInterval;
            }
        }
    }
}