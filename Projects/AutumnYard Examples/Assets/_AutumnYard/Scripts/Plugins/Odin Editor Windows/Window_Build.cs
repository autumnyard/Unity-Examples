using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

namespace AutumnYard.Editor
{
    public class Window_Build : OdinEditorWindow
    {
        [AssetList]
        [InlineEditor(Expanded = true)]
        [SerializeField]
        private BuildConfiguration configuration;


        #region UI

        [MenuItem("Chibig/Build %#W", priority = 0)]
        public static void ShowWindow() => GetWindow<Window_Build>("Build");

        [Button(ButtonSizes.Small), GUIColor(.8f, .1f, 0, .9f)]
        public void BuildAddressables() => DoBuildAddressables();

        [Button(ButtonSizes.Large), GUIColor(.9f, .1f, 0, 1)]
        public void BuildPlayer() => DoBuildPlayer();

        #endregion // UI


        #region Building process

        private void DoBuildAddressables()
        {
            Chibig.GlobalSettings.Instance.SetDirectives();
            BuildTools.DoBuildAddressables();
        }

        private void DoBuildPlayer()
        {
            GlobalSettings.Instance.SetDirectives();
            BuildTools.SetProduct();
            BuildTools.SetVersioning(in configuration);

#if UNITY_STANDALONE
            BuildTools.ConfigureStandalone(in configuration);
#elif UNITY_SWITCH
            BuildTools.ConfigureSwitch(in configuration);
#elif UNITY_PS4
            ConfigurePS4();
#endif

            var options = BuildTools.GetOptions(in configuration);
            BuildTools.DoBuildPlayerAsync(configuration, options);
        }


        [Button] private void PrintData() => BuildTools.PrintData();

        #endregion // Building process

    }
}
