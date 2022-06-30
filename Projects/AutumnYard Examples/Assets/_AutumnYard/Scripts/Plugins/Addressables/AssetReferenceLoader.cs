using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace AutumnYard.Core.Assets
{
    [System.Serializable]
    public sealed class AssetReferenceLoader
    {
        private readonly AssetLabelReference reference;
        private readonly bool log;
        private readonly int validateNumber;
        private AsyncOperationHandle<IList<Object>> asyncOp;
        private Object[] array; // TODO: Sirenix Odin dependency: [Show]

        public AssetReferenceLoader(AssetLabelReference reference, bool log = false)
        {
            this.reference = reference;
            this.log = log;
        }

        public AssetReferenceLoader(AssetLabelReference reference, int validateNumber, bool log = false)
        {
            this.reference = reference;
            this.log = log;
            this.validateNumber = validateNumber;
        }

        public Object this[int index] => array[index];

        public void Load()
        {
            asyncOp = Addressables.LoadAssetsAsync<Object>(reference, null);
            asyncOp.Completed += HandleComplete;

        }

        private void HandleComplete(AsyncOperationHandle<IList<Object>> objects)
        {
            asyncOp.Completed -= HandleComplete;

            if (objects.Status == AsyncOperationStatus.Failed)
            {
                throw new AssetException($"Failed loading assets with reference: {reference.labelString}");
                //Debug.LogError($"Failed loading assets with reference: {reference.labelString}");
                //return;
            }

            if (validateNumber > 0 && validateNumber - 1 != objects.Result.Count)
            {
                throw new AssetException($"Number of elements loaded ({objects.Result.Count}) is different from ValidateNumber ({validateNumber})");
                //Debug.LogError($"Number of elements loaded ({objects.Result.Count}) is different from ValidateNumber ({validateNumber})");
                //return;
            }

            array = new Object[objects.Result.Count];
            objects.Result.CopyTo(array, 0);

            if (log)
                Debug.Log($"Loaded {array.Length} assets with label: {reference.labelString}");
        }

        public void Unload()
        {
            if (!asyncOp.IsValid())
                return;

            array = null;
            Addressables.Release(asyncOp);
        }
    }
}
