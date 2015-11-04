namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The string type parser.
    /// </summary>
    public class StringTypeParser : TypeParser, ITypeParser
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
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            return value;
        }
    }
}