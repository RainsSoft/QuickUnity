using System.Collections;
using UnityEditor;
using UnityEngine;

/// <summary>
/// The Config namespace.
/// </summary>
namespace QuickUnityEditor.Config
{
    /// <summary>
    /// Class ExcelConfigEditor to edit Excel configuration files.
    /// </summary>
    public class ExcelConfigEditor : Editor
    {
        /// <summary>
        /// Generate the excel configuration data.
        /// </summary>
        [MenuItem("QuickUnity/Config/Generate Excel configuration data")]
        public static void GenExcelConfigData()
        {
            string path = EditorUtility.OpenFolderPanel("Load Excel configuration files of Directory", "Assets", "");
        }
    }
}