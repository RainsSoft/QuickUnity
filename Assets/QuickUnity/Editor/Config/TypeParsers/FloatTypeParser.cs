namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The float type parser.
    /// </summary>
    public class FloatTypeParser : TypeParser, ITypeParser
    {
        /// <summary>
        /// Parses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The parsed value.
        /// </returns>
        public override object ParseValue(string value)
        {
            return Parse<float>(value);
        }
    }
}