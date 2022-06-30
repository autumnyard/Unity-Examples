using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements; // TODO: Some wrong dependency with UI Elements system, throws lots of errors

namespace AutumnYard.Editor
{
    public sealed class Window_SceneSelector : EditorWindow
    {
        private bool foldoutOpen;

        [MenuItem("Autumn Yard/Windows/Scene Selector")]
        private static void OpenWindow()
        {
            GetWindow<Window_SceneSelector>("Scene Selector");
        }

        //private void Foll(PlayModeStateChange obj) => Fill();

        //[InitializeOnLoadMethod]
        //private static void RegisterCallbacks()
        //{
        //    EditorApplication.playModeStateChanged += ReturnToPreviousScene;
        //}

        //private void CreateGUI()
        //{
        //    Fill();
        //    EditorApplication.playModeStateChanged += Foll;
        //}

        //private void OnFocus() => Fill();
        //private void OnLostFocus() => Fill();


        //private static string[] GetScenes() => AssetDatabase.FindAssets("t:Scene Gym", new string[] { "Assets/_autumnyard/Scenes" });

        //private static void ReturnToPreviousScene(PlayModeStateChange change)
        //{
        //    if (change == PlayModeStateChange.EnteredEditMode)
        //    {
        //        //EditorSceneManager.OpenScene(SceneSelectorSettings.instance.PreviousScenePath, OpenSceneMode.Single);
        //    }
        //}


        //private void Fill()
        //{
        //    rootVisualElement.Clear();
        //    rootVisualElement.Add(UIElementsMethods.Header("El gran seleccionador de Gyms!", 10));
        //    rootVisualElement.Add(UIElementsMethods.Subheader("Tienen que llamarse \"Gym <Tipo> <Nombre>\" y despues ya lo que quieras.", 10));

        //    foreach (var sceneGuid in GetScenes())
        //    {
        //        rootVisualElement.Add(CreateButton_Gym(sceneGuid));
        //    }

        //    rootVisualElement.Add(UIElementsMethods.Header("Y ahora los contextos:", 10));
        //    var foldout = UIElementsMethods.Foldout(15);
        //    foldout.text = "Contextos";
        //    foldout.SetValueWithoutNotify(foldoutOpen);
        //    foldout.RegisterValueChangedCallback(value);
        //    foldout.Add(CreateButton_ContextWithoutMap(Context.MainMenu));
        //    foldout.Add(CreateButton_ContextWithMap(Constants.Map.Lair));
        //    foldout.Add(CreateButton_ContextWithMap(Constants.Map.Kitchen));
        //    foldout.Add(CreateButton_ContextWithMap(Constants.Map.Study));
        //    foldout.Add(CreateButton_ContextWithMap(Constants.Map.Garden));
        //    foldout.Add(CreateButton_ContextWithoutMap(Context.SaveEditor));
        //    rootVisualElement.Add(foldout);

        //}

        //private void value(ChangeEvent<bool> evt)
        //{
        //    foldoutOpen = evt.newValue;
        //}


        //private VisualElement CreateButton_ContextWithMap(Constants.Map map)
        //{
        //    var color = ColorDatabase.Instance.GetColorString(map.ToString());

        //    var label = UIElementsMethods.Label($"Game: <color=#{color}><b>{map}</b></color>", 0, 5, 90, TextAnchor.UpperRight);
        //    var buttonEditor = GenerateButton_SceneChangeMapEditor(map);
        //    var buttonPlay = GenerateButton_SceneChangeMapPlay(map);

        //    buttonEditor.SetEnabled(!Application.isPlaying);
        //    buttonPlay.SetEnabled(Application.isPlaying);

        //    return UIElementsMethods.Group(15, label, buttonEditor, buttonPlay);
        //}

        //private VisualElement CreateButton_ContextWithoutMap(Context context)
        //{
        //    var label = UIElementsMethods.Label($"<b>{context}</b>", 0, 5, 90, TextAnchor.UpperRight);
        //    var buttonEditor = GenerateButton_SceneChangeContextEditor(context);
        //    var buttonPlay = GenerateButton_SceneChangeContextPlay(context);

        //    buttonEditor.SetEnabled(!Application.isPlaying);
        //    buttonPlay.SetEnabled(Application.isPlaying);

        //    return UIElementsMethods.Group(15, label, buttonEditor, buttonPlay);


        //}

        //private VisualElement CreateButton_Gym(string sceneGuid)
        //{
        //    var scenePath = AssetDatabase.GUIDToAssetPath(sceneGuid);


        //    string[] split = scenePath.Split(' ', '.');

        //    Label label_type;
        //    if (split.Length >= 3)
        //    {
        //        AutumnYard.Core.Extensions.Convert(split[1], out ColorDatabase.GymType type);
        //        var color = ColorDatabase.Instance.GetGymTypeString(type);
        //        label_type = UIElementsMethods.Label($"<color=#{color}>{split[1]}</color>", 0, 5, 90, TextAnchor.UpperRight);
        //    }
        //    else
        //    {
        //        label_type = UIElementsMethods.Label("---", 0, 5, 90, TextAnchor.UpperRight);
        //    }

        //    var label_name = UIElementsMethods.Label($"<b>{split[2]}</b>", 0, 0, 90, TextAnchor.UpperLeft);
        //    var buttonOpen = GenerateButton_Open(scenePath);
        //    var buttonPlay = GenerateButton_Play(scenePath);
        //    var buttonChange = GenerateButton_Change(scenePath);

        //    buttonOpen.SetEnabled(!Application.isPlaying);
        //    buttonPlay.SetEnabled(!Application.isPlaying);
        //    buttonChange.SetEnabled(Application.isPlaying);

        //    return UIElementsMethods.Group(3, label_type, label_name, buttonOpen, buttonPlay, buttonChange);
        //}


        //private static Button GenerateButton_SceneChangeContextEditor(Context context)
        //{
        //    return new Button(() =>
        //    {
        //        SceneHandler.Editor_ChangeContext(context);
        //    })
        //    {
        //        text = "Editor",
        //    };
        //}

        //private static Button GenerateButton_SceneChangeContextPlay(Context context)
        //{
        //    return new Button(() =>
        //    {
        //        SceneHandler.Instance.ChangeContext(context);
        //    })
        //    {
        //        text = "Play",
        //    };
        //}

        //private static Button GenerateButton_SceneChangeMapEditor(Constants.Map map)
        //{
        //    return new Button(() =>
        //    {
        //        SceneHandler.Editor_ChangeMap(map);
        //    })
        //    {
        //        text = "Editor",
        //    };
        //}

        //private static Button GenerateButton_SceneChangeMapPlay(Constants.Map map)
        //{
        //    return new Button(() =>
        //    {
        //        //SceneHandler.Instance.ChangeContext(Context.Game);
        //        //SceneHandler.Instance.ChangeMap(map);
        //        Debug.Log($"No he logrado implementar esto todav�a :(. Tratando de cambiar al mapa {map} en play.");
        //    })
        //    {
        //        text = "Play",
        //    };
        //}

        //private static Button GenerateButton_Open(string path)
        //{
        //    return new Button(() =>
        //    {
        //        EditorSceneManager.OpenScene(path, OpenSceneMode.Single);
        //    })
        //    {
        //        text = "Open",
        //    };
        //}

        //private static Button GenerateButton_Play(string path)
        //{
        //    return new Button(() =>
        //    {
        //        SceneSelectorSettings.instance.PreviousScenePath = SceneManager.GetActiveScene().path;
        //        EditorSceneManager.OpenScene(path, OpenSceneMode.Single);
        //        EditorApplication.EnterPlaymode();
        //    })
        //    {
        //        text = "Play",
        //    };
        //}

        //private static Button GenerateButton_Change(string path)
        //{
        //    return new Button(() =>
        //    {
        //        EditorSceneManager.LoadSceneAsyncInPlayMode(path, new LoadSceneParameters(LoadSceneMode.Single));
        //    })
        //    {
        //        text = "Change",
        //    };
        //}
    }
}