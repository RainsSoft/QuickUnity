using QuickUnity.Events;
using System;
using System.Reflection;
using UnityEngine;

namespace QuickUnity.Patterns
{
    /// <summary>
    /// Singleton template class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Singleton<T> : EventDispatcher
    {
        /// <summary>
        /// Used for locking the instance calls.
        /// </summary>
        private static readonly object sSyncRoot = new object();

        /// <summary>
        /// The static instance.
        /// </summary>
        private static T sInstance;

        /// <summary>
        /// A value indicating whether this <see cref="Singleton{T}"/> is instantiated.
        /// </summary>
        private static bool sInstantiated;

        /// <summary>
        /// Gets a value indicating whether this <see cref="Singleton{T}"/> is instantiated.
        /// </summary>
        /// <value><c>true</c> if instantiated; otherwise, <c>false</c>.</value>
        public static bool instantiated
        {
            get { return sInstantiated; }
        }

        /// <summary>
        /// Gets the static instance.
        /// </summary>
        /// <value>The static instance.</value>
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
    /// SingletonMonoBehaviour utility class.
    /// </summary>
    public static class SingletonMonoBehaviourUtility
    {
        /// <summary>
        /// The name of GameObject root.
        /// </summary>
        public const string GAME_OBJECTS_ROOT_NAME = "Singleton MonoBehaviour GameObjects";

        /// <summary>
        /// Destroys all GameObject of SingletonMonoBehaviour.
        /// </summary>
        public static void DestroyAllSingletonMonoBehaviours()
        {
            GameObject targetGameObject = GameObject.Find(GAME_OBJECTS_ROOT_NAME);

            while (targetGameObject)
            {
                GameObject.Destroy(targetGameObject);
                targetGameObject = GameObject.Find(GAME_OBJECTS_ROOT_NAME);
            }
        }
    }

    /// <summary>
    /// Singleton template class for MonoBehaviour.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviourEventDispatcher where T : MonoBehaviourEventDispatcher
    {
        /// <summary>
        /// The static instance.
        /// </summary>
        private static T sInstance = null;

        /// <summary>
        /// If the application quit.
        /// </summary>
        private static bool sApplicationQuit = false;

        /// <summary>
        /// A value indicating whether this <see cref="SingletonMonoBehaviour{T}"/> is instantiated.
        /// </summary>
        private static bool sInstantiated = false;

        /// <summary>
        /// Gets a value indicating whether this <see cref="SingletonMonoBehaviour{T}"/> is instantiated.
        /// </summary>
        /// <value><c>true</c> if instantiated; otherwise, <c>false</c>.</value>
        public static bool instantiated
        {
            get { return sInstantiated; }
        }

        /// <summary>
        /// Gets the static instance.
        /// </summary>
        /// <value>The static instance.</value>
        public static T instance
        {
            get
            {
                if (sApplicationQuit)
                    return null;

                if (sInstantiated)
                    return sInstance;

                Type type = typeof(T);
                UnityEngine.Object[] objects = FindObjectsOfType(type);

                if (objects.Length > 0)
                {
                    instance = (T)objects[0];

                    if (objects.Length > 1)
                    {
                        Debug.LogWarning("There is more than one sInstance of Singleton of type \"" + type + "\". Keeping the first. Destroying the others.");

                        for (int i = 1, length = objects.Length; i < length; ++i)
                        {
                            MonoBehaviour behaviour = (MonoBehaviour)objects[i];
                            Destroy(behaviour.gameObject);
                        }
                    }

                    return sInstance;
                }

                // Find BehaviourSingletons root GameObject.
                GameObject root = GameObject.Find(SingletonMonoBehaviourUtility.GAME_OBJECTS_ROOT_NAME);

                // If can not find out this GameObject, then create one.
                if (root == null)
                {
                    root = new GameObject(SingletonMonoBehaviourUtility.GAME_OBJECTS_ROOT_NAME);
                    DontDestroyOnLoad(root);
                }

                // Find the GameObject who hold the singleton of this component.
                Transform singletonTrans = root.transform.FindChild(type.Name);

                if (singletonTrans == null)
                {
                    // Create a GameObject to add the component of this Singleton.
                    GameObject singletonGameObject = new GameObject(type.Name);
                    singletonGameObject.transform.parent = root.transform;
                    instance = singletonGameObject.AddComponent<T>();
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

                if (value != null)
                    DontDestroyOnLoad(sInstance);
            }
        }

        /// <summary>
        /// Destroy the gameObject self.
        /// </summary>
        public void Dispose()
        {
            if (gameObject != null)
                Destroy(gameObject);
        }

        /// <summary>
        /// Called when [destroy].
        /// </summary>
        protected override void OnDestroy()
        {
            base.OnDestroy();

            instance = null;
        }

        /// <summary>
        /// Called when [application quit].
        /// </summary>
        protected virtual void OnApplicationQuit()
        {
            sApplicationQuit = true;
        }
    }
}