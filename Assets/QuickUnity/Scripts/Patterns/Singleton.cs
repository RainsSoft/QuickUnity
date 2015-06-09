using System;
using System.Reflection;
using UnityEngine;

/// <summary>
/// The Patterns namespace.
/// </summary>
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
        private static readonly object syncRoot = new object();

        /// <summary>
        /// The instance
        /// </summary>
        private static T instance;

        /// <summary>
        /// The instantiated
        /// </summary>
        private static bool instantiated;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static T Instance
        {
            get
            {
                if (!instantiated)
                {
                    lock (syncRoot)
                    {
                        if (!instantiated)
                        {
                            Type type = typeof(T);
                            ConstructorInfo ctor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                                null, new Type[0], new ParameterModifier[0]);
                            instance = (T)ctor.Invoke(new object[0]);
                            instantiated = true;
                        }
                    }
                }

                return instance;
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
        /// The instance
        /// </summary>
        private static T instance = null;

        /// <summary>
        /// The instantiated
        /// </summary>
        private static bool instantiated = false;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static T Instance
        {
            get
            {
                if (instantiated)
                    return instance;

                Type type = typeof(T);
                UnityEngine.Object[] objects = FindObjectsOfType(type);

                if (objects.Length > 0)
                {
                    Instance = (T)objects[0];

                    if (objects.Length > 1)
                    {
                        UnityEngine.Debug.LogWarning("There is more than one instance of Singleton of type \"" + type + "\". Keeping the first. Destroying the others.");

                        for (int i = 1, length = objects.Length; i < length; ++i)
                        {
                            MonoBehaviour behaviour = (MonoBehaviour)objects[i];
                            DestroyImmediate(behaviour.gameObject);
                        }
                    }

                    return instance;
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
                    Instance = singletonGO.AddComponent<T>();
                }
                else
                {
                    // If already has one, get it.
                    T component = singletonTrans.GetComponent<T>();

                    if (component != null)
                        Instance = component;
                }

                return instance;
            }

            private set
            {
                instance = value;
                instantiated = value != null;
            }
        }
    }
}