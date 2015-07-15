using QuickUnity.Patterns;
using System.Collections;
using UnityEngine;

namespace QuickUnity.Examples.Singleton
{
    /// <summary>
    /// Class TestBehaviourSingletonOne.
    /// </summary>
    [AddComponentMenu("")]
    public class TestSingletonMonoBehaviourOne : SingletonMonoBehaviour<TestSingletonMonoBehaviourOne>
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