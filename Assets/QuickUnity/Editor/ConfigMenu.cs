using QuickUnity.Editor.Config;
using UnityEditor;

namespace QuickUnity.Editor
{
    /// <summary>
    /// This script adds the QuinUnity/Config menu options to the Unity Editor. This class cannot be inherited.
    /// </summary>
    public sealed class ConfigMenu
    {
        /// <summary>
        /// Generates the configuration metadata files.
        /// </summary>
        [MenuItem("QuickUnity/Config/Generate Configuration Metadata")]
        public static void GenerateConfigMetadata()
        {
            string excelFilesPath = UnityEditor.EditorUtility.OpenFolderPanel("Load configuration files of Directory", "", "");

            if (!string.IsNullOrEmpty(excelFilesPath))
            {
                string scriptFilesPath = UnityEditor.EditorUtility.OpenFolderPanel("VO script files of Directory", "Assets/Scripts", "");

                if (!string.IsNullOrEmpty(scriptFilesPath))
                {
                    string databasePath = UnityEditor.EditorUtility.OpenFolderPanel("Database of Directory You Want to Save", "Assets", "");

                    if (!string.IsNullOrEmpty(databasePath))
                    {
                        ConfigEditor.GenerateConfigMetadata(excelFilesPath, scriptFilesPath, databasePath);
                    }
                }
            }
        }
    }
}