using System;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Based on the implementation by: Incontrol (Gallant games)

namespace AutumnYard
{
    public abstract class SingletonComponent<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        private static bool hasInstance;
        private static int instanceId;
        private static readonly object lockObject = new object();


        public static T Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (hasInstance)
                    {
                        return instance;
                    }

                    instance = FindFirstInstance();
                    if (instance == null)
                    {
                        if (!Application.isPlaying)
                        {
                            throw new Exception($"We're not in Play, and couldn't find an instance for {typeof(T)}. Therefore, we don't create one.");
                        }

                        CreateNewInstance();
                    }

                    if (instance == null)
                    {
                        throw new Exception("The instance of singleton component " + typeof(T) + " was requested, but it doesn't appear to exist in the scene.");
                    }

                    hasInstance = true;
                    instanceId = instance.GetInstanceID();
                    return instance;
                }
            }
        }

        private static void CreateNewInstance()
        {
            instance = new GameObject(typeof(T).Name).gameObject.AddComponent<T>();
            DontDestroyOnLoad(instance);
        }


        /// <summary>
        /// Returns true if the object is NOT the singleton instance and should exit early from doing any redundant work.
        /// It will also log a warning if called from another instance in the editor during play mode.
        /// </summary>
        protected bool EnforceSingleton
        {
            get
            {
                if (GetInstanceID() == Instance.GetInstanceID())
                {
                    return false;
                }

                if (Application.isPlaying)
                {
                    enabled = false;
                }

                return true;
            }
        }


        /// <summary>
        /// Returns true if the object is the singleton instance.
        /// </summary>
        protected bool IsTheSingleton
        {
            get
            {
                lock (lockObject)
                {
                    // We compare against the last known instance ID because Unity destroys objects
                    // in random order and this may get called during teardown when the instance is
                    // already gone.
                    return GetInstanceID() == instanceId;
                }
            }
        }


        /// <summary>
        /// Returns true if the object is not the singleton instance.
        /// </summary>
        protected bool IsNotTheSingleton
        {
            get
            {
                lock (lockObject)
                {
                    // We compare against the last known instance ID because Unity destroys objects
                    // in random order and this may get called during teardown when the instance is
                    // already gone.
                    return GetInstanceID() != instanceId;
                }
            }
        }

        private static T[] FindInstances()
        {
            var objects = FindObjectsOfType<T>();
            Array.Sort(objects, (a, b) => a.transform.GetSiblingIndex().CompareTo(b.transform.GetSiblingIndex()));
            return objects;
        }

        private static T FindFirstInstance()
        {
            var objects = FindInstances();
            return objects.Length > 0 ? objects[0] : null;
        }

        // ReSharper disable once VirtualMemberNeverOverridden.Global
        private void Awake()
        {
            //Logger.Log($" --- Awake: {name}");

            if (Application.isPlaying && Instance != null)
            {
                if (GetInstanceID() != instanceId)
                {
#if UNITY_EDITOR
                    Debug.LogWarning("A redundant instance (" + name + ") of singleton " + typeof(T) + " is present in the scene.", this);
                    EditorGUIUtility.PingObject(this);
#endif
                    enabled = false;
                }

                // This might be unnecessary, but just to be safe we do it anyway.
                foreach (var redundantInstance in FindInstances().Where(o => o.GetInstanceID() != instanceId))
                {
                    redundantInstance.enabled = false;
                }
            }

            DoAwake();
        }

        protected virtual void DoAwake() { }

        // ReSharper disable once VirtualMemberNeverOverridden.Global
        protected virtual void OnDestroy()
        {
            lock (lockObject)
            {
                if (GetInstanceID() == instanceId)
                {
                    hasInstance = false;
                }
            }
        }
    }
}
