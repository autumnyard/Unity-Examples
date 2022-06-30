using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace AutumnYard.Tools.Scenes
{
    public sealed class SceneInAddressablesLoader : ISceneLoader
    {
        private enum Optimization { None, Unity, SwitchCPUBoost, All }
        private readonly string _assetRef; // TODO: Sirenix Odin dependency: [Show]
        private AsyncOperationHandle<SceneInstance> _asyncOp; // TODO: Sirenix Odin dependency: [Show]

        public bool IsLoaded => _asyncOp.IsValid();

        public SceneInAddressablesLoader(string assetRef)
        {
            _assetRef = assetRef;
        }

        public IEnumerator Load()
        {
            SetOptimization(Optimization.All);

            _asyncOp = Addressables.LoadSceneAsync(_assetRef, LoadSceneMode.Additive, false);
            yield return _asyncOp;

            SetOptimization(Optimization.None);
        }

        public IEnumerator Activate()
        {
            yield return _asyncOp.Result.ActivateAsync();
        }

        public void SetActive()
        {
            SceneManager.SetActiveScene(_asyncOp.Result.Scene);
        }


        public IEnumerator Unload()
        {
            if (!_asyncOp.IsValid())
                yield break;

            _asyncOp = Addressables.UnloadSceneAsync(_asyncOp, false);
            yield return _asyncOp;
            Addressables.Release(_asyncOp);
        }


        private void SetOptimization(Optimization opt)
        {
            switch (opt)
            {
                case Optimization.None:
#if !UNITY_EDITOR
                    Application.backgroundLoadingPriority = ThreadPriority.Normal;
#if UNITY_SWITCH
                    UnityEngine.Switch.Performance.SetCpuBoostMode(UnityEngine.Switch.Performance.CpuBoostMode.Normal);
#endif
#endif
                    break;

                case Optimization.Unity:
#if !UNITY_EDITOR
                    Application.backgroundLoadingPriority = ThreadPriority.High;
#if UNITY_SWITCH
                    UnityEngine.Switch.Performance.SetCpuBoostMode(UnityEngine.Switch.Performance.CpuBoostMode.Normal);
#endif
#endif
                    break;

                case Optimization.SwitchCPUBoost:
#if !UNITY_EDITOR
                    Application.backgroundLoadingPriority = ThreadPriority.Normal;
#if UNITY_SWITCH
                    UnityEngine.Switch.Performance.SetCpuBoostMode(UnityEngine.Switch.Performance.CpuBoostMode.FastLoad);
#endif
#endif
                    break;

                case Optimization.All:
#if !UNITY_EDITOR
                    Application.backgroundLoadingPriority = ThreadPriority.High;
#if UNITY_SWITCH
                    UnityEngine.Switch.Performance.SetCpuBoostMode(UnityEngine.Switch.Performance.CpuBoostMode.FastLoad);
#endif
#endif
                    break;
            }
        }
    }
}