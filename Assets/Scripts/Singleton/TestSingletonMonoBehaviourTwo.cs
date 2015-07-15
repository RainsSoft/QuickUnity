using QuickUnity.Patterns;
using System.Collections;
using UnityEngine;

namespace QuickUnity.Examples.Singleton
{
    /// <summary>
    /// Class TestBehaviourSingletonTwo.
    /// </summary>
    [AddComponentMenu("")]
    public class TestSingletonMonoBehaviourTwo : SingletonMonoBehaviour<TestSingletonMonoBehaviourTwo>
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