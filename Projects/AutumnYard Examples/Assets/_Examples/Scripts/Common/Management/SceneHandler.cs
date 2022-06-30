using System.Collections.Generic;
using AutumnYard.Tools.Scenes;

// An example of using the module AutumnYard.Plugins.Scene

namespace AutumnYard.Common
{
    /// <summary> A wrapper for 'DictOfScenesHandler', and store the Dictionary of Scenes. </summary>
    public sealed partial class SceneHandler : SingletonComponent<SceneHandler>
    {
        public enum Context { None, Menu, Example1, Example2, Debug }

        private readonly IReadOnlyDictionary<Context, ISceneLoader> _loaders
            = new Dictionary<Context, ISceneLoader>()
            {
                { Context.None, null },
                { Context.Menu, new SceneInBuildLoader( UnityEngine.SceneManagement.LoadSceneMode.Additive, "Menu") },
                { Context.Example1, new SceneInBuildLoader( UnityEngine.SceneManagement.LoadSceneMode.Additive, "Game - Example 1") },
                { Context.Example2, new SceneInBuildLoader( UnityEngine.SceneManagement.LoadSceneMode.Additive, "Game - Example 2") },
                { Context.Debug, new SceneInBuildLoader( UnityEngine.SceneManagement.LoadSceneMode.Additive, "Debug") },
            };

        private DictOfScenesHandler<Context> _handler;

        protected override void DoAwake()
        {
            _handler = new DictOfScenesHandler<Context>(_loaders, Context.None, this, "Loading");
        }

        public void ChangeContext(Context to)
        {
            _handler.ChangeTo(to);
        }
        public void ForceSetCurrentContext(Context to)
        {
            _handler.ForceSetCurrentTo(to);
            name = $"SceneHandler [{to}";
        }
    }
}
