namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The string type parser.
    /// </summary>
    public class StringTypeParser : ITypeParser
    {
        /// <summary>
        /// Parses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The parsed value.
        /// </returns>
        public object Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            return value;
        }
    }
}