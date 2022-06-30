using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace AutumnYard.Core.Assets
{
    [Serializable]
    public class ArrayAssetLocator<T>
      where T : UnityEngine.Object
    {
        [SerializeField] protected T[] data = null;
        private AsyncOperationHandle<IList<T>> asyncOp;

        public bool IsLoaded
        {
            get
            {
                if (data == null || data.Length == 0)
                    return false;

                // Ignore None, which is always 0
                for (int i = 1; i < data.Length; i++)
                {
                    if (data[i] == null) return false;
                }
                return true;
            }
        }


        public void Load<TEnum>(AssetLabelReference label) where TEnum : struct, Enum
        {
            if (data != null && data.Length > 0)
            {
                //Debug.Log($"Already loaded: {label.labelString}");
                return;
            }

            if (string.IsNullOrEmpty(label.labelString))
                throw new NullReferenceException($"Invalid AssetLabelReference");

            int length = Enum.GetValues(typeof(TEnum)).Length;

            //Debug.Log($"Begin loading {label.labelString}, into an array with size {length}");

            data = new T[length];
            asyncOp = Addressables.LoadAssetsAsync<T>(label, null);
            asyncOp.Completed += HandleComplete;

            void HandleComplete(AsyncOperationHandle<IList<T>> objects)
            {
                asyncOp.Completed -= HandleComplete;
                foreach (var go in objects.Result)
                {
                    string name = go.name;
                    string[] names = name.Split();
                    if (names.Length > 1)
                    {
                        name = names[names.Length - 1];
                    }
                    if (name.Convert<TEnum>(out var item, true))
                    {
                        //Debug.Log($"probando con {item}, del GO: {go.name}. Convierto a {(int)(object)item} que se corresponde con {(Constants.Item)(object)item}");
                        data[(int)(object)item] = go;
                    }
                }
                //Debug.Log($"  ... finished!");
            }
        }

        public T this[int index] => data[index];

        public void Unload()
        {
            if (!asyncOp.IsValid())
                return;

            if (Application.isPlaying)
                Addressables.Release(asyncOp);
        }
    }
}
