using UnityEngine;
using UnityEditor.AddressableAssets.Settings;

namespace AutumnYard.Plugins.Addressables.Editor
{
    public static class BuildTools
    {
        public static void DoBuildAddressables()
        {
            //AddressableAssetProfileSettings profileSettings = AddressableAssetSettingsDefaultObject.Settings.profileSettings;
            //string profileId = profileSettings.GetProfileId("Default");
            //AddressableAssetSettingsDefaultObject.Settings.activeProfileId = profileId;

            Debug.Log("Build Addressables: Begin");
            AddressableAssetSettings.BuildPlayerContent(out var result);
            Debug.Log($"Build Addressables: End {result.Duration}");
        }
    }
}
