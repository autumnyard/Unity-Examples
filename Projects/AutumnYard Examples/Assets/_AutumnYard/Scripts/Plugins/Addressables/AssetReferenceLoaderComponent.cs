using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AutumnYard.Core.Assets
{
    public sealed class AssetReferenceLoaderComponent : MonoBehaviour
    {
        [SerializeField] private AssetLabelReference reference;
        [SerializeField] private bool loadOnEnable;
        [SerializeField] private bool log;
        private AssetReferenceLoader loader; // TODO: Sirenix Odin dependency: [Show]

        private void Awake()
        {
            loader = new AssetReferenceLoader(reference, 1, log);
        }

        private void OnEnable()
        {
            if (loadOnEnable) Load();
        }

        private void OnDisable()
        {
            if (loadOnEnable) Unload();
        }

        //[Button, ButtonGroup, DisableInEditorMode]
        [ContextMenu("Load")] // TODO: Sirenix Odin dependency: [Button, ButtonGroup, DisableInEditorMode]
        public void Load()
        {
            if (!Application.isPlaying) return;

            loader.Load();
        }

        //[Button, ButtonGroup, DisableInEditorMode]
        [ContextMenu("Unload")] // TODO: Sirenix Odin dependency: [Button, ButtonGroup, DisableInEditorMode]
        public void Unload()
        {
            if (!Application.isPlaying) return;

            loader.Unload();
        }

    }
}
