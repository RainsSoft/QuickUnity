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

        protected override void Awake()
        {
            base.Awake();
            mTestOne = TestSingletonOne.instance;
        }

        public void Run()
        {
            mTestOne.Run();
        }
    }
}