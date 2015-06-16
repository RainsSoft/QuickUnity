using System;
using System.Collections;
using System.Reflection;

namespace QuickUnity.Utilitys
{
    /// <summary>
    /// The utility class about reflection functions. This class cannot be inherited.
    /// </summary>
    public sealed class ReflectionUtility
    {
        /// <summary>
        /// Creates the class sInstance dynamically.
        /// </summary>
        /// <param name="className">Name of the class.</param>
        /// <returns>System.Object.</returns>
        public static object CreateClassInstance(string className)
        {
            Type classType = Type.GetType(className);
            Assembly assembly = classType.Assembly;
            return assembly.CreateInstance(className);
        }

        /// <summary>
        /// Invokes the method of the object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameters">The parameters.</param>
        public static void InvokeMethod(object obj, string methodName, params object[] parameters)
        {
            Type type = obj.GetType();
            MethodInfo info = type.GetMethod(methodName);

            if (info != null)
                info.Invoke(obj, parameters);
        }

        /// <summary>
        /// Gets the object property value.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>System.Object.</returns>
        public static object GetObjectPropertyValue(object obj, string propertyName)
        {
            Type type = obj.GetType();
            PropertyInfo info = type.GetProperty(propertyName);

            if (info != null)
                return info.GetValue(obj, null);

            return null;
        }

        /// <summary>
        /// Sets the object property value.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">The value.</param>
        public static void SetObjectPropertyValue(object obj, string propertyName, object value)
        {
            Type type = obj.GetType();
            PropertyInfo info = type.GetProperty(propertyName);

            if (info != null)
                info.SetValue(obj, value, null);
        }
    }
}