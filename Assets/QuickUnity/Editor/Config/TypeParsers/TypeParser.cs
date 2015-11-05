using QuickUnity.Utilitys;
using System;
using System.Globalization;
using System.Threading;

namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The type parser base class.
    /// </summary>
    public abstract class TypeParser : ITypeParser
    {
        #region API

        /// <summary>
        /// Parses the type string.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        /// The parsed type string.
        /// </returns>
        public virtual string ParseType(string source)
        {
            return source;
        }

        /// <summary>
        /// Parses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The parsed value.
        /// </returns>
        public abstract object ParseValue(string value);

        #endregion API

        #region Protected Functions

        /// <summary>
        /// Parses the bool string.
        /// </summary>
        /// <param name="value">The value of string.</param>
        /// <returns>The bool value.</returns>
        protected bool ParseBool(string value)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(value))
            {
                TextInfo textInfo = Thread.CurrentThread.CurrentCulture.TextInfo;
                value = textInfo.ToTitleCase(value);
                bool.TryParse(value, out result);
            }

            return result;
        }

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