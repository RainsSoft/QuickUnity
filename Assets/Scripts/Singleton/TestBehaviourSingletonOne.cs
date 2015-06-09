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
        private TestSingletonOne mTestOne;

        private void Awake()
        {
            mTestOne = TestSingletonOne.instance;
        }

        public void Run()
        {
            mTestOne.Run();
        }
    }
}