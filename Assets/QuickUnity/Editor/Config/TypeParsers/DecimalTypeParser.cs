namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The decimal type parser.
    /// </summary>
    public class DecimalTypeParser : ITypeParser
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
            decimal result = decimal.Zero;

            if (!string.IsNullOrEmpty(value))
                decimal.TryParse(value, out result);

            return result;
        }
    }
}