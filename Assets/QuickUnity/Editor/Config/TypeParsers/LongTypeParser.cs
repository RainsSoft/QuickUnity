namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The long type parser.
    /// </summary>
    public class LongTypeParser : ITypeParser
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
                long result = 0;

                if (long.TryParse(value, out result))
                    return result;
            }

            return 0L;
        }
    }
}