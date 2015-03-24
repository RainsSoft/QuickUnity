using System.Collections;
using UnityEngine;

/// <summary>
/// The Config namespace.
/// </summary>
namespace QuickUnity.Config
{
    /// <summary>
    /// A class to mange Excel configuration data.
    /// </summary>
    public class ExcelConfigManager
    {
        /// <summary>
        /// The synchronize root.
        /// </summary>
        private static readonly object syncRoot = new object();

        /// <summary>
        /// The instance of singleton.
        /// </summary>
        private static ExcelConfigManager instance;

        /// <summary>
        /// Gets the instance of singleton.
        /// </summary>
        /// <value>The instance of ExcelConfigManager.</value>
        public static ExcelConfigManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ExcelConfigManager();
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="ExcelConfigManager"/> class from being created.
        /// </summary>
        private ExcelConfigManager()
        {
        }
    }
}