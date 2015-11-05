namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The long type parser.
    /// </summary>
    public class Int64TypeParser : TypeParser, ITypeParser
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
            return Parse<long>(value);
        }
    }
}