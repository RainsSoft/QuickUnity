namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The short type parser.
    /// </summary>
    public class Int16TypeParser : ITypeParser
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
            short result = short.MinValue;

            if (!string.IsNullOrEmpty(value))
                short.TryParse(value, out result);

            return result;
        }
    }
}