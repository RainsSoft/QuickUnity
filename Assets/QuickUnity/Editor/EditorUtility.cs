﻿using QuickUnity.Utilitys;
using System;
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
            int index = absolutePath.IndexOf("Assets/");

            if (index != -1)
                return absolutePath.Substring(index);

            return absolutePath;
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
        /// Moves all assets in directory.
        /// </summary>
        /// <param name="dirPath">The directory path.</param>
        /// <param name="destDirPath">The destination directory path.</param>
        public static void MoveAllAssetsInDirectory(string dirPath, string destDirPath)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
            FileInfo[] fileInfos = dirInfo.GetFiles();

            foreach (FileInfo fileInfo in fileInfos)
            {
                string destFilePath = destDirPath + fileInfo.Name;

                if (File.Exists(destFilePath))
                    DeleteAsset(destFilePath);

                string relativeOldPath = ConvertToRelativePath(fileInfo.FullName);
                string relativeNewPath = ConvertToRelativePath(destFilePath);
                string errorMessage = AssetDatabase.MoveAsset(relativeOldPath, relativeNewPath);

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    // Operation failed.
                    try
                    {
                        fileInfo.MoveTo(destFilePath);
                    }
                    catch (Exception e)
                    {
                        Debug.LogWarningFormat("Move file [sourcePath={0}, destPath={1}] got error message: {2}, stack trace: {3}",
                            fileInfo.FullName, destFilePath, e.Message, e.StackTrace);
                    }
                }
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
                DeleteAsset(filePath);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        }

        /// <summary>
        /// Deletes the asset.
        /// </summary>
        /// <param name="assetPath">The asset file path.</param>
        public static void DeleteAsset(string filePath)
        {
            string relativePath = ConvertToRelativePath(filePath);
            bool success = AssetDatabase.DeleteAsset(relativePath);

            if (!success)
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(filePath);
                    fileInfo.Delete();
                }
                catch (Exception e)
                {
                    Debug.LogWarningFormat("Delete file [{0}] got error message: {1}, stack trace: {2}", filePath, e.Message, e.StackTrace);
                }
            }
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

        /// <summary>
        /// Clears messages of console.
        /// </summary>
        public static void ClearConsole()
        {
            Type type = Type.GetType("UnityEditorInternal.LogEntries,UnityEditor.dll");
            ReflectionUtility.InvokeStaticMethod(type, "Clear");
        }

        #endregion Public Static Functions
    }
}