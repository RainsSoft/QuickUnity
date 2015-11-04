using UnityEngine;

namespace QuickUnity
{
    /// <summary>
    /// The environment of platform.
    /// </summary>
    public static class Environment
    {
        /// <summary>
        /// The new line of Unix operation system.
        /// </summary>
        public const string UNIX_NEW_LINE = "\n";

        /// <summary>
        /// The new line of Windows operation system.
        /// </summary>
        public const string WINDOWS_NEW_LINE = "\r\n";

        /// <summary>
        /// Gets the new line.
        /// </summary>
        /// <value>
        /// The new line.
        /// </value>
        public static string newLine
        {
            get
            {
                string result = string.Empty;

                switch (Application.platform)
                {
                    case RuntimePlatform.WindowsEditor:
                        result = WINDOWS_NEW_LINE;
                        break;

                    case RuntimePlatform.OSXEditor:
                    default:
                        result = UNIX_NEW_LINE;
                        break;
                }

                return result;
            }
        }

        /// <summary>
        /// Gets the streaming asset path by platform.
        /// </summary>
        /// <param name="assetPath">The asset path.</param>
        /// <returns>System.String.</returns>
        public static string GetStreamingAssetPath(string assetPath)
        {
            string path = "";

            switch (Application.platform)
            {
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.OSXEditor:
                default:
                    // Windows Editor or Mac OS X Editor
                    path = "file://" + Application.dataPath + "/StreamingAssets" + assetPath;
                    break;

                case RuntimePlatform.Android:
                    // Android
                    path = "jar:file://" + Application.dataPath + "!/assets" + assetPath;
                    break;

                case RuntimePlatform.IPhonePlayer:
                    // iOS
                    path = "file://" + Application.dataPath + "/Raw" + assetPath;
                    break;
            }

            return path;
        }
    }
}