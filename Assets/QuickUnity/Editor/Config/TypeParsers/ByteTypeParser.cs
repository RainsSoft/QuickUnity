namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The byte type parser.
    /// </summary>
    public class ByteTypeParser : ITypeParser
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
            if (!string.IsNullOrEmpty(value))
            {
                byte result = 0;

                if (byte.TryParse(value, out result))
                    return result;
            }

            return byte.MinValue;
        }
    }
}