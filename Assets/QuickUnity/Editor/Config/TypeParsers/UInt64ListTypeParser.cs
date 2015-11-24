using System;
using System.Collections.Generic;

namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The list of ulong type.
    /// </summary>
    public class UInt64ListTypeParser : TypeParser, ITypeParser
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
            return "ulong[]";
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
            List<ulong> list = new List<ulong>();
            string listSep = ConfigEditor.listSeparator;

            string[] valueStrArr = value.Split(listSep.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            if (valueStrArr != null && valueStrArr.Length > 0)
            {
                foreach (string valueStr in valueStrArr)
                {
                    ulong ulongVal = ParseNumber<ulong>(valueStr);
                    list.Add(ulongVal);
                }
            }

            return list.ToArray();
        }

        #endregion API
    }
}