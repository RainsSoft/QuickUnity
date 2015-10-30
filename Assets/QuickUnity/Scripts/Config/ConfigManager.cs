using iBoxDB.LocalServer;
using QuickUnity.Patterns;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace QuickUnity.Config
{
    /// <summary>
    /// The name of configuration parameter.
    /// </summary>
    public enum ConfigParameterName
    {
        PrimaryKey
    }

    /// <summary>
    /// Manage all configuration metadata.
    /// </summary>
    public sealed class ConfigManager : Singleton<ConfigManager>
    {
        /// <summary>
        /// The primary key.
        /// </summary>
        private string mPrimaryKey = string.Empty;

        /// <summary>
        /// The table index database.
        /// </summary>
        private DB.AutoBox mTableIndexDB;

        /// <summary>
        /// The metadata database map.
        /// </summary>
        private Dictionary<Type, DB.AutoBox> mMetadataDBMap;

        /// <summary>
        /// Prevents a default instance of the <see cref="ConfigManager"/> class from being created.
        /// </summary>
        private ConfigManager()
        {
            mMetadataDBMap = new Dictionary<Type, DB.AutoBox>();
        }

        #region API

        /// <summary>
        /// Sets the database root path.
        /// </summary>
        /// <param name="rootPath">The root path.</param>
        public void SetDatabaseRootPath(string rootPath)
        {
            DB.Root(rootPath);
            CreateTableIndexDB();
        }

        /// <summary>
        /// Gets the configuration metadata.
        /// </summary>
        /// <typeparam name="T">The type of metadata. </typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public T GetConfigMetadata<T>(long id) where T : ConfigMetadata, new()
        {
            DB.AutoBox db = GetDB<T>();

            if (db != null)
            {
                Type type = typeof(T);
                string tableName = type.Name;
                T item = db.SelectKey<T>(tableName, id);
                return item;
            }

            return null;
        }

        /// <summary>
        /// Gets the configuration metadata list.
        /// </summary>
        /// <typeparam name="T">The type of metadata.</typeparam>
        /// <param name="conditions">The conditions dictionary.</param>
        /// <returns></returns>
        public List<T> GetConfigMetadataList<T>(Dictionary<string, object> conditions) where T : ConfigMetadata, new()
        {
            DB.AutoBox db = GetDB<T>();

            if (db != null)
            {
                Type type = typeof(T);
                string tableName = type.Name;
                string sql = "from " + tableName + " where";

                int i = 0;
                int length = conditions.Keys.Count;

                foreach (string key in conditions.Keys)
                {
                    sql += " " + key + "==?";

                    if (i < length - 1)
                        sql += " &";

                    i++;
                }

                List<object> values = new List<object>(conditions.Values);
                IBEnumerable<T> items = db.Select<T>(sql, values.ToArray());
                return new List<T>(items);
            }

            return null;
        }

        /// <summary>
        /// Disposes the metadata database.
        /// </summary>
        /// <typeparam name="T">The type of metadata database.</typeparam>
        public void DisposeMetadataDB<T>()
        {
            Type type = typeof(T);
            DB.AutoBox db = mMetadataDBMap[type];

            if (db != null)
            {
                db.GetDatabase().Dispose();
                mMetadataDBMap.Remove(type);
                db = null;
            }
        }

        /// <summary>
        /// Disposes all metadata databases.
        /// </summary>
        public void DisposeAllMetadataDB()
        {
            foreach (KeyValuePair<Type, DB.AutoBox> kvp in mMetadataDBMap)
            {
                DB.AutoBox db = kvp.Value;

                if (db != null)
                    db.GetDatabase().Dispose();
            }

            mMetadataDBMap.Clear();
        }

        /// <summary>
        /// Disposes all databases.
        /// </summary>
        public void DisposeAllDB()
        {
            if (mTableIndexDB != null)
            {
                mTableIndexDB.GetDatabase().Dispose();
                mTableIndexDB = null;
            }

            DisposeAllMetadataDB();
        }

        #endregion API

        #region Private Functions

        /// <summary>
        /// Creates the table index database.
        /// </summary>
        private void CreateTableIndexDB()
        {
            if (mTableIndexDB == null)
            {
                DB server = new DB(ConfigMetadata.INDEX_TABLE_LOCAL_ADDRESS);
                DatabaseConfig.Config serverConfig = server.GetConfig();

                if (serverConfig != null)
                {
                    serverConfig.EnsureTable<MetadataLocalAddress>(MetadataLocalAddress.TABLE_NAME, MetadataLocalAddress.PRIMARY_KEY);
                    serverConfig.EnsureTable<ConfigParameter>(ConfigParameter.TABLE_NAME, ConfigParameter.PRIMARY_KEY);
                }

                mTableIndexDB = server.Open();
            }
        }

        /// <summary>
        /// Gets the primary key.
        /// </summary>
        /// <returns></returns>
        private string GetPrimaryKey()
        {
            if (string.IsNullOrEmpty(mPrimaryKey))
            {
                string paramKey = Enum.GetName(typeof(ConfigParameterName), ConfigParameterName.PrimaryKey);
                object param = GetConfigParameter(paramKey);

                if (param != null)
                    mPrimaryKey = param.ToString();
            }

            return mPrimaryKey;
        }

        /// <summary>
        /// Gets the configuration parameter.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The parameter value.</returns>
        private string GetConfigParameter(string key)
        {
            ConfigParameter configParam = mTableIndexDB.SelectKey<ConfigParameter>(ConfigParameter.TABLE_NAME, key);

            if (configParam != null)
                return configParam.value;

            return null;
        }

        /// <summary>
        /// Gets the table local address.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <returns>The local address of table.</returns>
        private long GetTableLocalAddress<T>() where T : ConfigMetadata
        {
            if (mTableIndexDB == null)
                CreateTableIndexDB();

            MetadataLocalAddress item = mTableIndexDB.SelectKey<MetadataLocalAddress>(MetadataLocalAddress.TABLE_NAME,
                typeof(T).FullName);

            if (item != null)
                return item.localAddress;

            return -1;
        }

        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <typeparam name="T">The type of metadata. </typeparam>
        /// <returns>The database object. </returns>
        private DB.AutoBox GetDB<T>() where T : ConfigMetadata, new()
        {
            Type type = typeof(T);
            string tableName = type.Name;
            DB.AutoBox db = null;

            if (mMetadataDBMap.ContainsKey(type))
            {
                db = mMetadataDBMap[type];
            }
            else
            {
                long localAddress = GetTableLocalAddress<T>();

                if (localAddress != -1)
                {
                    DB server = new DB(localAddress);
                    string primaryKey = GetPrimaryKey();

                    if (!string.IsNullOrEmpty(primaryKey))
                    {
                        server.GetConfig().EnsureTable<T>(tableName, primaryKey);
                        db = server.Open();
                        mMetadataDBMap.Add(type, db);
                    }
                    else
                    {
                        Debug.LogError("Table primary key can not be null or empty !");
                    }
                }
            }

            return db;
        }

        #endregion Private Functions
    }
}