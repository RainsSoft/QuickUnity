namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The ushort type parser.
    /// </summary>
    public class UInt16TypeParser : ITypeParser
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
            ushort result = ushort.MinValue;

            if (!string.IsNullOrEmpty(value))
                ushort.TryParse(value, out result);

            return result;
        }
    }
}