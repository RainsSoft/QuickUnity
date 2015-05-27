using UnityEngine;

/// <summary>
/// The Utilitys namespace.
/// </summary>
namespace QuickUnity.Utilitys
{
    /// <summary>
    /// Class VectorUtility. This class cannot be inherited.
    /// </summary>
    public sealed class VectorUtility
    {
        /// <summary>
        /// Gets the angle direction.
        /// </summary>
        /// <param name="forward">The forward.</param>
        /// <param name="targetDirection">The target direction.</param>
        /// <param name="up">Up.</param>
        /// <returns>System.Int32. If it is -1 then means left to the object, else it is 1 then means right to the object.</returns>
        public static int GetAngleDirection(Vector3 forward, Vector3 targetDirection, Vector3 up)
        {
            if (up == null)
                up = Vector3.up;

            Vector3 prep = Vector3.Cross(forward, targetDirection);
            float dir = Vector3.Dot(prep, up);

            if (dir > 0)
                return 1;
            else if (dir < 0)
                return -1;
            else
                return 0;
        }
    }
}