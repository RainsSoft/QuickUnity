namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The ulong type parser.
    /// </summary>
    public class UInt64TypeParser : ITypeParser
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
            ulong result = ulong.MinValue;

            if (!string.IsNullOrEmpty(value))
                ulong.TryParse(value, out result);

            return result;
        }
    }
}