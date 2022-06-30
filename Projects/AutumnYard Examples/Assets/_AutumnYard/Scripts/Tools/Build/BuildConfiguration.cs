using UnityEngine;
//using Sirenix.OdinInspector; // TODO: Sirenix Odin dependency: [EnumToggleButtons] in enums

namespace AutumnYard.Tools.Build
{
    [CreateAssetMenu(fileName = "Build configuration", menuName = "Autumn Yard/Build Configuration", order = 1)]
    public class BuildConfiguration : ScriptableObject
    {
        [Header("Versioning")]
        public string unityVersion = "1.0";

        [Header("System specific")]
        //public int ns_switchVersion = 0;
        //public bool ns_generateNSP = true;
        //public PS4BuildSubtarget ps4_buildTarget = PS4BuildSubtarget.Package;
        public BuildTools.StandaloneDRM standaloneDRM = BuildTools.StandaloneDRM.None;

        [Header("Scene selection")]
        public BuildTools.WhatToBuild whatToBuild = BuildTools.WhatToBuild.Release;
        public bool includeGyms;
        public bool includeSaveEditor;

        [Header("Settings")]
        public BuildTools.BuildType buildType = BuildTools.BuildType.Debug;
        public bool detailedBuildReport;
        public bool autoRunPlayer = true;
        public bool autoOpenFolder = true;

        [Header("Path & Naming")]
        public string path = "../../Builds/"; // "../../Builds/" // c:/Builds/Cool Game/
        public string baseName = "Cool Game";
        public bool writeDate = true;
        public bool writeData = false;
        public string comment = "";
    }
}
