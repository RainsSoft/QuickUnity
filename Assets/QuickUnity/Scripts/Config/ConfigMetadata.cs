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
        /// The name of metadata local address table.
        /// </summary>
        public const string METADATA_INDEX_TABLE_NAME = "MetadataTableIndex";

        /// <summary>
        /// The name of primary key.
        /// </summary>
        public const string PRIMARY_KEY_NAME = "typeName";

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
    /// The configuration metadata.
    /// </summary>
    public abstract class ConfigMetadata
    {
        /// <summary>
        /// The name of primary key.
        /// </summary>
        public const string PRIMARY_KEY_NAME = "id";

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