using Excel;
using iBoxDB.LocalServer;
using QuickUnity.Config;
using QuickUnity.Utilitys;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
        private const string DEFAULT_PRIMARY_KEY = "id";

        /// <summary>
        /// The default metadata namespace.
        /// </summary>
        private const string DEFAULT_METADATA_NAMESPACE = "QuickUnity.Config";

        /// <summary>
        /// The pah of Config VO script file.
        /// </summary>
        private const string CONFIG_VO_SCRIPT_TPL_FILE_PATH = "Assets/QuickUnity/Editor/EditorResources/ClassTemplates/ConfigVOClassTpl.txt";

        /// <summary>
        /// The search pattern of excel files.
        /// </summary>
        private const string EXCEL_FILES_SEARCH_PATTERN = "*.xlsx";

        /// <summary>
        /// The default row index of key in table.
        /// </summary>
        private const int DEFAULT_KEY_ROW_INDEX = 0;

        /// <summary>
        /// The default row index of type in table.
        /// </summary>
        private const int DEFAULT_TYPE_ROW_INDEX = 1;

        /// <summary>
        /// The default row index of comments in table.
        /// </summary>
        private const int DEFAULT_COMMENTS_ROW_INDEX = 2;

        /// <summary>
        /// The start row index of data.
        /// </summary>
        private const int DEFAULT_DATA_START_ROW_INDEX = 3;

        /// <summary>
        /// The supported type parsers.
        /// </summary>
        private static Dictionary<string, Type> sSupportedTypeParsers = new Dictionary<string, Type>()
        {
            { "bool", typeof(BoolTypeParser) },
            { "byte", typeof(ByteTypeParser) },
            { "sbyte", typeof(SByteTypeParser) },
            { "int", typeof(IntTypeParser) },
            { "long", typeof(LongTypeParser) },
            { "float", typeof(FloatTypeParser) },
            { "string", typeof(StringTypeParser) }
        };

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
                    sTableIndexServer = new DB(ConfigMetadata.INDEX_TABLE_LOCAL_ADDRESS);
                    DatabaseConfig.Config config = sTableIndexServer.GetConfig();

                    if (config != null)
                    {
                        config.EnsureTable<MetadataLocalAddress>(MetadataLocalAddress.TABLE_NAME, MetadataLocalAddress.PRIMARY_KEY);
                        config.EnsureTable<ConfigParameter>(ConfigParameter.TABLE_NAME, ConfigParameter.PRIMARY_KEY);
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
                string value = EditorPrefs.GetString(EditorUtility.projectRootDirName + ".ConfigEditor.primaryKey");

                if (string.IsNullOrEmpty(value))
                {
                    primaryKey = DEFAULT_PRIMARY_KEY;
                    value = DEFAULT_PRIMARY_KEY;
                }

                return value;
            }
            set { EditorPrefs.SetString(EditorUtility.projectRootDirName + ".ConfigEditor.primaryKey", value); }
        }

        /// <summary>
        /// Gets or sets the index of the key row.
        /// </summary>
        /// <value>
        /// The index of the key row.
        /// </value>
        public static int keyRowIndex
        {
            get
            {
                int value = EditorPrefs.GetInt(EditorUtility.projectRootDirName + ".ConfigEditor.keyRowIndex");

                if (value == 0)
                {
                    keyRowIndex = DEFAULT_KEY_ROW_INDEX;
                    value = DEFAULT_KEY_ROW_INDEX;
                }

                return value;
            }
            set { EditorPrefs.SetInt(EditorUtility.projectRootDirName + ".ConfigEditor.keyRowIndex", value); }
        }

        /// <summary>
        /// Gets or sets the index of the type row.
        /// </summary>
        /// <value>
        /// The index of the type row.
        /// </value>
        public static int typeRowIndex
        {
            get
            {
                int value = EditorPrefs.GetInt(EditorUtility.projectRootDirName + ".ConfigEditor.typeRowIndex");

                if (value == 0)
                {
                    typeRowIndex = DEFAULT_TYPE_ROW_INDEX;
                    value = DEFAULT_TYPE_ROW_INDEX;
                }

                return value;
            }
            set { EditorPrefs.SetInt(EditorUtility.projectRootDirName + ".ConfigEditor.typeRowIndex", value); }
        }

        /// <summary>
        /// Gets or sets the index of the comments row.
        /// </summary>
        /// <value>
        /// The index of the comments row.
        /// </value>
        public static int commentsRowIndex
        {
            get
            {
                int value = EditorPrefs.GetInt(EditorUtility.projectRootDirName + ".ConfigEditor.commentsRowIndex");

                if (value == 0)
                {
                    commentsRowIndex = DEFAULT_COMMENTS_ROW_INDEX;
                    value = DEFAULT_COMMENTS_ROW_INDEX;
                }

                return value;
            }
            set { EditorPrefs.SetInt(EditorUtility.projectRootDirName + ".ConfigEditor.commentsRowIndex", value); }
        }

        /// <summary>
        /// Gets or sets the index of the data start row.
        /// </summary>
        /// <value>
        /// The index of the data start row.
        /// </value>
        public static int dataStartRowIndex
        {
            get
            {
                int value = EditorPrefs.GetInt(EditorUtility.projectRootDirName + ".ConfigEditor.dataStartRowIndex");

                if (value == 0)
                {
                    dataStartRowIndex = DEFAULT_DATA_START_ROW_INDEX;
                    value = DEFAULT_DATA_START_ROW_INDEX;
                }

                return value;
            }
            set { EditorPrefs.SetInt(EditorUtility.projectRootDirName + ".ConfigEditor.dataStartRowIndex", value); }
        }

        /// <summary>
        /// Gets or sets the metadata namespace.
        /// </summary>
        /// <value>
        /// The metadata namespace.
        /// </value>
        public static string metadataNamespace
        {
            get
            {
                string value = EditorPrefs.GetString(EditorUtility.projectRootDirName + ".ConfigEditor.metadataNamespace");

                if (string.IsNullOrEmpty(value))
                {
                    metadataNamespace = DEFAULT_METADATA_NAMESPACE;
                    value = DEFAULT_METADATA_NAMESPACE;
                }

                return value;
            }
            set { EditorPrefs.SetString(EditorUtility.projectRootDirName + ".ConfigEditor.metadataNamespace", value); }
        }

        /// <summary>
        /// Gets or sets the excel files path.
        /// </summary>
        /// <value>
        /// The excel files path.
        /// </value>
        public static string excelFilesPath
        {
            get { return EditorPrefs.GetString(EditorUtility.projectRootDirName + ".ConfigEditor.excelFilesPath"); }
            set { EditorPrefs.SetString(EditorUtility.projectRootDirName + ".ConfigEditor.excelFilesPath", value); }
        }

        /// <summary>
        /// Gets or sets the script files path.
        /// </summary>
        /// <value>
        /// The script files path.
        /// </value>
        public static string scriptFilesPath
        {
            get { return EditorPrefs.GetString(EditorUtility.projectRootDirName + ".ConfigEditor.scriptFilesPath"); }
            set { EditorPrefs.SetString(EditorUtility.projectRootDirName + ".ConfigEditor.scriptFilesPath", value); }
        }

        /// <summary>
        /// Gets or sets the database cache files path.
        /// </summary>
        /// <value>
        /// The database cache files path.
        /// </value>
        public static string databaseCacheFilesPath
        {
            get { return EditorPrefs.GetString(EditorUtility.projectRootDirName + ".ConfigEditor.databaseCacheFilesPath"); }
            set { EditorPrefs.SetString(EditorUtility.projectRootDirName + ".ConfigEditor.databaseCacheFilesPath", value); }
        }

        /// <summary>
        /// Gets or sets the database files path.
        /// </summary>
        /// <value>
        /// The database path.
        /// </value>
        public static string databaseFilesPath
        {
            get { return EditorPrefs.GetString(EditorUtility.projectRootDirName + ".ConfigEditor.databaseFilesPath"); }
            set { EditorPrefs.SetString(EditorUtility.projectRootDirName + ".ConfigEditor.databaseFilesPath", value); }
        }

        /// <summary>
        /// Generates the configuration metadata.
        /// </summary>
        public static void GenerateConfigMetadata()
        {
            while (EditorApplication.isCompiling || EditorApplication.isUpdating)
            {
            }

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

            if (string.IsNullOrEmpty(databaseCacheFilesPath))
            {
                UnityEditor.EditorUtility.DisplayDialog("Error", "Please set the path of database cache files !", "OK");
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

            // Create source database directory.
            if (!Directory.Exists(databaseCacheFilesPath))
                Directory.CreateDirectory(databaseCacheFilesPath);

            // Create database files directory.
            if (!Directory.Exists(databaseFilesPath))
                Directory.CreateDirectory(databaseFilesPath);

            // Delete old script files.
            EditorUtility.DeleteAllFilesInDirectory(scriptFilesPath);

            // Clear database.
            EditorUtility.DeleteAllFilesInDirectory(databaseCacheFilesPath);
            EditorUtility.DeleteAllFilesInDirectory(databaseFilesPath);

            // Reset all database.
            DB.ResetStorage();

            // Set the root path of database.
            DB.Root(databaseCacheFilesPath);

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
                            tplTextCopy = tplTextCopy.Replace("{$Namespace}", metadataNamespace);
                            tplTextCopy = tplTextCopy.Replace("{$ClassName}", fileName);
                            tplTextCopy = tplTextCopy.Replace("{$Fields}", fieldsString);
                            EditorUtility.WriteText(targetScriptPath, tplTextCopy);
                        }

                        // Generate data list.
                        List<ConfigMetadata> dataList = GenerateDataList(table, fileName, keys);

                        // Save data list.
                        Type type = ReflectionUtility.GetType(GetMetadataFullName(fileName));

                        if (type != null)
                        {
                            ReflectionUtility.InvokeStaticGenericMethod(typeof(ConfigEditor),
                                "SaveDataList",
                                type,
                                new object[] { fileName, databaseFilesPath, dataList, i });
                        }
                    }
                    catch (Exception exception)
                    {
                        Debug.LogError(string.Format("Error Message: {0}, Stack Trace: {1}", exception.Message, exception.StackTrace));
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

            // Move database files.
            EditorUtility.MoveAllFilesInDirectory(databaseCacheFilesPath, databaseFilesPath + Path.AltDirectorySeparatorChar);

            // Refresh.
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            // Destroy progress bar.
            UnityEditor.EditorUtility.ClearProgressBar();

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
                string key = rows[keyRowIndex][i].ToString();
                string typeString = rows[DEFAULT_TYPE_ROW_INDEX][i].ToString();
                string comments = rows[DEFAULT_COMMENTS_ROW_INDEX][i].ToString();

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
            for (int i = DEFAULT_DATA_START_ROW_INDEX; i < rowCount; ++i)
            {
                ConfigMetadata metadata = (ConfigMetadata)ReflectionUtility.CreateClassInstance(GetMetadataFullName(className));

                for (int j = 0, keysCount = keys.Count; j < keysCount; ++j)
                {
                    MetadataKey key = keys[j];
                    string cellValue = table.Rows[i][j].ToString();
                    ITypeParser parser = TypeParserFactory.CreateTypeParser(sSupportedTypeParsers[key.type]);

                    if (parser != null)
                    {
                        object value = parser.Parse(cellValue);
                        ReflectionUtility.SetObjectFieldValue(metadata, key.key, value);
                    }
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
        /// <param name="index">The index.</param>
        private static void SaveDataList<T>(string tableName, string databasePath, List<ConfigMetadata> dataList, int index) where T : class
        {
            // Create new database.
            Type type = ReflectionUtility.GetType(GetMetadataFullName(tableName));
            long localAddress = ConfigMetadata.INDEX_TABLE_LOCAL_ADDRESS + index + 1;

            if (type != null)
            {
                // Insert table index.
                if (tableIndexDB != null)
                {
                    bool success = tableIndexDB.Insert(MetadataLocalAddress.TABLE_NAME,
                        new MetadataLocalAddress() { typeName = type.Name, typeNamespace = type.Namespace, localAddress = localAddress });

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
                            success = dataDb.Insert(tableName, (T)configMetadata);

                            if (!success)
                            {
                                Debug.LogErrorFormat("Insert Metadata object failed: [tableName = {0}, type = {1}]", tableName, type.FullName);
                                break;
                            }
                        }

                        dataServer.Dispose();
                        dataServer = null;
                    }
                    else
                    {
                        Debug.LogErrorFormat("Insert MetadataLocalAddress object into table [{0}] failed: [typeName = {1}, localAddress = {2}]",
                            tableName, type.FullName, localAddress);
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
                bool success = tableIndexDB.Insert(ConfigParameter.TABLE_NAME, new ConfigParameter() { key = paramKey, value = primaryKey });

                if (!success)
                    Debug.LogErrorFormat("Insert ConfigParameter object failed: [key = {0}, value = {1}]", paramKey, primaryKey);
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
            foreach (KeyValuePair<string, Type> kvp in sSupportedTypeParsers)
            {
                if (kvp.Key == type)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the full name of the metadata.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        private static string GetMetadataFullName(string name)
        {
            return string.Format("{0}.{1}", metadataNamespace, name);
        }
    }
}