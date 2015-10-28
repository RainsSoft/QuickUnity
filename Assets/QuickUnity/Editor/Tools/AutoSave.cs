using UnityEditor;

namespace QuickUnity.Editor.Tools
{
    /// <summary>
    /// AutoSave parameters. This class cannot be inherited.
    /// </summary>
    public static class AutoSave
    {
        /// <summary>
        /// Gets or sets a value indicating whether [automatic save enabled].
        /// </summary>
        /// <value><c>true</c> if [automatic save enabled]; otherwise, <c>false</c>.</value>
        public static bool autoSaveEnabled
        {
            get { return EditorPrefs.GetBool(EditorUtility.projectName + ".AutoSave.autoSaveEnabled"); }
            set { EditorPrefs.SetBool(EditorUtility.projectName + ".AutoSave.autoSaveEnabled", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [save current scene enabled].
        /// </summary>
        /// <value><c>true</c> if [save current scene enabled]; otherwise, <c>false</c>.</value>
        public static bool saveCurrentSceneEnabled
        {
            get { return EditorPrefs.GetBool(EditorUtility.projectName + ".AutoSave.saveCurrentSceneEnabled"); }
            set { EditorPrefs.SetBool(EditorUtility.projectName + ".AutoSave.saveCurrentSceneEnabled", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [save project enabled].
        /// </summary>
        /// <value><c>true</c> if [save project enabled]; otherwise, <c>false</c>.</value>
        public static bool saveProjectEnabled
        {
            get { return EditorPrefs.GetBool(EditorUtility.projectName + ".AutoSave.saveProjectEnabled"); }
            set { EditorPrefs.SetBool(EditorUtility.projectName + ".AutoSave.saveProjectEnabled", value); }
        }

        /// <summary>
        /// Gets or sets the automatic save interval time.
        /// </summary>
        /// <value>The automatic save interval.</value>
        public static int autoSaveInterval
        {
            get { return EditorPrefs.GetInt(EditorUtility.projectName + ".AutoSave.autoSaveInterval"); }
            set { EditorPrefs.SetInt(EditorUtility.projectName + ".AutoSave.autoSaveInterval", value); }
        }
    }
}