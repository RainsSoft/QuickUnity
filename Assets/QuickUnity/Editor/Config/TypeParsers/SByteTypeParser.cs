namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The sbyte type parser.
    /// </summary>
    public class SByteTypeParser : TypeParser, ITypeParser
    {
        /// <summary>
        /// Parses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The parsed value.
        /// </returns>
        public override object Parse(string value)
        {
            return Parse<sbyte>(value);
        }
    }
}