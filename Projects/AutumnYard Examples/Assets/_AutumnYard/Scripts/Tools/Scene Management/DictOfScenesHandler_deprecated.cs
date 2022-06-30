using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutumnYard.Tools.Scenes
{
    public sealed class DictOfScenesHandler_deprecated<TEnum> where TEnum : struct, Enum
    {
        private readonly IDictionary<TEnum, ISceneLoader> _sceneLoaders;
        private readonly TEnum _emptyValue;
        private readonly bool _setActive;
        private readonly MonoBehaviour _coroutineRunner;
        private readonly WaitForEndOfFrame _waitOneFrame = new WaitForEndOfFrame();
        private TEnum _currentValue; // TODO: Sirenix Odin dependency: [Show]
        private bool _isBusy; // TODO: Sirenix Odin dependency: [Show]

        [HideInInspector] public event Action<TEnum> OnBeginLoading;
        [HideInInspector] public event Action<TEnum> OnFinishLoading;

        public DictOfScenesHandler_deprecated(IDictionary<TEnum, ISceneLoader> scenes, TEnum emptyValue, MonoBehaviour coroutineRunner, bool setActive)
        {
            _emptyValue = emptyValue;
            _sceneLoaders = scenes;
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

            if (!_currentValue.Equals(_emptyValue))
            {
                if (_sceneLoaders[_currentValue].IsLoaded)
                    yield return _sceneLoaders[_currentValue].Unload();
                _currentValue = _emptyValue;
            }

            _currentValue = newValue;

            if (!_currentValue.Equals(_emptyValue))
            {
                yield return _sceneLoaders[_currentValue].Load();
                yield return _sceneLoaders[_currentValue].Activate();
                yield return _waitOneFrame;
                if (_setActive) _sceneLoaders[_currentValue].SetActive();
            }

            LightProbes.TetrahedralizeAsync();

            _isBusy = false;

            OnFinishLoading?.Invoke(_currentValue);
        }
        public void CloseAll()
        {
            _coroutineRunner.StartCoroutine(CloseAll_Coroutine());

            IEnumerator CloseAll_Coroutine()
            {
                if (!_currentValue.Equals(_emptyValue))
                {
                    yield return _sceneLoaders[_currentValue].Unload();
                    _currentValue = _emptyValue;
                }
            }
        }

#if AUTUMNYARD_DEBUG
        public void ForceCurrentTo(TEnum to) => _currentValue = to;
        public void ToggleToNext() => ChangeTo(_currentValue.EnumNext());
        public void ForceIsAlreadyLoaded() => OnFinishLoading?.Invoke(_currentValue);
#endif
    }

}