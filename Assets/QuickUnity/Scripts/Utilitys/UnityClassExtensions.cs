using System.Collections;
using UnityEngine;

namespace QuickUnity.Utilitys
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
        public static void RemoveComponent<T>(this GameObject gameObject) where T : Component
        {
            T component = gameObject.GetComponent<T>();

            if (component != null)
                GameObject.Destroy(component);
        }

        #endregion GameObject

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

        #endregion Transform
    }
}