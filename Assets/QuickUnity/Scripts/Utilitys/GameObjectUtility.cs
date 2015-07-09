using System;
using System.Reflection;
using UnityEngine;

namespace QuickUnity.Utilitys
{
    /// <summary>
    /// Class GameObjectUtility. This class cannot be inherited.
    /// </summary>
    public sealed class GameObjectUtility
    {
        /// <summary>
        /// Copies the component.
        /// </summary>
        /// <param name="source">The source Component.</param>
        /// <param name="target">The target game object.</param>
        //public static void CopyComponent(Component source, GameObject target)
        //{
        //    if (source == null || target == null)
        //        return;

        //    Type type = source.GetType();
        //    Component copy = target.AddComponent(type);
        //    FieldInfo[] fields = type.GetFields();

        //    foreach (FieldInfo fieldInfo in fields)
        //    {
        //        if (!fieldInfo.IsLiteral)
        //            fieldInfo.SetValue(copy, fieldInfo.GetValue(source));
        //    }
        //}

        /// <summary>
        /// Copies all components.
        /// </summary>
        /// <param name="source">The source GameObject.</param>
        /// <param name="target">The target GameObject.</param>
        /// <param name="exceptTransform">if set to <c>true</c> [except Transform component].</param>
        //public static void CopyAllComponents(GameObject source, GameObject target, bool exceptTransform = true)
        //{
        //    if (source == null || target == null)
        //        return;

        //    Component[] components = GetAllComponents(source);

        //    foreach (Component component in components)
        //    {
        //        if (exceptTransform && component.GetType().Name == "Transform")
        //            continue;

        //        CopyComponent(component, target);
        //    }
        //}
    }
}