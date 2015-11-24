using System;
using System.Collections.Generic;

namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The list of sbyte type.
    /// </summary>
    public class SByteListTypeParser : TypeParser, ITypeParser
    {
        #region API

        /// <summary>
        /// Parses the type string.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        /// The parsed type string.
        /// </returns>
        public override string ParseType(string source)
        {
            return "sbyte[]";
        }

        /// <summary>
        /// Parses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The parsed value.
        /// </returns>
        public override object ParseValue(string value)
        {
            List<sbyte> list = new List<sbyte>();
            string listSep = ConfigEditor.listSeparator;

            string[] valueStrArr = value.Split(listSep.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            if (valueStrArr != null && valueStrArr.Length > 0)
            {
                foreach (string valueStr in valueStrArr)
                {
                    sbyte sbyteVal = ParseNumber<sbyte>(valueStr);
                    list.Add(sbyteVal);
                }
            }

            return list.ToArray();
        }

        #endregion API
    }
}