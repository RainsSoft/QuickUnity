﻿namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The decimal type parser.
    /// </summary>
    public class DecimalTypeParser : TypeParser, ITypeParser
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
            return ParseNumber<decimal>(value);
        }
    }
}