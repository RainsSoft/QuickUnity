using QuickUnity.Patterns;
using System.Collections;
using UnityEngine;

namespace QuickUnity.Examples.Singleton
{
    /// <summary>
    /// Class TestBehaviourSingletonTwo.
    /// </summary>
    public class TestBehaviourSingletonTwo : BehaviourSingleton<TestBehaviourSingletonTwo>
    {
        private TestSingletonTwo mTestTwo;

        private void Awake()
        {
            mTestTwo = TestSingletonTwo.instance;
        }

        public void Run()
        {
            mTestTwo.Run();
        }
    }
}