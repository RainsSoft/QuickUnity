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
    }
}