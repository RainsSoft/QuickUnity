using QuickUnity.Utilitys;
using System.Collections.Generic;

namespace QuickUnity.Config
{
    /// <summary>
    /// The configuration metadata.
    /// </summary>
    public abstract class ConfigMetadata
    {
        /// <summary>
        /// The index table localAddress.
        /// </summary>
        public const long INDEX_TABLE_LOCAL_ADDRESS = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigMetadata"/> class.
        /// </summary>
        public ConfigMetadata()
        {
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            string output = string.Empty;
            Dictionary<string, object> map = ReflectionUtility.GetObjectFieldsValues(this);

            foreach (KeyValuePair<string, object> kvp in map)
            {
                output += kvp.Key + ": " + kvp.Value + ", ";
            }

            return base.ToString() + " (" + output.Substring(0, output.Length - 2) + ")";
        }
    }

    /// <summary>
    /// The metadata local address value object.
    /// </summary>
    public class MetadataLocalAddress : ConfigMetadata
    {
        /// <summary>
        /// The name of table.
        /// </summary>
        public const string TABLE_NAME = "MetadataTableIndexs";

        /// <summary>
        /// The primary key.
        /// </summary>
        public const string PRIMARY_KEY = "typeName";

        /// <summary>
        /// The type name of object.
        /// </summary>
        public string typeName;

        /// <summary>
        /// The type namespace.
        /// </summary>
        public string typeNamespace;

        /// <summary>
        /// The local address.
        /// </summary>
        public long localAddress;

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataLocalAddress" /> class.
        /// </summary>
        public MetadataLocalAddress()
            : base()
        {
        }
    }

    /// <summary>
    /// The configuration parameter object.
    /// </summary>
    public class ConfigParameter : ConfigMetadata
    {
        /// <summary>
        /// The name of table.
        /// </summary>
        public const string TABLE_NAME = "ConfigParameters";

        /// <summary>
        /// The primary key.
        /// </summary>
        public const string PRIMARY_KEY = "key";

        /// <summary>
        /// The key.
        /// </summary>
        public string key;

        /// <summary>
        /// The value.
        /// </summary>
        public string value;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigParameter"/> class.
        /// </summary>
        public ConfigParameter()
            : base()
        {
        }
    }
}