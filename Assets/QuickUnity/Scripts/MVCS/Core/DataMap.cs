using System;
using System.Collections.Generic;

namespace QuickUnity.MVCS
{
    /// <summary>
    /// A map class to save data with Dictionary.
    /// </summary>
    /// <typeparam name="K">The type of key in Dictionary.</typeparam>
    /// <typeparam name="V">The type of value in Dictionary.</typeparam>
    public class DataMap<K, V> : IDisposable
    {
        /// <summary>
        /// The map dictionary.
        /// </summary>
        protected Dictionary<K, V> mMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataMap{K, V}"/> class.
        /// </summary>
        public DataMap()
        {
            mMap = new Dictionary<K, V>();
        }

        #region Pubic Functions

        #region IDispsable Implementations

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            if (mMap != null)
            {
                mMap.Clear();
                mMap = null;
            }
        }

        #endregion IDispsable Implementations

        #endregion Pubic Functions

        #region Protected Functions

        /// <summary>
        /// Registers the specified data.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="data">The data.</param>
        protected void Register(K key, V data)
        {
            if (data == null)
                return;

            if (!mMap.ContainsKey(key))
                mMap.Add(key, data);
        }

        /// <summary>
        /// Retrieves the specified data.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The data.</returns>
        protected V Retrieve(K key)
        {
            if (key != null && mMap.ContainsKey(key))
                return mMap[key];

            return default(V);
        }

        /// <summary>
        /// Removes the specified data by key.
        /// </summary>
        /// <param name="key">The key.</param>
        protected void Remove(K key)
        {
            if (key == null)
                return;

            if (mMap.ContainsKey(key))
                mMap.Remove(key);
        }

        /// <summary>
        /// Removes the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        protected void Remove(V data)
        {
            if (data == null)
                return;

            foreach (KeyValuePair<K, V> kvp in mMap)
            {
                if (kvp.Value.Equals(data))
                {
                    Remove(kvp.Key);
                    return;
                }
            }
        }

        #endregion Protected Functions
    }
}