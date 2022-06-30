#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using Sirenix.Utilities.Editor;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

namespace AutumnYard
{
    using AutumnYard.Audio;

    public class Window_DataAssets : OdinMenuEditorWindow
    {
        public enum Menu { AudioConfigurations, SFX, UI }

#region Variables

        [OnValueChanged("StateChange")]
        //[LabelText("Manager View")]
        [HideLabel]
        [LabelWidth(100f)]
        [EnumToggleButtons]
        [ShowInInspector]
        private Menu menu;

        private int enumIndex = 0;

        private bool treeRebuild = false;

        //private DrawSelected<SoundAsset> drawSoundAssets = new DrawSelected<SoundAsset>();
        private DrawSelected<AudioSourceConfiguration> drawAudioConfigurations = new DrawSelected<AudioSourceConfiguration>();
        private DrawSelected<SoundAsset> drawSoundSFX = new DrawSelected<SoundAsset>();
        private DrawSelected<SoundAsset> drawSoundUI = new DrawSelected<SoundAsset>();
        private string pathAudioConfigurations = "Assets/_autumnyard/Audio/Configurations";
        private string pathSoundSFX = "Assets/_autumnyard/Audio/SFX";
        private string pathSoundUI = "Assets/_autumnyard/Audio/UI";

#endregion

#region UI

        [MenuItem("Autumn Yard/Data Assets %#E", priority = 1)]
        private static void OpenWindow() => GetWindow<Window_DataAssets>("Data Assets").Show(true);


        private void StateChange()
        {
            treeRebuild = true;
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTree tree = new OdinMenuTree();

            switch (menu)
            {
                case Menu.AudioConfigurations: tree.AddAllAssetsAtPath("", "_autumnyard/Audio/Configurations").AddThumbnailIcons(); break;
                case Menu.SFX: tree.AddAllAssetsAtPath("", "_autumnyard/Audio/SFX", true).AddThumbnailIcons(); break;
                case Menu.UI: tree.AddAllAssetsAtPath("", "_autumnyard/Audio/UI", true).AddThumbnailIcons(); break;
            }

            return tree;
        }


        protected override void Initialize()
        {
            drawAudioConfigurations.SetPath(pathAudioConfigurations);
            drawSoundSFX.SetPath(pathSoundSFX);
            drawSoundUI.SetPath(pathSoundUI);
        }

        protected override void OnGUI()
        {
            if (treeRebuild && Event.current.type == EventType.Layout)
            {
                ForceMenuTreeRebuild();
                treeRebuild = false;
            }

            SirenixEditorGUI.Title("Cool Game", "Autumn Yard Studio", TextAlignment.Center, true);
            EditorGUILayout.Space();

            switch (menu)
            {

                case Menu.AudioConfigurations:
                case Menu.SFX:
                case Menu.UI:
                    DrawEditor(enumIndex);
                    break;
            }

            EditorGUILayout.Space();

            base.OnGUI();
        }

        protected override void DrawEditors()
        {
            switch (menu)
            {

                case Menu.AudioConfigurations: drawAudioConfigurations.SetSelected(this.MenuTree.Selection.SelectedValue); break;
                case Menu.SFX: drawSoundSFX.SetSelected(this.MenuTree.Selection.SelectedValue); break;
                case Menu.UI: drawSoundUI.SetSelected(this.MenuTree.Selection.SelectedValue); break;

                default: DrawEditor(enumIndex); break;
            }

            //base.DrawEditors();
            DrawEditor((int)menu);

        }

        protected override IEnumerable<object> GetTargets()
        {
            List<object> targets = new List<object>
            {
                drawAudioConfigurations,
                drawSoundSFX,
                drawSoundUI,
                base.GetTarget()
            };
            enumIndex = targets.Count - 1;

            return targets;
        }

        protected override void DrawMenu()
        {

            switch (menu)
            {

                case Menu.AudioConfigurations:
                case Menu.SFX:
                case Menu.UI:
                    base.DrawMenu();
                    break;

                default:
                    break;
            }


        }

#endregion

    }

    public class DrawSelected<T> where T : ScriptableObject
    {
        [InlineEditor(InlineEditorObjectFieldModes.CompletelyHidden)]
        public T selected;

        [LabelWidth(100)]
        [PropertyOrder(-1)]
        [BoxGroup("CreateNew", ShowLabel = false)]
        [HorizontalGroup("CreateNew/Horizontal")]
        public string nameForNew;
        private string path;

        [HorizontalGroup("CreateNew/Horizontal")]
        [GUIColor(0.7f, 0.7f, 1f)]
        [Button]
        public void CreateNew()
        {
            if (nameForNew.Equals(string.Empty)) return;

            T newItem = ScriptableObject.CreateInstance<T>();

            if (path.Equals(string.Empty)) path = "Assets/_autumnyard";

            AssetDatabase.CreateAsset(newItem, path + "\\" + nameForNew + ".asset");
            AssetDatabase.SaveAssets();

            nameForNew = "";
        }

        [HorizontalGroup("CreateNew/Horizontal")]
        [GUIColor(1f, 0.7f, 0.7f)]
        [Button]
        public void DeleteSelected()
        {
            if (selected != null)
            {
                string _path = AssetDatabase.GetAssetPath(selected);
                AssetDatabase.DeleteAsset(_path);
                AssetDatabase.SaveAssets();
            }
        }

        public void SetSelected(object item)
        {
            var attempt = item as T;
            if (attempt != null)
                this.selected = attempt;
        }

        public void SetPath(string path)
        {
            this.path = path;
        }
    }
}
#endif
