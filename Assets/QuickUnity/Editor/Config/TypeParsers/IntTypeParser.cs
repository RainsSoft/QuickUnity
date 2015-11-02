namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The int type parser.
    /// </summary>
    public class IntTypeParser : ITypeParser
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
                int result = 0;

                if (int.TryParse(value, out result))
                    return result;
            }

            return 0;
        }
    }
}