using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;


namespace AutumnYard.Tools.Build
{
    public sealed class Window_Build : Editor.EditorWindowBase
    {
        public BuildConfiguration configuration;
        private Vector2 scroll;

        [MenuItem("Autumn Yard/Build %#W", priority = 0)]
        private static void ShowWindow() => GetWindow<Window_Build>("Build");

        private void OnGUI()
        {
            scroll = GUILayout.BeginScrollView(scroll);
            Label("Para otras cosas, mira en el manager GlobalSettings");
            //Button(OpenGlobalSettings, "Abrir GlobalSettings", 20, 200);
            GUILayout.Space(10f);

            Header("Asigna la configuracion:");
            //configuration =	EditorGUILayout.ObjectField(new GUIContent("Configuration", ""), configuration);
            configuration = (BuildConfiguration)EditorGUILayout.ObjectField("Configuration", configuration, typeof(BuildConfiguration), false);

            if (configuration != null)
            {
                //Header("System specific");
                //Int(ref configuration.ns_switchVersion, "Version: Switch");
                //Bool(ref configuration.ns_generateNSP, "Generate NSP");

                Header("Settings");
                Enum(ref configuration.buildType, "Build type");
                Enum(ref configuration.whatToBuild, "What to build");
                Bool(ref configuration.detailedBuildReport, "Detailed build report", "Provide extra information about the build for the asset BuildReportInspector (to debug sizes, see what assets are used in the build and why, etc)");
                Bool(ref configuration.autoRunPlayer, "Auto run player");
                Bool(ref configuration.autoOpenFolder, "Open folder");

                Header("Naming");
                String(ref configuration.path, "Path");
                String(ref configuration.baseName, "Base Name");
                Bool(ref configuration.writeDate, "Write date");
                Bool(ref configuration.writeData, "Write data");
                String(ref configuration.comment, "Comment");

            }
            GUILayout.EndScrollView();

            GUILayout.Space(10f);

            //Button(DoBuildAddressables, "Build Addressables", 20);
            Button(DoBuildPlayer, "Build", 50);
        }

        private void DoBuildPlayer()
        {
            //Chibig.Mika.GlobalSettings.Instance.SetDirectives();
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
    }

}
