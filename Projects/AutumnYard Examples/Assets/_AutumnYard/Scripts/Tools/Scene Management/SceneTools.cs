using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine.SceneManagement;

namespace AutumnYard.Tools.Scene
{
    public static class SceneTools
    {
        public static bool CheckIfLoaded(string sceneName)
        {
            // Primero busco a ver si ya esta, si esta me la guardo como referencia
            var count = SceneManager.sceneCount;
            for (int i = 0; i < count; i++)
            {
                UnityEngine.SceneManagement.Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name.Equals(sceneName))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool CheckIfSceneLoaded(in string text)
        {
            for (int i = SceneManager.sceneCount - 1; i >= 0; --i)
            {
                UnityEngine.SceneManagement.Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name.Contains(text))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool SetActiveScene(string name)
        {
            var count = SceneManager.sceneCount;
            for (int i = 0; i < count; i++)
            {
                UnityEngine.SceneManagement.Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name.Equals(name))
                {
                    SceneManager.SetActiveScene(scene);
                    return true;
                }
            }
            return false;
        }

        public static IEnumerator UnloadAllScenesThatContains(string text)
        {
            for (int i = SceneManager.sceneCount - 1; i >= 0; --i)
            {
                UnityEngine.SceneManagement.Scene scene = SceneManager.GetSceneAt(i);
                // Descargo todos los mapas y arreando
                if (scene.name.Contains(text))
                {
                    UnityEngine.Debug.Log($" - Unloading map: {scene.name}");
                    yield return SceneManager.UnloadSceneAsync(scene);
                }
            }
        }
    }
}
