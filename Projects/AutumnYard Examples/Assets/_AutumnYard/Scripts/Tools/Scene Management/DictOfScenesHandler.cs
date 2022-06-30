using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AutumnYard.Tools.Scenes
{
    public sealed class DictOfScenesHandler<TEnum> where TEnum : struct, System.Enum
    {
        private readonly IReadOnlyDictionary<TEnum, ISceneLoader> _sceneLoaders;
        private readonly TEnum _emptyValue;
        private readonly bool _setActive;
        private readonly MonoBehaviour _coroutineRunner;
        private readonly string _loadingScreenScene;
        private readonly WaitForEndOfFrame _waitOneFrame = new WaitForEndOfFrame();
        private TEnum _currentValue; // TODO: Sirenix Odin dependency: [Show]
        private bool _isBusy; // TODO: Sirenix Odin dependency: [Show]

        [HideInInspector] public event Action<TEnum> OnBeginLoading;
        [HideInInspector] public event Action<TEnum> OnFinishLoading;

        /// <summary>A tool to handle a dictionary of ISceneLoaders.</summary>
        /// <param name="contexts">The dictionary of ISceneLoaders.</param>
        /// <param name="emptyValue">The value for no scenes.</param>
        /// <param name="coroutineRunner">The MonoBehaviour where the coroutines will be runned.</param>
        /// <param name="loadingScreenScene">Optional: Should there be a Loading Screen scene during the loading?</param>
        /// <param name="setActive">Should the SceneLoader be called Active when loaded?</param>
        public DictOfScenesHandler(
            IReadOnlyDictionary<TEnum, ISceneLoader> contexts,
            TEnum emptyValue,
            MonoBehaviour coroutineRunner,
            string loadingScreenScene = null,
            bool setActive = false
            )
        {
            _emptyValue = emptyValue;
            _sceneLoaders = contexts;
            _loadingScreenScene = loadingScreenScene;
            _coroutineRunner = coroutineRunner;
            _setActive = setActive;
        }


        public void ChangeTo(TEnum to)
        {
            if (_isBusy) return;
            if (_currentValue.Equals(to)) return;

            _coroutineRunner.StartCoroutine(Change_Coroutine(to));
        }

        private IEnumerator Change_Coroutine(TEnum newValue)
        {
            OnBeginLoading?.Invoke(newValue);

            _isBusy = true;

            if (!string.IsNullOrEmpty(_loadingScreenScene)) yield return SceneManager.LoadSceneAsync(_loadingScreenScene, LoadSceneMode.Additive);

            if (!_currentValue.Equals(_emptyValue))
            {
                if (_sceneLoaders[_currentValue].IsLoaded)
                    yield return _sceneLoaders[_currentValue].Unload();
            }

            //yield return new WaitForSeconds(2f);

            _currentValue = newValue;

            if (!_currentValue.Equals(_emptyValue))
            {
                yield return _sceneLoaders[_currentValue].Load();
                yield return _sceneLoaders[_currentValue].Activate();
                yield return _waitOneFrame;
                if (_setActive) _sceneLoaders[_currentValue].SetActive();
            }

            //LightProbes.TetrahedralizeAsync(); // If there are LightProbes

            if (!string.IsNullOrEmpty(_loadingScreenScene)) yield return SceneManager.UnloadSceneAsync(_loadingScreenScene);

            _isBusy = false;

            OnFinishLoading?.Invoke(_currentValue);
        }

        public void ForceSetCurrentTo(TEnum to) => _currentValue = to;
#if AUTUMNYARD_DEBUG
        public void ToggleToNext() => ChangeTo(_currentValue.EnumNext());
        public void ForceIsAlreadyLoaded() => OnFinishLoading?.Invoke(_currentValue);
#endif
    }
}
