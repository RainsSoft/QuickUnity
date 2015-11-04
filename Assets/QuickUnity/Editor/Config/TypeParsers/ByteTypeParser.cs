namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The byte type parser.
    /// </summary>
    public class ByteTypeParser : TypeParser, ITypeParser
    {
        /// <summary>
        /// Parses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The parsed value.
        /// </returns>
        public override object Parse(string value)
        {
            return Parse<byte>(value);
        }
    }
}