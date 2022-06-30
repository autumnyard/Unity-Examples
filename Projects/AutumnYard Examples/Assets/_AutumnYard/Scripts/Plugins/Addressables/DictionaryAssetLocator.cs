using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace AutumnYard.Core.Assets
{
    [Serializable]
    public class DictionaryAssetLocator<T>
      where T : UnityEngine.Object
    {
        [SerializeField] protected Dictionary<string, T> data = null;
        private AsyncOperationHandle<IList<T>> asyncOp;

        public bool IsLoaded
        {
            get
            {
                if (data == null || data.Count == 0)
                    return false;

                foreach (var item in data)
                {
                    if (item.Value != null) return false;

                }
                return true;
            }
        }

        public void Load(AssetLabelReference label)
        {
            if (data != null && data.Count > 0)
            {
                //Debug.Log($"Already loaded: {label.labelString}");
                return;
            }

            if (string.IsNullOrEmpty(label.labelString)) throw new NullReferenceException($"Invalid AssetLabelReference");

            //Debug.Log($"Begin loading {label.labelString}...");

            data = new Dictionary<string, T>();
            asyncOp = Addressables.LoadAssetsAsync<T>(label, null);
            asyncOp.Completed += objects =>
            {
                if (objects.Status == AsyncOperationStatus.Failed) Debug.Log(" -------------- PROBANDO LOAD FAILED --------------");
                if (objects.Result == null) Debug.Log(" -------------- PROBANDO LOAD NULL BLABLALBA --------------");

                foreach (var go in objects.Result)
                {
                    data.Add(go.name, go);
                }
                //Debug.Log($"  ... finished!");
            };
        }

        public T this[string index] => data[index];

        public void Unload()
        {
            if (!asyncOp.IsValid())
                return;

            Addressables.Release(asyncOp);
        }
    }
}
