namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The float type parser.
    /// </summary>
    public class FloatTypeParser : ITypeParser
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
                float result = 0f;

                if (float.TryParse(value, out result))
                    return result;
            }

            return 0f;
        }
    }
}