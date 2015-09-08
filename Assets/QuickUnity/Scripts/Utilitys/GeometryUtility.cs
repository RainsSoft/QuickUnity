using UnityEngine;

namespace QuickUnity.Utilitys
{
    /// <summary>
    /// Utility class for common geometric functions.
    /// </summary>
    public static class GeometryUtility
    {
        /// <summary>
        /// Gets the angle between objet A and B.
        /// </summary>
        /// <param name="a">The position of A.</param>
        /// <param name="b">The position of B.</param>
        /// <returns></returns>
        public static float GetAngle(Vector3 a, Vector3 b)
        {
            return Mathf.Acos(Vector3.Dot(a.normalized, b.normalized)) * Mathf.Rad2Deg;
        }
    }
}