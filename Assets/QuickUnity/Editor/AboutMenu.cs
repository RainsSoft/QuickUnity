using UnityEditor;

namespace QuickUnity.Editor
{
    /// <summary>
    /// This script adds the QuinUnity/About menu options to the Unity Editor. This class cannot be inherited.
    /// </summary>
    public static class AboutMenu
    {
        /// <summary>
        /// Shows the about.
        /// </summary>
        [MenuItem("QuickUnity/About")]
        public static void ShowAbout()
        {
            UnityEditor.EditorUtility.DisplayDialog("About",
                "QuickUnity framework\nAuthor: Jerry Lee\nE-mail: cosmos53076@163.com",
                "OK");
        }
    }
}