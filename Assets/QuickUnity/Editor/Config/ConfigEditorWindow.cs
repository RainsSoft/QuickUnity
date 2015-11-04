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
        /// The debug mode.
        /// </summary>
        private bool mDebugMode;

        /// <summary>
        /// The primary key.
        /// </summary>
        private string mPrimaryKey;

        /// <summary>
        /// The key row index.
        /// </summary>
        private int mKeyRowIndex;

        /// <summary>
        /// The type row index.
        /// </summary>
        private int mTypeRowIndex;

        /// <summary>
        /// The comments row index.
        /// </summary>
        private int mCommentsRowIndex;

        /// <summary>
        /// The data start row index.
        /// </summary>
        private int mDataStartRowIndex;

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
        /// The database cache files path.
        /// </summary>
        private string mDBCacheFilesPath;

        /// <summary>
        /// The database files path.
        /// </summary>
        private string mDBFilesPath;

        #region Messages

        /// <summary>
        /// Called when [GUI].
        /// </summary>
        private void OnGUI()
        {
            // Get data.
            mDebugMode = ConfigEditor.debugMode;
            mPrimaryKey = ConfigEditor.primaryKey;
            mKeyRowIndex = ConfigEditor.keyRowIndex;
            mTypeRowIndex = ConfigEditor.typeRowIndex;
            mCommentsRowIndex = ConfigEditor.commentsRowIndex;
            mDataStartRowIndex = ConfigEditor.dataStartRowIndex;
            mMetadataNamespace = ConfigEditor.metadataNamespace;
            mExcelFilesPath = ConfigEditor.excelFilesPath;
            mScriptFilesPath = ConfigEditor.scriptFilesPath;
            mDBCacheFilesPath = ConfigEditor.databaseCacheFilesPath;
            mDBFilesPath = ConfigEditor.databaseFilesPath;

            // Debug mode.
            GUILayout.BeginVertical();
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(58);
            GUILayout.Label("Debug Mode: ");
            GUILayout.Space(7);
            mDebugMode = EditorGUILayout.Toggle(mDebugMode, GUILayout.Width(100f));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            // Primary key set.
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(54);
            GUILayout.Label("Primary  Key: ");
            GUILayout.Space(7);
            mPrimaryKey = EditorGUILayout.TextField(mPrimaryKey, GUILayout.Width(100f));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            // Key row index set.
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(42);
            GUILayout.Label("Key Row Index: ");
            GUILayout.Space(7);
            mKeyRowIndex = EditorGUILayout.IntField(mKeyRowIndex, GUILayout.Width(100f));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            // Type row index set.
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(36);
            GUILayout.Label("Type Row Index: ");
            GUILayout.Space(7);
            mTypeRowIndex = EditorGUILayout.IntField(mTypeRowIndex, GUILayout.Width(100f));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            // Comments row index set.
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(2);
            GUILayout.Label("Comments Row Index: ");
            GUILayout.Space(7);
            mCommentsRowIndex = EditorGUILayout.IntField(mCommentsRowIndex, GUILayout.Width(100f));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            // Data start row index set.
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(6);
            GUILayout.Label("Data Start Row Index: ");
            GUILayout.Space(7);
            mDataStartRowIndex = EditorGUILayout.IntField(mDataStartRowIndex, GUILayout.Width(100f));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            // Namespace.
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(8);
            GUILayout.Label("Metadata Namespace: ");
            GUILayout.Space(7);
            mMetadataNamespace = EditorGUILayout.TextField(mMetadataNamespace, GUILayout.Width(200f));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            // Excel files path set.
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(42);
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

            // Script files path set.
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(38);
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

            // Database cache files path set.
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(14);
            GUILayout.Label("DB Cache Files Path: ");
            EditorGUI.BeginDisabledGroup(true);
            mDBCacheFilesPath = EditorGUILayout.TextField(mDBCacheFilesPath, GUILayout.Width(350f));
            EditorGUI.EndDisabledGroup();
            GUILayout.Space(10f);
            if (GUILayout.Button("Browse"))
            {
                ConfigEditor.databaseCacheFilesPath = UnityEditor.EditorUtility.OpenFolderPanel("Database cache files of Directory You Want to Save", "", "");
            }
            GUILayout.EndHorizontal();

            // Database files path set.
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(54);
            GUILayout.Label("DB Files Path: ");
            EditorGUI.BeginDisabledGroup(true);
            mDBFilesPath = EditorGUILayout.TextField(mDBFilesPath, GUILayout.Width(350f));
            EditorGUI.EndDisabledGroup();
            GUILayout.Space(10f);
            if (GUILayout.Button("Browse"))
            {
                ConfigEditor.databaseFilesPath = UnityEditor.EditorUtility.OpenFolderPanel("Database files of Directory You Want to Save", "Assets", "");
            }
            GUILayout.EndHorizontal();

            // Button bar.
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
            ConfigEditor.debugMode = mDebugMode;
            ConfigEditor.primaryKey = mPrimaryKey;
            ConfigEditor.metadataNamespace = mMetadataNamespace;
            ConfigEditor.keyRowIndex = mKeyRowIndex;
            ConfigEditor.typeRowIndex = mTypeRowIndex;
            ConfigEditor.commentsRowIndex = mCommentsRowIndex;
            ConfigEditor.dataStartRowIndex = mDataStartRowIndex;
        }

        #endregion Messages
    }
}