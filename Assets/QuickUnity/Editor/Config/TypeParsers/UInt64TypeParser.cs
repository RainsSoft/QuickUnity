namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The ulong type parser.
    /// </summary>
    public class UInt64TypeParser : TypeParser, ITypeParser
    {
        /// <summary>
        /// Parses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The parsed value.
        /// </returns>
        public override object ParseValue(string value)
        {
            return ParseNumber<ulong>(value);
        }
    }
}