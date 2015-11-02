namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The double type parser.
    /// </summary>
    public class DoubleTypeParser : ITypeParser
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
            double result = 0d;

            if (!string.IsNullOrEmpty(value))
                double.TryParse(value, out result);

            return result;
        }
    }
}