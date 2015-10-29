using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;

namespace QuickUnity.Utilitys
{
    /// <summary>
    /// The utility class about reflection functions. This class cannot be inherited.
    /// </summary>
    public static class ReflectionUtility
    {
        /// <summary>
        /// Gets the type of type name.
        /// </summary>
        /// <param name="typeName">Name of the type.</param>
        /// <returns></returns>
        public static Type GetType(string typeName)
        {
            Type type = Type.GetType(typeName);

            if (type == null)
            {
                //Reload project dll.
                Assembly assembly = Assembly.Load("Assembly-CSharp");
                type = assembly.GetType(typeName);
            }

            return type;
        }

        /// <summary>
        /// Gets the type of the property.
        /// </summary>
        /// <param name="propertyType">Type of the property.</param>
        /// <returns>The type of property</returns>
        public static Type GetPropertyType(Type propertyType)
        {
            Type type = propertyType;
            if (type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(Nullable<>)))
                return Nullable.GetUnderlyingType(type);
            return type;
        }

        /// <summary>
        /// Casts the value.
        /// </summary>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="valueType">Type of the value.</param>
        /// <param name="value">The value.</param>
        /// <returns>The new object of target type.</returns>
        public static object CastValue(Type targetType, Type valueType, object value)
        {
            if (targetType.Equals(valueType))
            {
                return value;
            }
            else
            {
                if (targetType.IsGenericType)
                {
                    if (targetType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        if (value == null)
                            return null;
                        else if (valueType.Equals(typeof(string)) && (string)value == string.Empty)
                            return null;
                    }
                    targetType = GetPropertyType(targetType);
                }

                if (targetType.IsEnum && valueType.Equals(typeof(string)))
                    return Enum.Parse(targetType, value.ToString());

                if (targetType.IsPrimitive && valueType.Equals(typeof(string)) && string.IsNullOrEmpty((string)value))
                    value = 0;

                try
                {
                    return Convert.ChangeType(value, GetPropertyType(targetType));
                }
                catch (Exception ex)
                {
                    TypeConverter cnv = TypeDescriptor.GetConverter(GetPropertyType(targetType));
                    if (cnv != null && cnv.CanConvertFrom(value.GetType()))
                        return cnv.ConvertFrom(value);
                    else
                        throw ex;
                }
            }
        }

        /// <summary>
        /// Creates the instance of class.
        /// </summary>
        /// <param name="typeFullName">Full name of the type.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static object CreateClassInstance(string typeFullName, object[] args = null)
        {
            if (!string.IsNullOrEmpty(typeFullName))
            {
                Type type = GetType(typeFullName);
                return CreateClassInstance(type, args);
            }

            return null;
        }

        /// <summary>
        /// Creates the instance of type.
        /// </summary>
        /// <param name="type">The type of object.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static object CreateClassInstance(Type type, object[] args = null)
        {
            if (type != null)
            {
                Assembly assembly = type.Assembly;
                return assembly.CreateInstance(type.FullName, false, BindingFlags.CreateInstance, null, args, null, null);
            }

            return null;
        }

        /// <summary>
        /// Determines whether the specified object has method.
        /// </summary>
        /// <param name="objName">The object.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <returns>If the specified object has method, it will return true; otherwise return false.</returns>
        public static bool HasMethod(string objName, string methodName, BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
        {
            if (!string.IsNullOrEmpty(objName) && !string.IsNullOrEmpty(methodName))
            {
                Type type = Type.GetType(objName);

                if (type == null)
                    return false;

                MethodInfo info = type.GetMethod(methodName, bindingAttr);

                if (info != null)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Invokes the method of the object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The object of method return.</returns>
        public static object InvokeMethod(object obj, string methodName, object[] parameters = null)
        {
            return InvokeMethod(obj, methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, parameters);
        }

        /// <summary>
        /// Invokes the method of the object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="bindingAttr">The binding attribute.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The object of method return.</returns>
        public static object InvokeMethod(object obj, string methodName, BindingFlags bindingAttr, object[] parameters = null)
        {
            if (obj != null && !string.IsNullOrEmpty(methodName))
            {
                Type type = obj.GetType();
                MethodInfo info = type.GetMethod(methodName, bindingAttr);

                if (info != null)
                    return info.Invoke(obj, parameters);
            }

            return null;
        }

        /// <summary>
        /// Invokes the generic method.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="genericType">Type of the generic.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="bindingAttr">The binding attribute.</param>
        /// <returns>
        /// The object of method return.
        /// </returns>
        public static object InvokeGenericMethod(object obj,
            string methodName,
            Type genericType,
            object[] parameters = null,
            BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
        {
            if (obj != null && !string.IsNullOrEmpty(methodName))
            {
                Type type = obj.GetType();
                MethodInfo[] methods = type.GetMethods();

                if (methods != null && methods.Length > 0)
                {
                    foreach (MethodInfo info in methods)
                    {
                        if (info.IsGenericMethod && info.Name == methodName)
                        {
                            MethodInfo genericInfo = info.MakeGenericMethod(genericType);

                            if (genericInfo != null)
                                return genericInfo.Invoke(obj, parameters);
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Invokes the method.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="bindingAttr">The binding attribute.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// The object of method return.
        /// </returns>
        public static object InvokeStaticMethod(Type type,
            string methodName,
            object[] parameters = null)
        {
            MethodInfo info = type.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

            if (info != null)
                return info.Invoke(null, parameters);

            return null;
        }

        /// <summary>
        /// Invokes the static generic method.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="genericType">Type of the generic.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The object of method return.</returns>
        public static object InvokeStaticGenericMethod(Type type,
            string methodName,
            Type genericType,
            object[] parameters = null)
        {
            if (!string.IsNullOrEmpty(methodName))
            {
                MethodInfo[] methods = type.GetMethods(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);

                if (methods != null && methods.Length > 0)
                {
                    foreach (MethodInfo info in methods)
                    {
                        if (info.IsGenericMethod && info.Name == methodName)
                        {
                            MethodInfo genericInfo = info.MakeGenericMethod(genericType);

                            if (genericInfo != null)
                                return genericInfo.Invoke(null, parameters);
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the object fields values.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>Dictionary&lt;System.String, System.Object&gt;.</returns>
        public static Dictionary<string, object> GetObjectFieldsValues(object obj)
        {
            return GetObjectFieldsValues(obj, BindingFlags.Instance | BindingFlags.Public);
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
            return GetObjectFieldValue(obj, fieldName, BindingFlags.Instance | BindingFlags.Public);
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
        /// Sets the object field value.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="value">The value.</param>
        public static void SetObjectFieldValue(object obj, string fieldName, object value)
        {
            if (obj != null && !string.IsNullOrEmpty(fieldName))
            {
                Type type = obj.GetType();
                FieldInfo info = type.GetField(fieldName);

                if (info != null)
                    info.SetValue(obj, value);
            }
        }

        /// <summary>
        /// Gets the object properties values.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>Dictionary&lt;System.String, System.Object&gt;.</returns>
        public static Dictionary<string, object> GetObjectPropertiesValues(object obj)
        {
            return GetObjectPropertiesValues(obj, BindingFlags.Instance | BindingFlags.Public);
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
            return GetObjectPropertyValue(obj, propertyName, BindingFlags.Instance | BindingFlags.Public);
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