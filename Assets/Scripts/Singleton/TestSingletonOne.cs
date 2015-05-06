using QuickUnity.Patterns;
using UnityEngine;

/// <summary>
/// The Singleton namespace.
/// </summary>
namespace QuickUnity.Examples.Singleton
{
    /// <summary>
    /// Class TestSingletonOne.
    /// </summary>
    public class TestSingletonOne : Singleton<TestSingletonOne>
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="TestSingletonOne"/> class from being created.
        /// </summary>
        private TestSingletonOne()
        {
            UnityEngine.Debug.Log("TestSingletonOne");
        }

        public void Run()
        {
            UnityEngine.Debug.Log("TestSingletonOne Run!");
        }
    }
}