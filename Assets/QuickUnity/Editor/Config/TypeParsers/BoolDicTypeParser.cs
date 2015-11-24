using System.IO;

namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The <string, bool> Dictionary object parser.
    /// </summary>
    public class BoolDicTypeParser : BoolTypeParser, ITypeParser
    {
        /// <summary>
        /// Parses the type string.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        /// The parsed type string.
        /// </returns>
        public override string ParseType(string source)
        {
            return "Dictionary<string, bool>";
        }

        /// <summary>
        /// Gets the stream data.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The stream data.
        /// </returns>
        public override MemoryStream GetStream(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
            }

            return null;
        }

        /// <summary>
        /// Parses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The parsed value.
        /// </returns>
        public override object ParseValue(string value)
        {
            return null;
        }
    }
}