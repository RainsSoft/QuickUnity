namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The int type parser.
    /// </summary>
    public class Int32TypeParser : ITypeParser
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
            int result = 0;

            if (!string.IsNullOrEmpty(value))
                int.TryParse(value, out result);

            return result;
        }
    }
}