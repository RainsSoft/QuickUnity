using iBoxDB.LocalServer;
using QuickUnity.Patterns;
using System;
using System.Collections.Generic;

namespace QuickUnity.Config
{
    /// <summary>
    /// Manage all configuration metadata.
    /// </summary>
    public sealed class ConfigManager : Singleton<ConfigManager>
    {
        /// <summary>
        /// The table index database.
        /// </summary>
        private DB.AutoBox mTableIndexDB;

        /// <summary>
        /// The database map.
        /// </summary>
        private Dictionary<Type, DB.AutoBox> mDBMap;

        /// <summary>
        /// Prevents a default instance of the <see cref="ConfigManager"/> class from being created.
        /// </summary>
        private ConfigManager()
        {
            mDBMap = new Dictionary<Type, DB.AutoBox>();
        }

        #region API

        /// <summary>
        /// Sets the database root path.
        /// </summary>
        /// <param name="rootPath">The root path.</param>
        public void SetDatabaseRootPath(string rootPath)
        {
            DB.Root(rootPath);
            DB server = new DB(1);
            server.GetConfig().EnsureTable<MetadataLocalAddress>(MetadataLocalAddress.METADATA_INDEX_TABLE_NAME,
                MetadataLocalAddress.PRIMARY_KEY_NAME);
            mTableIndexDB = server.Open();
        }

        /// <summary>
        /// Gets the configuration metadata.
        /// </summary>
        /// <typeparam name="T">The type of metadata. </typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public T GetConfigMetadata<T>(long id) where T : ConfigMetadata, new()
        {
            Type type = typeof(T);
            string tableName = type.Name;
            DB.AutoBox db = null;

            if (mDBMap.ContainsKey(type))
            {
                db = mDBMap[type];
            }
            else
            {
                long localAddress = GetTableLocalAddress<T>();

                if (localAddress != -1)
                {
                    DB server = new DB(localAddress);
                    server.GetConfig().EnsureTable<T>(tableName, ConfigMetadata.PRIMARY_KEY_NAME);
                    db = server.Open();
                    mDBMap.Add(type, db);
                }
            }

            if (db != null)
            {
                T item = db.SelectKey<T>(tableName, id);
                return item;
            }

            return null;
        }

        #endregion API

        #region Private Functions

        /// <summary>
        /// Gets the table local address.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <returns>The local address of table.</returns>
        private long GetTableLocalAddress<T>() where T : ConfigMetadata
        {
            MetadataLocalAddress item = mTableIndexDB.SelectKey<MetadataLocalAddress>(MetadataLocalAddress.METADATA_INDEX_TABLE_NAME,
                typeof(T).FullName);

            if (item != null)
                return item.localAddress;

            return -1;
        }

        #endregion Private Functions
    }
}