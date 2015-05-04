using QuickUnity.Patterns;
using System.Collections;
using UnityEngine;

/// <summary>
/// The Singleton namespace.
/// </summary>
namespace QuickUnity.Examples.Singleton
{
    /// <summary>
    /// Class TestBehaviourSingletonOne.
    /// </summary>
    public class TestBehaviourSingletonOne : BehaviourSingleton<TestBehaviourSingletonOne>
    {
        private TestSingletonOne testOne;

        private void Awake()
        {
            testOne = TestSingletonOne.Instance;
        }

        public void Run()
        {
            testOne.Run();
        }
    }
}