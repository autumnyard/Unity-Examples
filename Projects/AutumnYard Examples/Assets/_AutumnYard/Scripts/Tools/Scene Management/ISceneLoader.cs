using System.Collections;

namespace AutumnYard.Tools.Scenes
{
    public interface ISceneLoader
    {
        bool IsLoaded { get; }
        IEnumerator Load();
        IEnumerator Activate();
        void SetActive();
        IEnumerator Unload();
    }
}