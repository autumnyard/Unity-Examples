using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;


namespace AutumnYard.Tools.Build
{
    public static class BuildTools
    {
        public enum BuildType { DebugSlow, Debug, Master }
        public enum WhatToBuild { Release, BuildSettings }
        public enum StandaloneDRM { None, Steam, Gog, }
        private enum Color { Normal, Success, Failure }

        public static void SetProduct()
        {
            //AutumnYard.GlobalSettings.Instance.SetProduct();
            //PlayerSettings.productName = AutumnYard.GlobalSettings.Instance.productName;
            //PlayerSettings.companyName = companyName;
            //PlayerSettings.bundleVersion = unityVersion;
            Debug.LogError("Missing method implementation: SetProduct");
        }

        public static void SetVersioning(in BuildConfiguration configuration)
        {
            PlayerSettings.bundleVersion = configuration.unityVersion;
#if UNITY_SWITCH
            PlayerSettings.Switch.releaseVersion = configuration.ns_switchVersion.ToString();
#endif
        }

        public static BuildPlayerOptions GetOptions(in BuildConfiguration configuration)
        {
            BuildPlayerOptions options = new BuildPlayerOptions
            {
                scenes = GetScenes(in configuration),
                locationPathName = CalculateName(in configuration),
                target = EditorUserBuildSettings.activeBuildTarget,// BuildTarget.Switch;
            };

            options.options = configuration.buildType switch
            {
                BuildType.DebugSlow => BuildOptions.Development | BuildOptions.AllowDebugging | BuildOptions.StrictMode,
                BuildType.Debug => BuildOptions.Development | BuildOptions.StrictMode,
                _ => BuildOptions.None
            };

            if (configuration.detailedBuildReport)
                options.options |= BuildOptions.DetailedBuildReport;

            if (configuration.autoRunPlayer)
                options.options |= BuildOptions.AutoRunPlayer;

            return options;
        }

        private static string[] GetScenes(in BuildConfiguration configuration)
        {
            switch (configuration.whatToBuild)
            {
                default:
                case WhatToBuild.Release:
                    {
                        List<string> scenes = new List<string>();

                        scenes.Add("Assets/_Examples/Scenes/Menu.unity");
                        scenes.Add("Assets/_Examples/Scenes/Loading.unity");
                        scenes.Add("Assets/_Examples/Scenes/Game - Example 1.unity");
                        scenes.Add("Assets/_Examples/Scenes/Game - Example 2.unity");

                        //if (configuration.includeGyms)
                        //{
                        //    var asds = AssetDatabase.FindAssets("t:Scene Gym", new string[] { "Assets/_autumnyard/Scenes" });
                        //    foreach (var item in asds)
                        //    {
                        //        var scenePath = AssetDatabase.GUIDToAssetPath(item);
                        //        scenes.Add(scenePath);
                        //    }
                        //}
                        //if (configuration.includeSaveEditor)
                        //{
                        //    scenes.Add("Assets/_autumnyard/Scenes/Contexts/SaveEditor.unity");
                        //}
#if AUTUMNYARD_DEBUG
                        scenes.Add("Assets/_Examples/Scenes/Debug.unity");
#endif

                        return scenes.ToArray();
                    }

                case WhatToBuild.BuildSettings:
                    {
                        return EditorBuildSettings.scenes.Select(s => s.path).ToArray();
                    }
            }

        }

        private static string CalculateName(in BuildConfiguration configuration)
        {
            StringBuilder fileName = new StringBuilder(configuration.path);

#if UNITY_STANDALONE
            fileName.Append($"{configuration.baseName}-Win");
#elif UNITY_ANDROID
            fileName.Append($"{configuration.baseName}-Android");
#elif UNITY_SWITCH
            fileName.Append($"{configuration.baseName}-Switch");
#elif UNITY_PS4
      fileName.Append($"PS4-{configuration.baseName}");
#endif

            if (configuration.writeDate)
            {
                fileName.Append($"-b{System.DateTime.Now.ToString("yyyyMMdd-HHmm")}");
            }

            if (configuration.writeData)
            {
#if UNITY_STANDALONE
                fileName.Append($"-v{configuration.unityVersion}");
#elif UNITY_ANDROID
                fileName.Append($"-v{configuration.unityVersion}");
#elif UNITY_SWITCH
                fileName.Append($"-v{configuration.ns_switchVersion}({configuration.unityVersion})");
#elif UNITY_PS4
                fileName.Append($"-v{unityVersion}") // FALTA ARREGLAR ESTO
#endif
                fileName.Append(configuration.buildType == BuildTools.BuildType.Debug ? "d" : "r");
                //fileName.Append(GlobalSettings.Instance.directiveDebug ? "c" : "");
                Debug.LogError("Missing method implementation: Check directive debug for naming");
#if UNITY_STANDALONE
                fileName.Append(configuration.standaloneDRM != BuildTools.StandaloneDRM.None ? "-" + configuration.standaloneDRM.ToString() : "");
#endif
            }


            if (!configuration.comment.Equals(""))
            {
                fileName.Append($"-{configuration.comment}");
            }

#if UNITY_STANDALONE
            fileName.Append($"/{configuration.baseName}.exe");
#elif UNITY_ANDROID
            fileName.Append($"/{configuration.baseName}.apk");
#elif UNITY_SWITCH
            fileName.Append(configuration.ns_generateNSP ? ".nsp" : ".nspd");
#elif UNITY_PS4
#endif

            return fileName.ToString();
        }

        private static void Print(string text, Color color = Color.Normal)
        {
            string colorString;
            switch (color)
            {
                default:
                case Color.Normal: colorString = $"blue"; break;
                case Color.Success: colorString = $"green"; break;
                case Color.Failure: colorString = $"red"; break;
            }
            Debug.Log($"<color={colorString}>PabloBuilder: {text}</color>");
        }


        public static void PrintData()
        {
            Print($" ---- Unity build data ----");
            Print($"{PlayerSettings.applicationIdentifier}: {PlayerSettings.productGUID}, {PlayerSettings.companyName} and {PlayerSettings.productName}");

#if UNITY_SWITCH
            Print($"Defines: {PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Switch)}");
#elif UNITY_STANDALONE
            Print($"Defines: {PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone)}");
#elif UNITY_PS4
            Print($"Defines: {PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.PS4)}");
#elif UNITY_XBOX
#endif

            Print($"Unity version: {PlayerSettings.bundleVersion}");
#if UNITY_SWITCH
            Print($" ---- Switch specifics ----");
            Print($"{PlayerSettings.Switch.applicationID}, Display: {PlayerSettings.Switch.displayVersion}. Switch version: {PlayerSettings.Switch.releaseVersion}");
#endif
        }


        public static void DoBuildPlayerSync(in BuildConfiguration configuration, in BuildPlayerOptions options)
        {
            var buildTimeInit = System.DateTime.Now;
            Print($" ==== Gonna build for {options.target}-{configuration.buildType} ====");
            Print($"Output file: {options.locationPathName}");
            PrintData();

            Debug.Log("Build Executable: Begin");

            BuildReport report = BuildPipeline.BuildPlayer(options);

            Debug.Log("Build Executable: End ");

            BuildSummary summary = report.summary;

            switch (summary.result)
            {
                case BuildResult.Unknown:
                    Print("Build failed CON EXTRA?AS CONSECUENCIAS", Color.Failure);
                    break;

                case BuildResult.Succeeded:
                    Print("Build succeeded: " + summary.totalSize + " bytes", Color.Success);
                    if (configuration.autoOpenFolder)
                    {
                        string itemPath = options.locationPathName.Replace(@"/", @"\");   // explorer doesn't like front slashes
                        System.Diagnostics.Process.Start("explorer.exe", "/select," + itemPath);
                    }
                    var buildTimeFinish = System.DateTime.Now;
                    var diff = buildTimeFinish - buildTimeInit;
                    Print($"Build time: {diff}", Color.Success);

                    break;

                case BuildResult.Failed:
                    Print("Build failed", Color.Failure);
                    break;

                case BuildResult.Cancelled:
                    Print("Build cancelled", Color.Normal);
                    break;
            }


        }

        public static void DoBuildPlayerAsync(BuildConfiguration configuration, BuildPlayerOptions options)
        {
            var buildTimeInit = System.DateTime.Now;
            Print($" ==== Gonna build for {options.target}-{configuration.buildType} ====");
            Print($"Output file: {options.locationPathName}");
            PrintData();

            EditorApplication.delayCall += () =>
            {
                BuildReport report = BuildPipeline.BuildPlayer(options);

                Print($"==== Report ====");

                BuildSummary summary = report.summary;

                switch (summary.result)
                {
                    case BuildResult.Unknown:
                        Print("Build failed CON EXTRA?AS CONSECUENCIAS", Color.Failure);
                        break;

                    case BuildResult.Succeeded:
                        Print("Build succeeded: " + summary.totalSize + " bytes", Color.Success);
                        if (configuration.autoOpenFolder)
                        {
                            string itemPath = options.locationPathName.Replace(@"/", @"\");   // explorer doesn't like front slashes
                            System.Diagnostics.Process.Start("explorer.exe", "/select," + itemPath);
                        }
                        var buildTimeFinish = System.DateTime.Now;
                        var diff = buildTimeFinish - buildTimeInit;
                        Print($"Build time: {diff}", Color.Success);

                        break;

                    case BuildResult.Failed:
                        Print("Build failed", Color.Failure);
                        break;

                    case BuildResult.Cancelled:
                        Print("Build cancelled", Color.Normal);
                        break;
                }
            };
        }

#if UNITY_STANDALONE
        public static void ConfigureStandalone(in BuildConfiguration configuration)
        {
            EditorUserBuildSettings.development = configuration.buildType == BuildType.Debug;
            EditorUserBuildSettings.buildScriptsOnly = false;
        }
#elif UNITY_SWITCH
        public static void ConfigureSwitch(in BuildConfiguration configuration)
        {
            BuildTools.SetProduct();
            BuildTools.SetVersioning(in configuration);

            PlayerSettings.bundleVersion = configuration.unityVersion;
            PlayerSettings.Switch.releaseVersion = configuration.ns_switchVersion.ToString();

            PlayerSettings.Switch.startupUserAccount = PlayerSettings.Switch.StartupUserAccount.Required;
            PlayerSettings.Switch.touchScreenUsage = PlayerSettings.Switch.TouchScreenUsage.Supported; //(buildType == BuildType.Debug) ? PlayerSettings.Switch.TouchScreenUsage.Supported : PlayerSettings.Switch.TouchScreenUsage.None;
            PlayerSettings.Switch.logoType = PlayerSettings.Switch.LogoType.LicensedByNintendo;
            PlayerSettings.Switch.applicationAttribute = PlayerSettings.Switch.ApplicationAttribute.None;

            PlayerSettings.Switch.userAccountSaveDataSize = 4194304; // 2097152;
            PlayerSettings.Switch.userAccountSaveDataJournalSize = 4194304; // 2097152;

            PlayerSettings.Switch.screenResolutionBehavior = PlayerSettings.Switch.ScreenResolutionBehavior.Both;

            //PlayerSettings.Switch.useSwitchCPUProfiler = false;
            //PlayerSettings.Switch.networkInterfaceManagerInitializeEnabled = true;
            //PlayerSettings.Switch.socketInitializeEnabled = true;
            //PlayerSettings.Switch.useSwitchCPUProfiler = false;

            PlayerSettings.Switch.supportedNpadCount = 1;
            PlayerSettings.Switch.supportedNpadStyles = PlayerSettings.Switch.SupportedNpadStyle.FullKey
              | PlayerSettings.Switch.SupportedNpadStyle.Handheld
              | PlayerSettings.Switch.SupportedNpadStyle.JoyDual;

            EditorUserBuildSettings.development = configuration.buildType == BuildType.Debug;
            EditorUserBuildSettings.switchCreateRomFile = configuration.ns_generateNSP; // If we want this in a bundle, we don't create an NSP to edit the Program Index in the NSPD
            EditorUserBuildSettings.buildScriptsOnly = false;
        }
#endif
    }
}
