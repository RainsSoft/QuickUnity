using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace QuickUnity
{
    /// <summary>
    /// Class UnityClassExtensions.
    /// </summary>
    public static class UnityClassExtensions
    {
        #region GameObject

        /// <summary>
        /// Get or add component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gameObject">The game object.</param>
        /// <returns>T.</returns>
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            T component = gameObject.GetComponent<T>();

            if (component == null)
                component = gameObject.AddComponent<T>();

            return component;
        }

        /// <summary>
        /// Removes the component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gameObject">The game object.</param>
        /// <param name="immediate">if set to <c>true</c> [use Unity3D API GameObject.DestroyImmediate to do].</param>
        public static void RemoveComponent<T>(this GameObject gameObject, bool immediate = false) where T : Component
        {
            T component = gameObject.GetComponent<T>();

            if (component != null)
            {
                if (immediate)
                    GameObject.DestroyImmediate(component);
                else
                    GameObject.Destroy(component);
            }
        }

        /// <summary>
        /// Gets all components of the GameObject.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        /// <returns>Component[].</returns>
        public static Component[] GetAllComponents(this GameObject gameObject)
        {
            if (gameObject == null)
                return null;

            return gameObject.GetComponents(typeof(Component));
        }

        /// <summary>
        /// Copies all components.
        /// </summary>
        /// <param name="source">The source GameObject.</param>
        /// <param name="target">The target GameObject.</param>
        /// <param name="exceptTransform">if set to <c>true</c> [except Transform component].</param>
        public static void CopyAllComponents(this GameObject source, GameObject target, bool exceptTransform = true)
        {
            if (target == null)
                return;

            Component[] components = source.GetAllComponents();

            foreach (Component component in components)
            {
                if (exceptTransform && component.GetType().Name == "Transform")
                    continue;

                component.CopyComponent(target);
            }
        }

        /// <summary>
        /// Finds the game object in children.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="targetObjectName">Name of the target object.</param>
        /// <param name="includeInactive">if set to <c>true</c> [include inactive GameObject].</param>
        /// <returns></returns>
        public static GameObject FindGameObjectInChildren(this GameObject source, string targetObjectName, bool includeInactive = true)
        {
            Transform sourceTransform = source.transform;

            foreach (Transform childTransform in sourceTransform)
            {
                if (!includeInactive && !childTransform.gameObject.activeSelf)
                    continue;

                if (childTransform.name == targetObjectName)
                    return childTransform.gameObject;

                GameObject targetGameObject = childTransform.gameObject.FindGameObjectInChildren(targetObjectName, includeInactive);

                if (targetGameObject != null)
                    return targetGameObject;
            }

            return null;
        }

        /// <summary>
        /// Sets the layer in children.
        /// </summary>
        /// <param name="source">The source GameObject.</param>
        /// <param name="layer">The layer.</param>
        /// <param name="includeParent">if set to <c>true</c> [include parent].</param>
        /// <param name="includeInactive">if set to <c>true</c> [include inactive GameObject].</param>
        public static void SetLayerInChildren(this GameObject source, int layer = -1, bool includeParent = true, bool includeInactive = true)
        {
            if (layer >= 0)
            {
                if (includeParent)
                    source.layer = layer;

                Transform sourceTransform = source.transform;

                foreach (Transform childTransform in sourceTransform)
                {
                    if (!includeInactive && !childTransform.gameObject.activeSelf)
                        continue;

                    childTransform.gameObject.layer = layer;
                    childTransform.gameObject.SetLayerInChildren(layer, includeParent, includeInactive);
                }
            }
        }

        /// <summary>
        /// Sets the layer of all children.
        /// </summary>
        /// <param name="source">The source GameObject.</param>
        /// <param name="layerName">Name of the layer.</param>
        /// <param name="includeParent">if set to <c>true</c> [set layer include parent].</param>
        /// <param name="includeInactive">if set to <c>true</c> [include inactive GameObject].</param>
        public static void SetLayerInChildren(this GameObject source, string layerName, bool includeParent = true, bool includeInactive = true)
        {
            if (!string.IsNullOrEmpty(layerName))
            {
                int layer = LayerMask.NameToLayer(layerName);
                source.gameObject.SetLayerInChildren(layer, includeParent, includeInactive);
            }
        }

        #endregion GameObject

        #region Component

        /// <summary>
        /// Copies the component.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        public static void CopyComponent(this Component source, GameObject target)
        {
            if (source == null || target == null)
                return;

            Type type = source.GetType();
            Component copy = target.AddComponent(type);
            FieldInfo[] fields = type.GetFields();
            PropertyInfo[] properties = type.GetProperties();

            foreach (FieldInfo fieldInfo in fields)
            {
                if (!fieldInfo.IsLiteral)
                    fieldInfo.SetValue(copy, fieldInfo.GetValue(source));
            }

            foreach (PropertyInfo propertyInfo in properties)
            {
                propertyInfo.SetValue(copy, propertyInfo.GetValue(source, null), null);
            }
        }

        #endregion Component

        #region Vector3

        /// <summary>
        /// If this Vector3 object strictly equals other Vector3 object.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="other">The other.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool StrictlyEquals(this Vector3 self, Vector3 other)
        {
            if (self.ToString() == other.ToString())
                return true;

            return false;
        }

        /// <summary>
        /// Return the strict string of this Vector3 object.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <returns>System.String.</returns>
        /// <param name="decimalDigits">The decimal digits.</param>
        /// <returns>System.String.</returns>
        public static string StrictlyToString(this Vector3 self, int decimalDigits = 4)
        {
            string format = "f" + decimalDigits.ToString();
            return "(" + self.x.ToString(format) + ", " + self.y.ToString(format) + ", " + self.z.ToString(format) + ")";
        }

        #endregion Vector3

        #region Transform

        /// <summary>
        /// Removes the component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="transform">The transform of a game object.</param>
        public static void RemoveComponent<T>(this Transform transform) where T : Component
        {
            T component = transform.GetComponent<T>();

            if (component != null)
                GameObject.Destroy(component);
        }

        /// <summary>
        /// Gets the position x.
        /// </summary>
        /// <param name="transform">The transform of a game object.</param>
        /// <returns>System.Single.</returns>
        public static float GetPositionX(this Transform transform)
        {
            return transform.position.x;
        }

        /// <summary>
        /// Sets the position x.
        /// </summary>
        /// <param name="transform">The transform of a game object.</param>
        /// <param name="value">The value.</param>
        public static void SetPositionX(this Transform transform, float value)
        {
            if (transform.position.x != value)
                transform.position = new Vector3(value, transform.position.y, transform.position.z);
        }

        /// <summary>
        /// Gets the position y.
        /// </summary>
        /// <param name="transform">The transform of a game object.</param>
        /// <returns>System.Single.</returns>
        public static float GetPositionY(this Transform transform)
        {
            return transform.position.y;
        }

        /// <summary>
        /// Sets the position y.
        /// </summary>
        /// <param name="transform">The transform of a game object.</param>
        /// <param name="value">The value.</param>
        public static void SetPositionY(this Transform transform, float value)
        {
            if (transform.position.y != value)
                transform.position = new Vector3(transform.position.x, value, transform.position.z);
        }

        /// <summary>
        /// Gets the position z.
        /// </summary>
        /// <param name="transform">The transform of a game object.</param>
        /// <returns>System.Single.</returns>
        public static float GetPositionZ(this Transform transform)
        {
            return transform.position.z;
        }

        /// <summary>
        /// Sets the position z.
        /// </summary>
        /// <param name="transform">The transform of a game object.</param>
        /// <param name="value">The value.</param>
        public static void SetPositionZ(this Transform transform, float value)
        {
            if (transform.position.z != value)
                transform.position = new Vector3(transform.position.x, transform.position.y, value);
        }

        /// <summary>
        /// Gets the local position x.
        /// </summary>
        /// <param name="transform">The transform of a game object.</param>
        /// <returns>System.Single.</returns>
        public static float GetLocalPositionX(this Transform transform)
        {
            return transform.localPosition.x;
        }

        /// <summary>
        /// Sets the local position x.
        /// </summary>
        /// <param name="transform">The transform of a game object.</param>
        /// <param name="value">The value.</param>
        public static void SetLocalPositionX(this Transform transform, float value)
        {
            if (transform.localPosition.x != value)
                transform.localPosition = new Vector3(value, transform.localPosition.y, transform.localPosition.z);
        }

        /// <summary>
        /// Gets the local position y.
        /// </summary>
        /// <param name="transform">The transform of a game object.</param>
        /// <returns>System.Single.</returns>
        public static float GetLocalPositionY(this Transform transform)
        {
            return transform.localPosition.y;
        }

        /// <summary>
        /// Sets the local position y.
        /// </summary>
        /// <param name="transform">The transform of a game object.</param>
        /// <param name="value">The value.</param>
        public static void SetLocalPositionY(this Transform transform, float value)
        {
            if (transform.localPosition.y != value)
                transform.localPosition = new Vector3(transform.localPosition.x, value, transform.localPosition.z);
        }

        /// <summary>
        /// Gets the local position z.
        /// </summary>
        /// <param name="transform">The transform of a game object.</param>
        /// <returns>System.Single.</returns>
        public static float GetLocalPositionZ(this Transform transform)
        {
            return transform.localPosition.z;
        }

        /// <summary>
        /// Sets the local position z.
        /// </summary>
        /// <param name="transform">The transform of a game object.</param>
        /// <param name="value">The value.</param>
        public static void SetLocalPositionZ(this Transform transform, float value)
        {
            if (transform.localPosition.z != value)
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, value);
        }

        /// <summary>
        /// Finds the transform of object in children.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="targetTransformName">Name of the target transform.</param>
        /// <param name="includeInactive">if set to <c>true</c> [include inactive GameObject].</param>
        /// <returns></returns>
        public static Transform FindTransformInChildren(this Transform source, string targetTransformName, bool includeInactive = true)
        {
            foreach (Transform childTransform in source)
            {
                if (!includeInactive && !childTransform.gameObject.activeSelf)
                    continue;

                if (childTransform.name == targetTransformName)
                    return childTransform;

                Transform targetTransform = childTransform.FindTransformInChildren(targetTransformName, includeInactive);

                if (targetTransform != null)
                    return targetTransform;
            }

            return null;
        }

        /// <summary>
        /// Finds the transforms in children contain key.
        /// </summary>
        /// <param name="source">The source transform.</param>
        /// <param name="key">The key.</param>
        /// <param name="list">The list.</param>
        /// <param name="exclude">The exclude transform.</param>
        /// <param name="includeInactive">if set to <c>true</c> [include inactive object].</param>
        public static void FindTransformsInChildrenContainKey(this Transform source, string key, ref List<Transform> list, Transform exclude, bool includeInactive = true)
        {
            foreach (Transform childTransform in source)
            {
                if (childTransform == exclude)
                    continue;

                if (!includeInactive && !childTransform.gameObject.activeSelf)
                    continue;

                if (childTransform.name.IndexOf(key) != -1)
                    list.Add(childTransform);

                childTransform.FindTransformsInChildrenContainKey(key, ref list, exclude, includeInactive);
            }
        }

        #endregion Transform
    }
}