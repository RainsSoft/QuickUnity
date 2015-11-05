namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The int type parser.
    /// </summary>
    public class Int32TypeParser : TypeParser, ITypeParser
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
            return Parse<int>(value);
        }
    }
}