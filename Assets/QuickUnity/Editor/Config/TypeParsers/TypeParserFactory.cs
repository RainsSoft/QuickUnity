using QuickUnity.Utilitys;
using System;

namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The factory of type parser.
    /// </summary>
    public static class TypeParserFactory
    {
        /// <summary>
        /// Creates the type parser.
        /// </summary>
        /// <param name="parserName">Name of the parser.</param>
        /// <returns>The instance of type parser.</returns>
        public static ITypeParser CreateTypeParser(Type type)
        {
            if (type != null)
            {
                ITypeParser parser = (ITypeParser)ReflectionUtility.CreateClassInstance(type);
                return parser;
            }

            return null;
        }
    }
}