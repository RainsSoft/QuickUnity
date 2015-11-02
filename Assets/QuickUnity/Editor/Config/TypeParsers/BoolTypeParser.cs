using System.Globalization;
using System.Threading;

namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The bool type parser.
    /// </summary>
    public class BoolTypeParser : ITypeParser
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
                TextInfo textInfo = Thread.CurrentThread.CurrentCulture.TextInfo;
                value = textInfo.ToTitleCase(value);
                bool result = false;

                if (bool.TryParse(value, out result))
                    return result;
            }

            return false;
        }
    }
}