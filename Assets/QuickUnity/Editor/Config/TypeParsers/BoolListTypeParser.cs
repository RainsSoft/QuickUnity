using System;
using System.Collections.Generic;

namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The list of bool type.
    /// </summary>
    public class BoolListTypeParser : BoolTypeParser, ITypeParser
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
            return "bool[]";
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
            List<bool> list = new List<bool>();
            string listSep = ConfigEditor.listSeparator;

            string[] valueStrArr = value.Split(listSep.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            if (valueStrArr != null && valueStrArr.Length > 0)
            {
                foreach (string valueStr in valueStrArr)
                {
                    bool boolVal = (bool)base.ParseValue(valueStr);
                    list.Add(boolVal);
                }
            }

            return list.ToArray();
        }

        #endregion API
    }
}