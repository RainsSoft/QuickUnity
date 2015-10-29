using QuickUnity.Utilitys;
using System.Collections.Generic;

namespace QuickUnity.Config
{
    /// <summary>
    /// The metadata local address value object.
    /// </summary>
    public class MetadataLocalAddress
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
        /// The local address.
        /// </summary>
        public long localAddress;
    }

    /// <summary>
    /// The configuration parameter object.
    /// </summary>
    public class ConfigParamater
    {
        /// <summary>
        /// The name of table.
        /// </summary>
        public const string TABLE_NAME = "ConfigParamaters";

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
    }

    /// <summary>
    /// The configuration metadata.
    /// </summary>
    public abstract class ConfigMetadata
    {
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
}