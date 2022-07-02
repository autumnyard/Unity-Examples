using System.Collections.Generic;
using AutumnYard.Tools.Scenes;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

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

#if UNITY_EDITOR
        private static void Editor_LoadContextGameWithMap(string path, OpenSceneMode mode)
        {
            EditorSceneManager.SaveOpenScenes();
            var scene = EditorSceneManager.OpenScene(path, mode);
        }

        [MenuItem("Autumn Yard/Load: Menu #F4", priority = 10)]
        private static void Editor_LoadMenu() => Editor_LoadContextGameWithMap("Assets/_Examples/Scenes/Menu.unity", OpenSceneMode.Single);

        [MenuItem("Autumn Yard/Load: Player #F5", priority = 10)]
        private static void Editor_LoadExamplePlayer() => Editor_LoadContextGameWithMap("Assets/_Examples/Scenes/Game - Example Player.unity", OpenSceneMode.Single);

        [MenuItem("Autumn Yard/Load: Cards #F6", priority = 10)]
        private static void Editor_LoadExampleCards() => Editor_LoadContextGameWithMap("Assets/_Examples/Scenes/Game - Example Cards.unity", OpenSceneMode.Single);

        [MenuItem("Autumn Yard/Load: World #F7", priority = 10)]
        private static void Editor_LoadExampleWorld() => Editor_LoadContextGameWithMap("Assets/_Examples/Scenes/Game - Example World.unity", OpenSceneMode.Single);

        [MenuItem("Autumn Yard/Load: PointAndClick #F8", priority = 10)]
        private static void Editor_LoadExamplePointAndClick() => Editor_LoadContextGameWithMap("Assets/_Examples/Scenes/Game - Example PointAndClick.unity", OpenSceneMode.Single);

        [MenuItem("Autumn Yard/Load: Menu (Additive) %#F4", priority = 10)]
        private static void Editor_LoadMenuAdditive() => Editor_LoadContextGameWithMap("Assets/_Examples/Scenes/Menu.unity", OpenSceneMode.Additive);

        [MenuItem("Autumn Yard/Load: Player (Additive) %#F5", priority = 10)]
        private static void Editor_LoadExamplePlayerAdditive() => Editor_LoadContextGameWithMap("Assets/_Examples/Scenes/Game - Example Player.unity", OpenSceneMode.Additive);

        [MenuItem("Autumn Yard/Load: Cards (Additive) %#F6", priority = 10)]
        private static void Editor_LoadExampleCardsAdditive() => Editor_LoadContextGameWithMap("Assets/_Examples/Scenes/Game - Example Cards.unity", OpenSceneMode.Additive);

        [MenuItem("Autumn Yard/Load: World (Additive) %#F7", priority = 10)]
        private static void Editor_LoadExampleWorldAdditive() => Editor_LoadContextGameWithMap("Assets/_Examples/Scenes/Game - Example World.unity", OpenSceneMode.Additive);

        [MenuItem("Autumn Yard/Load: PointAndClick (Additive) %#F8", priority = 10)]
        private static void Editor_LoadExamplePointAndClickAdditive() => Editor_LoadContextGameWithMap("Assets/_Examples/Scenes/Game - Example PointAndClick.unity", OpenSceneMode.Additive);
#endif
    }
}
