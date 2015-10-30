using System.IO;
using System.Threading;
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
        /// Gets the root directory name of project.
        /// </summary>
        /// <value>The root directory name of project.</value>
        public static string projectRootDirName
        {
            get
            {
                DirectoryInfo dirInfo = new DirectoryInfo(Application.dataPath);
                return dirInfo.Parent.Name;
            }
        }

        /// <summary>
        /// Gets the project path.
        /// </summary>
        /// <value>
        /// The project path.
        /// </value>
        public static string projectPath
        {
            get
            {
                DirectoryInfo dirInfo = new DirectoryInfo(Application.dataPath);
                return dirInfo.Parent.FullName;
            }
        }

        #region Public Static Functions

        /// <summary>
        /// Converts absolute path to relative path.
        /// </summary>
        /// <param name="absolutePath">The absolute path.</param>
        /// <returns>System.String.</returns>
        public static string ConvertToRelativePath(string absolutePath)
        {
            return absolutePath.Substring(absolutePath.IndexOf("Assets/"));
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
                reader.Close();
                reader = null;
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

        /// <summary>
        /// Moves all files in directory.
        /// </summary>
        /// <param name="dirPath">The directory path.</param>
        /// <param name="destDirPath">The destination directory path.</param>
        public static void MoveAllFilesInDirectory(string dirPath, string destDirPath)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
            FileInfo[] fileInfos = dirInfo.GetFiles();

            foreach (FileInfo fileInfo in fileInfos)
            {
                string destFilePath = destDirPath + fileInfo.Name;

                if (File.Exists(destFilePath))
                    File.Delete(destFilePath);

                fileInfo.MoveTo(destFilePath);
            }
        }

        /// <summary>
        /// Deletes all files in directory.
        /// </summary>
        /// <param name="dirPath">The directory path.</param>
        public static void DeleteAllFilesInDirectory(string dirPath)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
            FileInfo[] fileInfos = dirInfo.GetFiles();

            foreach (FileInfo fileInfo in fileInfos)
            {
                fileInfo.Delete();
            }
        }

        /// <summary>
        /// Deletes all assets in directory.
        /// </summary>
        /// <param name="dirPath">The directory path.</param>
        public static void DeleteAllAssetsInDirectory(string dirPath)
        {
            string[] filePaths = Directory.GetFiles(dirPath);

            foreach (string filePath in filePaths)
            {
                string relativePath = ConvertToRelativePath(filePath);
                AssetDatabase.DeleteAsset(relativePath);
            }

            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        }

        /// <summary>
        /// Waits the editor processing.
        /// </summary>
        public static void WaitEditorProcessing()
        {
            // Refresh.
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        #endregion Public Static Functions
    }
}