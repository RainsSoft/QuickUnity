namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The bool type parser.
    /// </summary>
    public class BoolTypeParser : TypeParser, ITypeParser
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
            return ParseBool(value);
        }
    }
}