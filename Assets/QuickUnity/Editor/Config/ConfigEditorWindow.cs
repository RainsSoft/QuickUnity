using UnityEditor;
using UnityEngine;

namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The configuration editor window.
    /// </summary>
    public class ConfigEditorWindow : EditorWindow
    {
        /// <summary>
        /// The primary key.
        /// </summary>
        private string mPrimaryKey;

        /// <summary>
        /// The metadata namespace.
        /// </summary>
        private string mMetadataNamespace;

        /// <summary>
        /// The excel files path.
        /// </summary>
        private string mExcelFilesPath;

        /// <summary>
        /// The script files path.
        /// </summary>
        private string mScriptFilesPath;

        /// <summary>
        /// The database files path.
        /// </summary>
        private string mDatabaseFilesPath;

        #region Messages

        /// <summary>
        /// Called when [GUI].
        /// </summary>
        private void OnGUI()
        {
            // Get data.
            mPrimaryKey = ConfigEditor.primaryKey;
            mMetadataNamespace = ConfigEditor.metadataNamespace;
            mExcelFilesPath = ConfigEditor.excelFilesPath;
            mScriptFilesPath = ConfigEditor.scriptFilesPath;
            mDatabaseFilesPath = ConfigEditor.databaseFilesPath;

            // Primary key set.
            GUILayout.BeginVertical();
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(50);
            GUILayout.Label("Primary  Key: ");
            GUILayout.Space(10);
            mPrimaryKey = EditorGUILayout.TextField(mPrimaryKey, GUILayout.Width(100f));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            // Namespace.
            GUILayout.BeginVertical();
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Metadata Namespace: ");
            GUILayout.Space(10);
            mMetadataNamespace = EditorGUILayout.TextField(mMetadataNamespace, GUILayout.Width(200f));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            // Excel files path set.
            GUILayout.BeginVertical();
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(38);
            GUILayout.Label("Excel Files Path: ");
            EditorGUI.BeginDisabledGroup(true);
            mExcelFilesPath = EditorGUILayout.TextField(mExcelFilesPath, GUILayout.Width(350f));
            EditorGUI.EndDisabledGroup();
            GUILayout.Space(10f);
            if (GUILayout.Button("Browse"))
            {
                ConfigEditor.excelFilesPath = UnityEditor.EditorUtility.OpenFolderPanel("Load excel files of Directory", "", "");
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            // Script files path set.
            GUILayout.BeginVertical();
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(35);
            GUILayout.Label("Script Files Path: ");
            EditorGUI.BeginDisabledGroup(true);
            mScriptFilesPath = EditorGUILayout.TextField(mScriptFilesPath, GUILayout.Width(350f));
            EditorGUI.EndDisabledGroup();
            GUILayout.Space(10f);
            if (GUILayout.Button("Browse"))
            {
                ConfigEditor.scriptFilesPath = UnityEditor.EditorUtility.OpenFolderPanel("VO script files of Directory", "Assets/Scripts", "");
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            // Database files path set.
            GUILayout.BeginVertical();
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(14);
            GUILayout.Label("Database Files Path: ");
            EditorGUI.BeginDisabledGroup(true);
            mDatabaseFilesPath = EditorGUILayout.TextField(mDatabaseFilesPath, GUILayout.Width(350f));
            EditorGUI.EndDisabledGroup();
            GUILayout.Space(10f);
            if (GUILayout.Button("Browse"))
            {
                ConfigEditor.databaseFilesPath = UnityEditor.EditorUtility.OpenFolderPanel("Database files of Directory You Want to Save", "Assets", "");
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            // Button bar.
            GUILayout.BeginVertical();
            GUILayout.Space(10);
            GUILayout.BeginHorizontal(GUILayout.Width(300));
            GUILayout.Space(150);
            if (GUILayout.Button("Clear All Paths Set"))
            {
                ConfigEditor.excelFilesPath = string.Empty;
                ConfigEditor.scriptFilesPath = string.Empty;
                ConfigEditor.databaseFilesPath = string.Empty;
            }
            GUILayout.Space(30);
            if (GUILayout.Button("Generate Metadata"))
            {
                ConfigEditor.GenerateConfigMetadata();
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            // Save data.
            ConfigEditor.primaryKey = mPrimaryKey;
            ConfigEditor.metadataNamespace = mMetadataNamespace;
        }

        #endregion Messages
    }
}