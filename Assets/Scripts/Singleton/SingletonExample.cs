﻿using System.Collections;
using UnityEngine;

/// <summary>
/// The Event namespace.
/// </summary>
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
            TestBehaviourSingletonOne testBehaivourOne = TestBehaviourSingletonOne.instance;
            TestBehaviourSingletonTwo testBehaivourTwo = TestBehaviourSingletonTwo.instance;
            testBehaivourOne.Run();
            testBehaivourTwo.Run();
        }
    }
}