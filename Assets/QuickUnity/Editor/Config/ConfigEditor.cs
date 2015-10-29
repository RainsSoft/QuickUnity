﻿using Excel;
using iBoxDB.LocalServer;
using QuickUnity.Config;
using QuickUnity.Utilitys;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using UnityEditor;
using UnityEngine;

namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The metadata key struct.
    /// </summary>
    public struct MetadataKey
    {
        /// <summary>
        /// The key string.
        /// </summary>
        public string key;

        /// <summary>
        /// The type of object.
        /// </summary>
        public string type;

        /// <summary>
        /// The comments of object.
        /// </summary>
        public string comments;
    }

    /// <summary>
    /// ConfigEditor handle all methods of config data.
    /// </summary>
    public static class ConfigEditor
    {
        /// <summary>
        /// The default primary key.
        /// </summary>
        public const string DEFAULT_PRIMARY_KEY = "id";

        /// <summary>
        /// The configuration namespace prefix.
        /// </summary>
        private const string CONFIG_NAMESPACE_PREFIX = "QuickUnity.Config.";

        /// <summary>
        /// The pah of Config VO script file.
        /// </summary>
        private const string CONFIG_VO_SCRIPT_TPL_FILE_PATH = "Assets/QuickUnity/Editor/EditorResources/ClassTemplates/ConfigVOClassTpl.txt";

        /// <summary>
        /// The search pattern of excel files.
        /// </summary>
        private const string EXCEL_FILES_SEARCH_PATTERN = "*.xlsx";

        /// <summary>
        /// The row index of key in table.
        /// </summary>
        private const int KEY_ROW_INDEX = 0;

        /// <summary>
        /// The row index of type in table.
        /// </summary>
        private const int TYPE_ROW_INDEX = 1;

        /// <summary>
        /// The row index of comments in table.
        /// </summary>
        private const int COMMENTS_ROW_INDEX = 2;

        /// <summary>
        /// The start row index of values.
        /// </summary>
        private const int VALUES_START_ROW_INDEX = 3;

        /// <summary>
        /// The supported type parsers.
        /// </summary>
        private static Dictionary<string, string> sSupportedTypeParsers = new Dictionary<string, string>()
        {
            { "int", "ParseInt" },
            { "long", "ParseLong" },
            { "float", "ParseFloat" },
            { "string", "ParseString" }
        };

        /// <summary>
        /// The source database path.
        /// </summary>
        private static string sSourceDatabasePath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Metadata";

        /// <summary>
        /// The table index local server.
        /// </summary>
        private static DB sTableIndexServer;

        /// <summary>
        /// Gets the table index server.
        /// </summary>
        /// <value>
        /// The table index server.
        /// </value>
        public static DB tableIndexServer
        {
            get
            {
                if (sTableIndexServer == null)
                {
                    sTableIndexServer = new DB(1);
                    DatabaseConfig.Config config = sTableIndexServer.GetConfig();

                    if (config != null)
                    {
                        config.EnsureTable<MetadataLocalAddress>(MetadataLocalAddress.TABLE_NAME, MetadataLocalAddress.PRIMARY_KEY);
                        config.EnsureTable<ConfigParamater>(ConfigParamater.TABLE_NAME, ConfigParamater.PRIMARY_KEY);
                    }

                    sTableIndexServer.MinConfig();
                }

                return sTableIndexServer;
            }
        }

        /// <summary>
        /// The table index database.
        /// </summary>
        private static DB.AutoBox sTableIndexDB;

        /// <summary>
        /// Gets the table index database.
        /// </summary>
        /// <value>
        /// The table index database.
        /// </value>
        public static DB.AutoBox tableIndexDB
        {
            get
            {
                if (sTableIndexDB == null && tableIndexServer != null)
                    sTableIndexDB = tableIndexServer.Open();

                return sTableIndexDB;
            }
        }

        /// <summary>
        /// The metadata key format function.
        /// </summary>
        public static Func<string, string> metadataKeyFormatter;

        /// <summary>
        /// The metadata name format function.
        /// </summary>
        public static Func<string, string> metadataNameFormatter;

        /// <summary>
        /// Gets or sets the primary key.
        /// </summary>
        /// <value>
        /// The primary key.
        /// </value>
        public static string primaryKey
        {
            get
            {
                string value = EditorPrefs.GetString(EditorUtility.projectName + ".ConfigEditor.primaryKey");

                if (string.IsNullOrEmpty(value))
                {
                    primaryKey = DEFAULT_PRIMARY_KEY;
                    value = DEFAULT_PRIMARY_KEY;
                }

                return value;
            }
            set { EditorPrefs.SetString(EditorUtility.projectName + ".ConfigEditor.primaryKey", value); }
        }

        /// <summary>
        /// Gets or sets the excel files path.
        /// </summary>
        /// <value>
        /// The excel files path.
        /// </value>
        public static string excelFilesPath
        {
            get { return EditorPrefs.GetString(EditorUtility.projectName + ".ConfigEditor.excelFilesPath"); }
            set { EditorPrefs.SetString(EditorUtility.projectName + ".ConfigEditor.excelFilesPath", value); }
        }

        /// <summary>
        /// Gets or sets the script files path.
        /// </summary>
        /// <value>
        /// The script files path.
        /// </value>
        public static string scriptFilesPath
        {
            get { return EditorPrefs.GetString(EditorUtility.projectName + ".ConfigEditor.scriptFilesPath"); }
            set { EditorPrefs.SetString(EditorUtility.projectName + ".ConfigEditor.scriptFilesPath", value); }
        }

        /// <summary>
        /// Gets or sets the database files path.
        /// </summary>
        /// <value>
        /// The database path.
        /// </value>
        public static string databaseFilesPath
        {
            get { return EditorPrefs.GetString(EditorUtility.projectName + ".ConfigEditor.databaseFilesPath"); }
            set { EditorPrefs.SetString(EditorUtility.projectName + ".ConfigEditor.databaseFilesPath", value); }
        }

        /// <summary>
        /// Generates the configuration metadata.
        /// </summary>
        public static void GenerateConfigMetadata()
        {
            if (string.IsNullOrEmpty(excelFilesPath))
            {
                UnityEditor.EditorUtility.DisplayDialog("Error", "Please set the path of excel files !", "OK");
                return;
            }

            if (string.IsNullOrEmpty(scriptFilesPath))
            {
                UnityEditor.EditorUtility.DisplayDialog("Error", "Please set the path of script files !", "OK");
                return;
            }

            if (string.IsNullOrEmpty(databaseFilesPath))
            {
                UnityEditor.EditorUtility.DisplayDialog("Error", "Please set the path of database files !", "OK");
                return;
            }

            DirectoryInfo dirInfo = new DirectoryInfo(excelFilesPath);
            FileInfo[] fileInfos = dirInfo.GetFiles(EXCEL_FILES_SEARCH_PATTERN);
            string tplText = EditorUtility.ReadTextAsset(CONFIG_VO_SCRIPT_TPL_FILE_PATH);

            // Delete old script files.
            EditorUtility.DeleteAllAssetsInDirectory(scriptFilesPath);

            // Clear database.
            EditorUtility.DeleteAllFilesInDirectory(sSourceDatabasePath);
            EditorUtility.DeleteAllAssetsInDirectory(databaseFilesPath);

            // Reset all database.
            DB.ResetStorage();

            // Create source database directory.
            if (!Directory.Exists(sSourceDatabasePath))
                Directory.CreateDirectory(sSourceDatabasePath);

            // Set the root path of database.
            DB.Root(sSourceDatabasePath);

            for (int i = 0, length = fileInfos.Length; i < length; ++i)
            {
                FileInfo fileInfo = fileInfos[i];

                if (fileInfo != null)
                {
                    string filePath = fileInfo.FullName;
                    string fileName = Path.GetFileNameWithoutExtension(fileInfo.Name);

                    // Format metadata name.
                    if (metadataNameFormatter != null)
                        fileName = metadataNameFormatter(fileName);

                    try
                    {
                        FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read);
                        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(fileStream);
                        DataSet result = excelReader.AsDataSet();
                        DataTable table = result.Tables[0];
                        List<MetadataKey> keys = GenerateTableKeys(table);
                        string fieldsString = GenerateVOFieldsString(keys);
                        string targetScriptPath = scriptFilesPath;

                        // Generate VO script files.
                        if (!string.IsNullOrEmpty(targetScriptPath))
                        {
                            targetScriptPath += Path.DirectorySeparatorChar + fileName + EditorUtility.SCRIPT_FILE_EXTENSIONS;
                            string tplTextCopy = (string)tplText.Clone();
                            tplTextCopy = tplTextCopy.Replace("{$ClassName}", fileName);
                            tplTextCopy = tplTextCopy.Replace("{$Fields}", fieldsString);
                            EditorUtility.WriteText(targetScriptPath, tplTextCopy);
                        }

                        // Generate data list.
                        List<ConfigMetadata> dataList = GenerateDataList(table, fileName, keys);

                        // Save data list.
                        Type type = ReflectionUtility.GetType(CONFIG_NAMESPACE_PREFIX + fileName);

                        if (type != null)
                        {
                            ReflectionUtility.InvokeStaticGenericMethod(typeof(ConfigEditor),
                                "SaveDataList",
                                type,
                                new object[] { fileName, databaseFilesPath, dataList, i + 2 });
                        }
                    }
                    catch (Exception exception)
                    {
                        Debug.LogError(string.Format("Please check files and setup options ! [Error Message: {0}]", exception.Message));
                    }
                }

                // Show progress of generation.
                UnityEditor.EditorUtility.DisplayProgressBar("Holding on", "The progress of configuration metadata generation.", (float)(i + 1) / length);
            }

            // Save configuration parameters.
            SaveConfigParameters();

            // Destroy database.
            DestroyDatabase();

            // Sleep 0.5 second.
            Thread.Sleep(500);

            // Destroy progress bar.
            UnityEditor.EditorUtility.ClearProgressBar();

            // Move database files.
            EditorUtility.MoveAllFilesInDirectory(sSourceDatabasePath, databaseFilesPath + Path.AltDirectorySeparatorChar);

            // Refresh.
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            // Show alert.
            UnityEditor.EditorUtility.DisplayDialog("Tip", "The metadata generated !", "OK");
        }

        /// <summary>
        /// Generates the keys of table.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns>The list of keys.</returns>
        private static List<MetadataKey> GenerateTableKeys(DataTable table)
        {
            DataColumnCollection columns = table.Columns;
            DataRowCollection rows = table.Rows;
            int columnCount = columns.Count;
            List<MetadataKey> keys = new List<MetadataKey>();

            for (int i = 0; i < columnCount; i++)
            {
                string key = rows[KEY_ROW_INDEX][i].ToString();
                string typeString = rows[TYPE_ROW_INDEX][i].ToString();
                string comments = rows[COMMENTS_ROW_INDEX][i].ToString();

                // Format key.
                if (metadataKeyFormatter != null)
                    key = metadataKeyFormatter(key);

                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(typeString) && IsSupportedDataType(typeString))
                {
                    MetadataKey metadataKey = new MetadataKey();
                    metadataKey.key = key;
                    metadataKey.type = typeString;
                    metadataKey.comments = comments;
                    keys.Add(metadataKey);
                }
            }

            return keys;
        }

        /// <summary>
        /// Generates the VO fields string.
        /// </summary>
        /// <param name="keys">The keys list.</param>
        /// <returns>The VO fields string.</returns>
        private static string GenerateVOFieldsString(List<MetadataKey> keys)
        {
            string fieldsString = string.Empty;

            if (keys != null && keys.Count > 0)
            {
                for (int i = 0, length = keys.Count; i < length; ++i)
                {
                    MetadataKey metadataKey = keys[i];
                    fieldsString += string.Format("\t\t/// <summary>\r\n\t\t/// {0}\r\n\t\t/// </summary>\r\n\t\tpublic {1} {2};",
                        metadataKey.comments,
                        metadataKey.type,
                        metadataKey.key);

                    if (i < length - 1)
                        fieldsString += "\r\n\r\n";
                }
            }

            return fieldsString;
        }

        /// <summary>
        /// Generates the data list.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="className">Name of the class.</param>
        /// <param name="keys">The keys.</param>
        /// <returns></returns>
        private static List<ConfigMetadata> GenerateDataList(DataTable table, string className, List<MetadataKey> keys)
        {
            List<ConfigMetadata> dataList = new List<ConfigMetadata>();

            int rowCount = table.Rows.Count;
            for (int i = VALUES_START_ROW_INDEX; i < rowCount; ++i)
            {
                ConfigMetadata metadata = (ConfigMetadata)ReflectionUtility.CreateClassInstance(CONFIG_NAMESPACE_PREFIX + className);

                for (int j = 0, keysCount = keys.Count; j < keysCount; ++j)
                {
                    MetadataKey key = keys[j];
                    string methodName = sSupportedTypeParsers[key.type];
                    string cellValue = table.Rows[i][j].ToString();
                    object value = ReflectionUtility.InvokeStaticMethod(typeof(QuickUnity.Editor.Config.ConfigEditor), methodName, new object[1] { cellValue });
                    ReflectionUtility.SetObjectFieldValue(metadata, key.key, value);
                }

                dataList.Add(metadata);
            }

            return dataList;
        }

        /// <summary>
        /// Saves the data list.
        /// </summary>
        /// <typeparam name="T">The type of data.</typeparam>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="databasePath">The database path.</param>
        /// <param name="dataList">The data list.</param>
        /// <param name="localAddress">The local address.</param>
        private static void SaveDataList<T>(string tableName, string databasePath, List<ConfigMetadata> dataList, long localAddress) where T : class
        {
            // Create new database.
            Type type = ReflectionUtility.GetType(CONFIG_NAMESPACE_PREFIX + tableName);

            if (type != null)
            {
                // Insert table index.
                if (tableIndexDB != null)
                {
                    bool success = tableIndexDB.Insert(MetadataLocalAddress.TABLE_NAME,
                        new MetadataLocalAddress() { typeName = type.FullName, localAddress = localAddress });

                    if (success)
                    {
                        // Insert data list into new table by localAddress.
                        DB dataServer = new DB(localAddress);
                        ReflectionUtility.InvokeGenericMethod(dataServer.GetConfig(),
                            "EnsureTable",
                            type,
                            new object[] { tableName, new string[] { primaryKey } });
                        dataServer.MinConfig();
                        DB.AutoBox dataDb = dataServer.Open();

                        foreach (object configMetadata in dataList)
                        {
                            dataDb.Insert(tableName, (T)configMetadata);
                        }

                        dataServer.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// Saves the configuration parameters.
        /// </summary>
        private static void SaveConfigParameters()
        {
            if (tableIndexDB != null)
            {
                // Save parameter "PrimaryKey".
                string paramKey = Enum.GetName(typeof(ConfigParameterName), ConfigParameterName.PrimaryKey);
                tableIndexDB.Insert(ConfigParamater.TABLE_NAME, new ConfigParamater() { key = paramKey, value = primaryKey });
            }
        }

        /// <summary>
        /// Destroys the database.
        /// </summary>
        private static void DestroyDatabase()
        {
            if (sTableIndexDB != null)
                sTableIndexDB = null;

            if (sTableIndexServer != null)
            {
                sTableIndexServer.Dispose();
                sTableIndexServer = null;
            }
        }

        /// <summary>
        /// Determines whether [the specified data type] [is supported type].
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private static bool IsSupportedDataType(string type)
        {
            foreach (KeyValuePair<string, string> kvp in sSupportedTypeParsers)
            {
                if (kvp.Key == type)
                    return true;
            }

            return false;
        }

        #region Parse Data Type Functions

        /// <summary>
        /// Parses the int value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The int value.</returns>
        private static int ParseInt(string value)
        {
            return int.Parse(value);
        }

        /// <summary>
        /// Parses the long value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The long value.</returns>
        private static long ParseLong(string value)
        {
            return long.Parse(value);
        }

        /// <summary>
        /// Parses the float value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The float value.</returns>
        private static float ParseFloat(string value)
        {
            return float.Parse(value);
        }

        /// <summary>
        /// Parses the string value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The string value.</returns>
        private static string ParseString(string value)
        {
            return value;
        }

        #endregion Parse Data Type Functions
    }
}