using UnityEngine;

namespace AutumnYard
{
    public abstract class SingletonAsset<T> : ScriptableObject where T : ScriptableObject
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (!instance)
                {
                    instance = Resources.Load<T>(typeof(T).Name);
                }

#if UNITY_EDITOR
                if (!instance)
                {
                    Debug.LogError($"SingletonAsset: Can't find asset {typeof(T).Name}");
                }
#endif

                instance.hideFlags = HideFlags.DontUnloadUnusedAsset;
                return instance;
            }
        }

#if UNITY_EDITOR
        //[Title("Persistence")]
        //[Button, GUIColor(1, 1, 0), PropertyOrder(-9)]
        [NaughtyAttributes.Button]
        public new void SetDirty()
        {
            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif

    }
}
