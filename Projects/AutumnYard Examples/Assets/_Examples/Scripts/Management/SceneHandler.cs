using System.Collections.Generic;
using AutumnYard.Tools.Scenes;

// An example of using the module AutumnYard.Plugins.Scene

namespace AutumnYard.Example1
{
    /// <summary> A wrapper for 'DictOfScenesHandler', and store the Dictionary of Scenes. </summary>
    public sealed partial class SceneHandler : SingletonComponent<SceneHandler>
    {
        public enum Context { None, Menu, Game, Debug }

        private readonly IReadOnlyDictionary<Context, ISceneLoader> _loaders
            = new Dictionary<Context, ISceneLoader>()
            {
                { Context.None, null },
                { Context.Menu, new SceneInBuildLoader( UnityEngine.SceneManagement.LoadSceneMode.Additive, "Menu") },
                { Context.Game, new SceneInBuildLoader( UnityEngine.SceneManagement.LoadSceneMode.Additive, "Game - Example1") },
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
