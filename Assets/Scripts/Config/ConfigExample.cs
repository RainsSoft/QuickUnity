using QuickUnity.Config;
using QuickUnity.Examples.Config.VO;
using System.Collections.Generic;
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
            manager.SetDatabaseRootPath(Application.streamingAssetsPath + Path.AltDirectorySeparatorChar + "Metadata");

            TestData data = manager.GetConfigMetadata<TestData>(1);
            Debug.Log(data);

            Dictionary<string, object> conditions = new Dictionary<string, object>();
            conditions.Add("itemName", "edwqdsa");
            conditions.Add("price", 154791f);
            List<TestDataTwo> list = manager.GetConfigMetadataList<TestDataTwo>(conditions);
            Debug.Log(list[0]);
        }
    }
}