using System.Globalization;
using System.Threading;

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
            bool result = false;

            if (!string.IsNullOrEmpty(value))
            {
                TextInfo textInfo = Thread.CurrentThread.CurrentCulture.TextInfo;
                value = textInfo.ToTitleCase(value.Trim());
                bool.TryParse(value, out result);
            }

            return result;
        }
    }
}