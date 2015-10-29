using UnityEditor;
using UnityEngine;

namespace QuickUnity.Editor.Tools
{
    /// <summary>
    /// AutoSaveEditorWindow help you to set up parameters about saving scene and assets automatically.
    /// </summary>
    public class AutoSaveEditorWindow : EditorWindow
    {
        /// <summary>
        /// The automatic save enabled
        /// </summary>
        private bool mAutoSaveEnabled;

        /// <summary>
        /// Whether saving current scene.
        /// </summary>
        private bool mSaveCurrentSceneEnabled;

        /// <summary>
        /// Whether saving project.
        /// </summary>
        private bool mSaveProjectEnabled;

        /// <summary>
        /// The interval time of automatic save.
        /// </summary>
        private int mAutoSaveInterval = 180;

        /// <summary>
        /// The mark of reseting save time
        /// </summary>
        private bool mResetSaveTime = false;

        /// <summary>
        /// Editor GUI code.
        /// </summary>
        private void OnGUI()
        {
            // Show autosave enabled toggle GUI.
            GUILayout.Space(10);
            GUILayout.BeginVertical();
            mAutoSaveEnabled = EditorGUILayout.ToggleLeft("Enable AutoSave", mAutoSaveEnabled);
            GUILayout.EndVertical();

            EditorGUI.BeginDisabledGroup(!mAutoSaveEnabled);

            // Show toggle GUI of enabling save current scene.
            GUILayout.Space(10);
            GUILayout.BeginVertical();
            mSaveCurrentSceneEnabled = EditorGUILayout.ToggleLeft("Enable save current scene", mSaveCurrentSceneEnabled);
            GUILayout.EndVertical();

            // Show toggle GUI of enabling save project.
            GUILayout.Space(10);
            GUILayout.BeginVertical();
            mSaveProjectEnabled = EditorGUILayout.ToggleLeft("Enable save project", mSaveProjectEnabled);
            GUILayout.EndVertical();

            // Show interval time of automatic save.
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Interval time");
            mAutoSaveInterval = EditorGUILayout.IntField(mAutoSaveInterval, GUILayout.Width(50f));
            GUILayout.Label("seconds");
            GUILayout.Space(150f);
            GUILayout.EndHorizontal();

            EditorGUI.EndDisabledGroup();

            // Whether tell AutoSaveEditorDetector to reset save time.
            if (mAutoSaveInterval != AutoSave.autoSaveInterval)
                mResetSaveTime = true;

            // Save data.
            AutoSave.autoSaveEnabled = mAutoSaveEnabled;
            AutoSave.saveCurrentSceneEnabled = mSaveCurrentSceneEnabled;
            AutoSave.saveProjectEnabled = mSaveProjectEnabled;
            AutoSave.autoSaveInterval = mAutoSaveInterval;
        }

        /// <summary>
        /// Called when [focus].
        /// </summary>
        private void OnFocus()
        {
            mAutoSaveEnabled = AutoSave.autoSaveEnabled;
            mSaveCurrentSceneEnabled = AutoSave.saveCurrentSceneEnabled;
            mSaveProjectEnabled = AutoSave.saveProjectEnabled;
            mAutoSaveInterval = AutoSave.autoSaveInterval;
        }

        /// <summary>
        /// Called when [destroy].
        /// </summary>
        private void OnDestroy()
        {
            if (mResetSaveTime)
                AutoSaveEditorDetector.ResetSaveTime();
        }
    }
}