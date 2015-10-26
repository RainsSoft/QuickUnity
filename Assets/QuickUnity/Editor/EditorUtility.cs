using System.IO;
using UnityEditor;
using UnityEngine;

namespace QuickUnity
{
    /// <summary>
    /// Utility class for Unity Editor.
    /// </summary>
    public static class EditorUtility
    {
        /// <summary>
        /// Script file extensions.
        /// </summary>
        public const string SCRIPT_FILE_EXTENSIONS = ".cs";

        /// <summary>
        /// Gets the name of the project.
        /// </summary>
        /// <value>The name of the project.</value>
        public static string projectName
        {
            get
            {
                string path = Application.dataPath;
                string[] folderNames = path.Split(char.Parse("/"));

                if (folderNames.Length > 2)
                    return folderNames[folderNames.Length - 2];

                return null;
            }
        }

        /// <summary>
        /// Reads the text.
        /// </summary>
        /// <param name="path">The path of text file.</param>
        /// <returns></returns>
        public static string ReadText(string path)
        {
            string text = null;

            if (!string.IsNullOrEmpty(path))
            {
                StreamReader reader = new StreamReader(path, true);
                text = reader.ReadToEnd();
            }

            return text;
        }

        /// <summary>
        /// Writes the text.
        /// </summary>
        /// <param name="path">The path of text file.</param>
        /// <param name="text">The text.</param>
        /// <param name="append">if set to <c>true</c> [append string to the text file].</param>
        public static void WriteText(string path, string text, bool append = false)
        {
            if (!string.IsNullOrEmpty(path) || !string.IsNullOrEmpty(text))
            {
                StreamWriter writer = new StreamWriter(path, append);
                writer.Write(text);
                writer.Flush();
                writer.Close();
                writer = null;
            }
        }

        /// <summary>
        /// Reads the text asset.
        /// </summary>
        /// <param name="path">The path of text asset.</param>
        /// <returns></returns>
        public static string ReadTextAsset(string path)
        {
            TextAsset asset = AssetDatabase.LoadMainAssetAtPath(path) as TextAsset;

            if (asset)
                return asset.text;

            return null;
        }
    }
}