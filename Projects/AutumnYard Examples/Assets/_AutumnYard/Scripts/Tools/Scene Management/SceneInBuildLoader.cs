using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AutumnYard.Tools.Scenes
{
    public sealed class SceneInBuildLoader : ISceneLoader
    {
        private string[] _scenes;
        private LoadSceneMode _loadSceneMode;

        public bool IsLoaded => SceneManager.GetSceneByName(_scenes[0]).IsValid();

        /// <summary>
        /// SceneLoader for one or more scenes in the Build Settings.
        /// </summary>
        /// <param name="loadSceneMode">Load the new scenes with Single or Additive mode.</param>
        /// <param name="scenes">Provide at least one scene. Multiple scenes can be provided.</param>
        public SceneInBuildLoader(LoadSceneMode loadSceneMode, params string[] scenes)
        {
            _loadSceneMode = loadSceneMode;
            _scenes = scenes;
        }

        public IEnumerator Load()
        {
            if (_scenes == null) yield break;

            if (_scenes.Length == 0) yield break;

            for (int i = 0; i < _scenes.Length; i++)
            {
                if (i == 0)
                {
                    yield return SceneManager.LoadSceneAsync(_scenes[i], _loadSceneMode);
                }
                else
                {
                    yield return SceneManager.LoadSceneAsync(_scenes[i], LoadSceneMode.Additive);
                }
            }
        }

        public IEnumerator Activate()
        {
            //Debug.LogError("SceneLoaderBuild.Activate: No lo he implementado!");
            yield break;
        }

        public void SetActive()
        {
            Debug.LogError("SceneLoaderBuild.SetActive: No lo he implementado!");
        }

        public IEnumerator Unload()
        {
            if (_scenes == null) yield break;

            if (_scenes.Length == 0) yield break;

            for (int i = 0; i < _scenes.Length; i++)
            {
                yield return SceneManager.UnloadSceneAsync(_scenes[i]);
            }
        }
    }
}