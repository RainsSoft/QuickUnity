namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The sbyte type parser.
    /// </summary>
    public class SByteTypeParser : ITypeParser
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
            sbyte result = sbyte.MinValue;

            if (!string.IsNullOrEmpty(value))
                sbyte.TryParse(value, out result);

            return result;
        }
    }
}