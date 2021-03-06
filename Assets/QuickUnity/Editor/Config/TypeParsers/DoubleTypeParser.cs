﻿namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The double type parser.
    /// </summary>
    public class DoubleTypeParser : TypeParser, ITypeParser
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
            return ParseNumber<double>(value);
        }
    }
}