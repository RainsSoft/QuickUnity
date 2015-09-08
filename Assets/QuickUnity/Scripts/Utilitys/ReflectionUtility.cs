using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace QuickUnity.Utilitys
{
    /// <summary>
    /// The utility class about reflection functions. This class cannot be inherited.
    /// </summary>
    public static class ReflectionUtility
    {
        /// <summary>
        /// Creates the class instance dynamically.
        /// </summary>
        /// <param name="className">Name of the class.</param>
        /// <returns>System.Object.</returns>
        public static object CreateClassInstance(string className)
        {
            if (!string.IsNullOrEmpty(className))
            {
                Type classType = Type.GetType(className);
                Assembly assembly = classType.Assembly;
                return assembly.CreateInstance(className, false);
            }

            return null;
        }

        /// <summary>
        /// Creates the class instance with arguments.
        /// </summary>
        /// <param name="className">Name of the class.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static object CreateClassInstance(string className, object[] args)
        {
            if (!string.IsNullOrEmpty(className))
            {
                Type classType = Type.GetType(className);
                Assembly assembly = classType.Assembly;
                return assembly.CreateInstance(className, false, BindingFlags.CreateInstance, null, args, null, null);
            }

            return null;
        }

        /// <summary>
        /// Invokes the method of the object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameters">The parameters.</param>
        public static void InvokeMethod(object obj, string methodName, object[] parameters = null)
        {
            if (obj != null && !string.IsNullOrEmpty(methodName))
            {
                Type type = obj.GetType();
                MethodInfo info = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                if (info != null)
                    info.Invoke(obj, parameters);
            }
        }

        /// <summary>
        /// Invokes the method of the object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="bindingAttr">The binding attribute.</param>
        /// <param name="parameters">The parameters.</param>
        public static void InvokeMethod(object obj, string methodName, BindingFlags bindingAttr, object[] parameters = null)
        {
            if (obj != null && !string.IsNullOrEmpty(methodName))
            {
                Type type = obj.GetType();
                MethodInfo info = type.GetMethod(methodName, bindingAttr);

                if (info != null)
                    info.Invoke(obj, parameters);
            }
        }

        /// <summary>
        /// Gets the object fields values.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>Dictionary&lt;System.String, System.Object&gt;.</returns>
        public static Dictionary<string, object> GetObjectFieldsValues(object obj)
        {
            if (obj != null)
            {
                Type type = obj.GetType();
                FieldInfo[] infos = type.GetFields();

                Dictionary<string, object> map = new Dictionary<string, object>();

                foreach (FieldInfo info in infos)
                    map.Add(info.Name, info.GetValue(obj));

                return map;
            }

            return null;
        }

        /// <summary>
        /// Gets the object fields values.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="bindingAttr">The binding attribute.</param>
        /// <returns>Dictionary&lt;System.String, System.Object&gt;.</returns>
        public static Dictionary<string, object> GetObjectFieldsValues(object obj, BindingFlags bindingAttr)
        {
            if (obj != null)
            {
                Type type = obj.GetType();
                FieldInfo[] infos = type.GetFields(bindingAttr);

                Dictionary<string, object> map = new Dictionary<string, object>();

                foreach (FieldInfo info in infos)
                    map.Add(info.Name, info.GetValue(obj));

                return map;
            }

            return null;
        }

        /// <summary>
        /// Gets the object field value.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>System.Object.</returns>
        public static object GetObjectFieldValue(object obj, string fieldName)
        {
            if (obj != null && !string.IsNullOrEmpty(fieldName))
            {
                Type type = obj.GetType();
                FieldInfo info = type.GetField(fieldName);

                if (info != null)
                    return info.GetValue(obj);
            }

            return null;
        }

        /// <summary>
        /// Gets the object field value.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="bindingAttr">The binding attribute.</param>
        /// <returns>System.Object.</returns>
        public static object GetObjectFieldValue(object obj, string fieldName, BindingFlags bindingAttr)
        {
            if (obj != null && !string.IsNullOrEmpty(fieldName))
            {
                Type type = obj.GetType();
                FieldInfo info = type.GetField(fieldName, bindingAttr);

                if (info != null)
                    return info.GetValue(obj);
            }

            return null;
        }

        /// <summary>
        /// Gets the object properties values.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>Dictionary&lt;System.String, System.Object&gt;.</returns>
        public static Dictionary<string, object> GetObjectPropertiesValues(object obj)
        {
            if (obj != null)
            {
                Type type = obj.GetType();
                PropertyInfo[] infos = type.GetProperties();

                Dictionary<string, object> map = new Dictionary<string, object>();

                foreach (PropertyInfo info in infos)
                    map.Add(info.Name, info.GetValue(obj, null));

                return map;
            }

            return null;
        }

        /// <summary>
        /// Gets the object properties values.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="bindingAttr">The binding attribute.</param>
        /// <returns>Dictionary&lt;System.String, System.Object&gt;.</returns>
        public static Dictionary<string, object> GetObjectPropertiesValues(object obj, BindingFlags bindingAttr)
        {
            if (obj != null)
            {
                Type type = obj.GetType();
                PropertyInfo[] infos = type.GetProperties(bindingAttr);

                Dictionary<string, object> map = new Dictionary<string, object>();

                foreach (PropertyInfo info in infos)
                    map.Add(info.Name, info.GetValue(obj, null));

                return map;
            }

            return null;
        }

        /// <summary>
        /// Gets the object property value.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>System.Object.</returns>
        public static object GetObjectPropertyValue(object obj, string propertyName)
        {
            if (obj != null && !string.IsNullOrEmpty(propertyName))
            {
                if (string.IsNullOrEmpty(propertyName))
                    return null;

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(propertyName);

                if (info != null)
                    return info.GetValue(obj, null);
            }

            return null;
        }

        /// <summary>
        /// Gets the object property value.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="bindingAttr">The binding attribute.</param>
        /// <returns>System.Object.</returns>
        public static object GetObjectPropertyValue(object obj, string propertyName, BindingFlags bindingAttr)
        {
            if (obj != null && !string.IsNullOrEmpty(propertyName))
            {
                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(propertyName, bindingAttr);

                if (info != null)
                    return info.GetValue(obj, null);
            }

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
            if (obj != null && !string.IsNullOrEmpty(propertyName))
            {
                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(propertyName);

                if (info != null)
                    info.SetValue(obj, value, null);
            }
        }
    }
}