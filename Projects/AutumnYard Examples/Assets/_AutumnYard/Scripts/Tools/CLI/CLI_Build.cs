using UnityEngine;
using UnityEditor;
using AutumnYard.Tools.Build;

namespace AutumnYard.Tools.CLI
{
    public static class CLI_Build
    {
        private static string path = "C:/Builds/Elusive/";

        public static void Build()
        {
            Debug.Log("--- Begin build --- ");

            BuildConfiguration configuration = new BuildConfiguration
            {
                path = path,
                writeDate = false,
                writeData = false,
                autoOpenFolder = true
            };

            //SetDebugMode();
            //SetCheatMode();
            BuildTools.SetProduct();

#if UNITY_STANDALONE
            BuildTools.ConfigureStandalone(in configuration);
            //SetStandaloneDRM();
#elif UNITY_SWITCH
			BuildTools.ConfigureSwitch(in configuration);
#elif UNITY_PS4
            BuildTools.ConfigurePS4();
#endif
            AutumnYard.Plugins.Addressables.Editor.BuildTools.DoBuildAddressables();
            var options = BuildTools.GetOptions(in configuration);
            BuildTools.DoBuildPlayerSync(in configuration, in options);

            Debug.Log("--- End build --- ");

            EditorApplication.Exit(0);
        }

    }
}
