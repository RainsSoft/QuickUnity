using UnityEngine;

namespace QuickUnity.Utilitys
{
    /// <summary>
    /// A utility class for doing something about path. This class cannot be inherited.
    /// </summary>
    public static class PathUtility
    {
        /// <summary>
        /// Gets the streaming asset path.
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