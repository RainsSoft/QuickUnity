using System;
using System.Reflection;
using UnityEngine;

namespace QuickUnity.Patterns
{
    /// <summary>
    /// Singleton template class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Singleton<T>
    {
        /// <summary>
        /// The synchronize root.
        /// </summary>
        private static readonly object sSyncRoot = new object();

        /// <summary>
        /// The sInstance
        /// </summary>
        private static T sInstance;

        /// <summary>
        /// The sInstantiated
        /// </summary>
        private static bool sInstantiated;

        /// <summary>
        /// Gets the sInstance.
        /// </summary>
        /// <value>The sInstance.</value>
        public static T instance
        {
            get
            {
                if (!sInstantiated)
                {
                    lock (sSyncRoot)
                    {
                        if (!sInstantiated)
                        {
                            Type type = typeof(T);
                            ConstructorInfo ctor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                                null, new Type[0], new ParameterModifier[0]);
                            sInstance = (T)ctor.Invoke(new object[0]);
                            sInstantiated = true;
                        }
                    }
                }

                return sInstance;
            }
        }
    }

    /// <summary>
    /// Singleton template class for MonoBehaviour.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        /// <summary>
        /// The name of GameObject root.
        /// </summary>
        private const string GAME_OBJECTS_ROOT_NAME = "BehaviourSingleton Objects";

        /// <summary>
        /// The sInstance
        /// </summary>
        private static T sInstance = null;

        /// <summary>
        /// The sInstantiated
        /// </summary>
        private static bool sInstantiated = false;

        /// <summary>
        /// Gets the sInstance.
        /// </summary>
        /// <value>The sInstance.</value>
        public static T instance
        {
            get
            {
                if (sInstantiated)
                    return sInstance;

                Type type = typeof(T);
                UnityEngine.Object[] objects = FindObjectsOfType(type);

                if (objects.Length > 0)
                {
                    instance = (T)objects[0];

                    if (objects.Length > 1)
                    {
                        UnityEngine.Debug.LogWarning("There is more than one sInstance of Singleton of type \"" + type + "\". Keeping the first. Destroying the others.");

                        for (int i = 1, length = objects.Length; i < length; ++i)
                        {
                            MonoBehaviour behaviour = (MonoBehaviour)objects[i];
                            DestroyImmediate(behaviour.gameObject);
                        }
                    }

                    return sInstance;
                }

                // Find BehaviourSingletons root GameObject.
                GameObject root = GameObject.Find(GAME_OBJECTS_ROOT_NAME);

                // If can not find out this GameObject, then create one.
                if (root == null)
                {
                    root = new GameObject(GAME_OBJECTS_ROOT_NAME);
                    DontDestroyOnLoad(root);
                }

                // Find the GameObject who hold the singleton of this component.
                Transform singletonTrans = root.transform.FindChild(type.Name);

                if (singletonTrans == null)
                {
                    // Create a GameObject to add the component of this Singleton.
                    GameObject singletonGO = new GameObject(type.Name);
                    singletonGO.transform.parent = root.transform;
                    instance = singletonGO.AddComponent<T>();
                }
                else
                {
                    // If already has one, get it.
                    T component = singletonTrans.GetComponent<T>();

                    if (component != null)
                        instance = component;
                }

                return sInstance;
            }

            private set
            {
                sInstance = value;
                sInstantiated = value != null;
            }
        }
    }
}