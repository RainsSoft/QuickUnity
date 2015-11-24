using System;
using System.Collections.Generic;

namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The list of int type.
    /// </summary>
    public class Int32ListTypeParser : TypeParser, ITypeParser
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
            return "int[]";
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
            List<int> list = new List<int>();
            string listSep = ConfigEditor.listSeparator;

            string[] valueStrArr = value.Split(listSep.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            if (valueStrArr != null && valueStrArr.Length > 0)
            {
                foreach (string valueStr in valueStrArr)
                {
                    int intVal = ParseNumber<int>(valueStr);
                    list.Add(intVal);
                }
            }

            return list.ToArray();
        }

        #endregion API
    }
}