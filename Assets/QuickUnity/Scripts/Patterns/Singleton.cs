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
        /// Used for locking the instance calls.
        /// </summary>
        private static readonly object mSyncRoot = new object();

        /// <summary>
        /// The static instance.
        /// </summary>
        private static T mInstance;

        /// <summary>
        /// A value indicating whether this <see cref="Singleton{T}"/> is instantiated.
        /// </summary>
        private static bool mInstantiated;

        /// <summary>
        /// Gets a value indicating whether this <see cref="Singleton{T}"/> is instantiated.
        /// </summary>
        /// <value><c>true</c> if instantiated; otherwise, <c>false</c>.</value>
        public static bool instantiated
        {
            get { return mInstantiated; }
        }

        /// <summary>
        /// Gets the static instance.
        /// </summary>
        /// <value>The static instance.</value>
        public static T instance
        {
            get
            {
                if (!mInstantiated)
                {
                    lock (mSyncRoot)
                    {
                        if (!mInstantiated)
                        {
                            Type type = typeof(T);
                            ConstructorInfo ctor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                                null, new Type[0], new ParameterModifier[0]);
                            mInstance = (T)ctor.Invoke(new object[0]);
                            mInstantiated = true;
                        }
                    }
                }

                return mInstance;
            }
        }
    }

    /// <summary>
    /// Singleton template class for MonoBehaviour.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        /// <summary>
        /// The name of GameObject root.
        /// </summary>
        private const string GAME_OBJECTS_ROOT_NAME = "SingletonMonoBehaviour GameObjects";

        /// <summary>
        /// The static instance.
        /// </summary>
        private static T mInstance = null;

        /// <summary>
        /// A value indicating whether this <see cref="SingletonMonoBehaviour{T}"/> is instantiated.
        /// </summary>
        private static bool mInstantiated = false;

        /// <summary>
        /// Gets a value indicating whether this <see cref="SingletonMonoBehaviour{T}"/> is instantiated.
        /// </summary>
        /// <value><c>true</c> if instantiated; otherwise, <c>false</c>.</value>
        public static bool instantiated
        {
            get { return mInstantiated; }
        }

        /// <summary>
        /// Gets the static instance.
        /// </summary>
        /// <value>The static instance.</value>
        public static T instance
        {
            get
            {
                if (mInstantiated)
                    return mInstance;

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

                    return mInstance;
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

                return mInstance;
            }

            private set
            {
                mInstance = value;
                mInstantiated = value != null;
            }
        }

        /// <summary>
        /// Destroy GameObject.
        /// </summary>
        public void Dispose()
        {
            if (gameObject != null)
                Destroy(gameObject);
        }
    }
}