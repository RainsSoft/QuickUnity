using QuickUnity.DesignPattern;
using System.Collections;
using UnityEngine;

/// <summary>
/// The Singleton namespace.
/// </summary>
namespace QuickUnity.Examples.Singleton
{
    /// <summary>
    /// Class TestBehaviourSingletonTwo.
    /// </summary>
    public class TestBehaviourSingletonTwo : BehaviourSingleton<TestBehaviourSingletonTwo>
    {
        private TestSingletonTwo testTwo;

        private void Awake()
        {
            testTwo = TestSingletonTwo.Instance;
        }

        public void Run()
        {
            testTwo.Run();
        }
    }
}