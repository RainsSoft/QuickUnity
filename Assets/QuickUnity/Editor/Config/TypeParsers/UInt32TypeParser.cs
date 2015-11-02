namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The uint type parser.
    /// </summary>
    public class UInt32TypeParser : ITypeParser
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
            uint result = uint.MinValue;

            if (!string.IsNullOrEmpty(value))
                uint.TryParse(value, out result);

            return result;
        }
    }
}