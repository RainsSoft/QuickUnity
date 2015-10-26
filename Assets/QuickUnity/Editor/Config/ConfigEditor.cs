using Excel;
using iBoxDB.LocalServer;
using QuickUnity.Config;
using QuickUnity.Utilitys;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
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
        /// The configuration namespace prefix.
        /// </summary>
        private const string CONFIG_NAMESPACE_PREFIX = "QuickUnity.Config.";

        /// <summary>
        /// The pah of Config VO script file.
        /// </summary>
        private const string CONFIG_VO_SCRIPT_TPL_FILE_PATH = "Assets/QuickUnity/Editor/EditorResources/ClassTemplates/ConfigVOClassTpl.txt";

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
        /// Generates the configuration metadata.
        /// </summary>
        /// <param name="excelFilesPath">The excel files path.</param>
        /// <param name="scriptFilesPath">The script files path.</param>
        /// <param name="databasePath">The database path.</param>
        public static void GenerateConfigMetadata(string excelFilesPath, string scriptFilesPath, string databasePath)
        {
            string[] fileEntries = Directory.GetFiles(excelFilesPath, "*.xlsx");
            string tplText = EditorUtility.ReadTextAsset(CONFIG_VO_SCRIPT_TPL_FILE_PATH);

            for (int i = 0, length = fileEntries.Length; i < length; ++i)
            {
                string fileEntry = fileEntries[i];

                if (!string.IsNullOrEmpty(fileEntry))
                {
                    string[] dirs = fileEntry.Split(Path.DirectorySeparatorChar);
                    string fileName = dirs[dirs.Length - 1].Split('.')[0];

                    try
                    {
                        FileStream fileStream = File.Open(fileEntry, FileMode.Open, FileAccess.Read);
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

                            // Delete old script file.
                            FileInfo fileInfo = new FileInfo(targetScriptPath);

                            if (fileInfo != null)
                                fileInfo.Delete();

                            string tplTextCopy = (string)tplText.Clone();
                            tplTextCopy = tplTextCopy.Replace("{$ClassName}", fileName);
                            tplTextCopy = tplTextCopy.Replace("{$Fields}", fieldsString);
                            EditorUtility.WriteText(targetScriptPath, tplTextCopy);
                            AssetDatabase.Refresh();
                        }

                        // Generate data list.
                        List<ConfigMetadata> dataList = GenerateDataList(table, fileName, keys);

                        // Save data list.
                        Type type = ReflectionUtility.GetType(CONFIG_NAMESPACE_PREFIX + fileName);

                        if (type != null)
                        {
                            ReflectionUtility.InvokeStaticGenericMethod(typeof(QuickUnity.Editor.Config.ConfigEditor),
                                "SaveDataList",
                                type,
                                new object[] { fileName, databasePath, dataList, i + 2 });
                        }
                    }
                    catch (IOException exception)
                    {
                        Debug.LogError(string.Format("Please close the opened data file ! [Error Message: {0}]", exception.Message));
                    }
                }
            }

            AssetDatabase.Refresh();
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

                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(typeString) && IsSupportedType(typeString))
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
                ConfigMetadata metadata = null;

                while (metadata == null)
                {
                    metadata = (ConfigMetadata)ReflectionUtility.CreateClassInstance(CONFIG_NAMESPACE_PREFIX + className);
                }

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
        /// <param name="tableName">Name of the table.</param>
        /// <param name="databasePath">The database path.</param>
        /// <param name="dataList">The data list.</param>
        /// <param name="localAddress">The local address.</param>
        private static void SaveDataList<T>(string tableName, string databasePath, List<T> dataList, long localAddress) where T : class
        {
            // Clear database.
            DirectoryInfo dirInfo = new DirectoryInfo(databasePath);
            FileInfo[] fileInfos = dirInfo.GetFiles();

            foreach (FileInfo fileInfo in fileInfos)
            {
                fileInfo.Delete();
            }

            // Create new database.
            Type type = ReflectionUtility.GetType(CONFIG_NAMESPACE_PREFIX + tableName);

            if (type != null)
            {
                // Insert table index.
                DB.Root(databasePath);
                DB indexServer = new DB(1);
                ReflectionUtility.InvokeGenericMethod(indexServer.GetConfig(),
                    "EnsureTable",
                    typeof(MetadataLocalAddress),
                    new object[] { MetadataLocalAddress.METADATA_INDEX_TABLE_NAME, new string[] { "localAddress" } });
                indexServer.MinConfig();
                DB.AutoBox indexDb = indexServer.Open();
                bool success = indexDb.Insert(MetadataLocalAddress.METADATA_INDEX_TABLE_NAME, new MetadataLocalAddress() { localAddress = localAddress });

                // Insert data list into new table by localAddress.
                if (success)
                {
                    DB dataServer = new DB(localAddress);
                    ReflectionUtility.InvokeGenericMethod(dataServer.GetConfig(),
                        "EnsureTable",
                        type,
                        new object[] { tableName, new string[] { "id" } });
                    dataServer.MinConfig();
                    DB.AutoBox dataDb = dataServer.Open();

                    foreach (object configMetadata in dataList)
                    {
                        dataDb.Insert(tableName, (T)configMetadata);

                        //dataDb.Insert(tableName, (TestData)configMetadata);
                    }
                }
            }
        }

        /// <summary>
        /// Determines whether [the specified type] [is supported type].
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private static bool IsSupportedType(string type)
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