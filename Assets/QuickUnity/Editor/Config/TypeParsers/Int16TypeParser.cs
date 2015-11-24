namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The short type parser.
    /// </summary>
    public class Int16TypeParser : TypeParser, ITypeParser
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
            return ParseNumber<short>(value);
        }
    }
}