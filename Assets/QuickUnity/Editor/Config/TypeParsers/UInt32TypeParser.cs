namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The uint type parser.
    /// </summary>
    public class UInt32TypeParser : TypeParser, ITypeParser
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
            return Parse<uint>(value);
        }
    }
}