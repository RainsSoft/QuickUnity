using System;
using System.Collections.Generic;

namespace QuickUnity.Net.Http
{
    /// <summary>
    /// The URLVariables class allows you to transfer variables between an application and a server.
    /// </summary>
    public class URLVariables
    {
        /// <summary>
        /// The key/value pair variables.
        /// </summary>
        private Dictionary<string, string> mVariables;

        /// <summary>
        /// Gets the key/value pair variables.
        /// </summary>
        /// <value>The variables.</value>
        public Dictionary<string, string> variables
        {
            get { return mVariables; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="URLVariables"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        public URLVariables(string source = null)
        {
            mVariables = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(source))
                Decode(source);
        }

        /// <summary>
        /// Converts the variable string to properties of the specified Dictionary object.
        /// </summary>
        /// <param name="source">A URL-encoded query string containing name/value pairs.</param>

        public void Decode(string source)
        {
            string[] pairStrArr = source.Split('&');

            if (pairStrArr.Length > 0)
            {
                foreach (string pairStr in pairStrArr)
                {
                    string[] kvPair = pairStr.Split('=');

                    if (kvPair.Length == 2)
                        mVariables.Add(kvPair[0], kvPair[1]);
                }
            }
        }

        /// <summary>
        /// Returns a string containing all enumerable variables, in the MIME content encoding application/x-www-form-urlencoded.
        /// </summary>
        /// <returns>A <see cref="System.String" /> A URL-encoded string containing name/value pairs.</returns>

        public override string ToString()
        {
            List<string> kvPairs = new List<string>();

            foreach (KeyValuePair<string, string> kvp in mVariables)
            {
                string kvPair = kvp.Key + "=" + kvp.Value;
                kvPairs.Add(kvPair);
            }

            return string.Join("&", kvPairs.ToArray());
        }
    }
}