using QuickUnity.Utilitys;
using System;
using System.Globalization;

namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The type parser base class.
    /// </summary>
    public abstract class TypeParser : ITypeParser
    {
        /// <summary>
        /// Parses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The parsed value.
        /// </returns>
        public abstract object Parse(string value);

        #region Protected Functions

        /// <summary>
        /// Parses the specified value.
        /// </summary>
        /// <typeparam name="T">The type of object</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The converted object.</returns>
        protected T Parse<T>(string value)
        {
            T result = default(T);
            Type targetType = typeof(T);
            object[] args = new object[4] { value, NumberStyles.Any, CultureInfo.InvariantCulture, result };

            if (targetType != null && !string.IsNullOrEmpty(value))
            {
                ReflectionUtility.InvokeStaticMethod(targetType, "TryParse", new Type[4] {
                    typeof(string), typeof(NumberStyles), typeof(CultureInfo), targetType.MakeByRefType()
                }, ref args);
            }

            return (T)args[3];
        }

        #endregion Protected Functions
    }
}