using UnityEditor;

namespace QuickUnity.Editor
{
    /// <summary>
    /// This script adds the QuinUnity/Config menu options to the Unity Editor. This class cannot be inherited.
    /// </summary>
    public sealed class ConfigMenu
    {
        /// <summary>
        /// Generates the JSON format configuration file.
        /// </summary>
        [MenuItem("QuickUnity/Config/Generate JSON Metadata")]
        public static void GenerateJSONConfigFile()
        {
            //string path = EditorUtility.OpenFolderPanel("xlsx files")
        }
    }
}