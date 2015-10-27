using QuickUnity.Config;
using System.Collections;
using System.IO;
using UnityEngine;

namespace QuickUnity.Examples.Config
{
    /// <summary>
    /// The example of configuration metadata.
    /// </summary>
    public class ConfigExample : MonoBehaviour
    {
        // Use this for initialization
        private void Start()
        {
            ConfigManager manager = ConfigManager.instance;
            manager.SetDatabaseRootPath(Application.streamingAssetsPath + Path.DirectorySeparatorChar + "Metadata");
            TestData data = manager.GetConfigMetadata<TestData>(1);
            Debug.Log(data);

            TestDataTwo data2 = manager.GetConfigMetadata<TestDataTwo>(3);
            Debug.Log(data2);
        }
    }
}