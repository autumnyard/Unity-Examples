using System.Collections.Generic;
using AutumnYard.Tools.Scenes;

// An example of using the module AutumnYard.Plugins.Scene

namespace AutumnYard.Common
{
    /// <summary> A wrapper for 'DictOfScenesHandler', and store the Dictionary of Scenes. </summary>
    public sealed partial class SceneHandler : SingletonComponent<SceneHandler>
    {
        public enum Context 
        { 
            None, 
            Menu, 
            ExamplePlayer, ExampleCards, ExampleWorld, ExamplePointAndClick,
            Debug 
        }

        private readonly IReadOnlyDictionary<Context, ISceneLoader> _loaders
            = new Dictionary<Context, ISceneLoader>()
            {
                { Context.None, null },
                { Context.Menu, new SceneInBuildLoader( UnityEngine.SceneManagement.LoadSceneMode.Additive, "Menu") },
                { Context.ExamplePlayer, new SceneInBuildLoader( UnityEngine.SceneManagement.LoadSceneMode.Additive, "Game - Example Player") },
                { Context.ExampleCards, new SceneInBuildLoader( UnityEngine.SceneManagement.LoadSceneMode.Additive, "Game - Example Cards") },
                { Context.ExampleWorld, new SceneInBuildLoader( UnityEngine.SceneManagement.LoadSceneMode.Additive, "Game - Example World") },
                { Context.ExamplePointAndClick, new SceneInBuildLoader( UnityEngine.SceneManagement.LoadSceneMode.Additive, "Game - Example PointAndClick") },
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
