using System.Collections;
using UnityEngine;

namespace QuickUnity.Examples.Singleton
{
    /// <summary>
    /// Class SingletonExample.
    /// </summary>
    public class SingletonExample : MonoBehaviour
    {
        // Use this for initialization
        private void Start()
        {
            TestSingletonMonoBehaviourOne testBehaivourOne = TestSingletonMonoBehaviourOne.instance;
            TestSingletonMonoBehaviourTwo testBehaivourTwo = TestSingletonMonoBehaviourTwo.instance;
            testBehaivourOne.Run();
            testBehaivourTwo.Run();
        }
    }
}