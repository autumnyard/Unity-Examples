using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

namespace AutumnYard
{
    public sealed class Window_Tools : OdinMenuEditorWindow
    {

        #region Window

        [MenuItem("Autumn Yard/Tools %#Q", priority = -1)]
        private static void OpenWindow() => GetWindow<Window_Tools>("Tools").Show(true);

        // Default colors
        private Color Red() => Color.red;
        private Color Green() => new Color32(100, 150, 100, 255); // Recipe
        private Color Purple() => new Color32(200, 100, 200, 255); // Quest
        private Color Blue() => new Color32(100, 150, 200, 255);
        private Color BrownLight() => new Color32(200, 150, 100, 255); // Item
        private Color BrownDark() => new Color32(200, 120, 100, 255);

        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTree tree = new OdinMenuTree(supportsMultiSelect: true)
            {
                { "Helpers", this, EditorIcons.Globe },
            };

            tree.AddAllAssetsAtPath("Managers", "_autumnyard/Resources", typeof(ScriptableObject))
                .AddThumbnailIcons()
                .SortMenuItemsByName();

            tree.AddAllAssetsAtPath("Variables", "_autumnyard/Scriptable Objects", true)
                .AddThumbnailIcons()
                .SortMenuItemsByName();

            tree.AddAllAssetsAtPath("Game Datas", "_autumnyard/GameData/", typeof(AutumnYard.Data.RawGameData), true)
                .AddThumbnailIcons()
                .SortMenuItemsByName();

            return tree;
        }

        #endregion // Window


        #region Game

        [FoldoutGroup("Game")]
        [Button("Next Day")]
        private void NextDay() => World.Instance.NextDay();

        [FoldoutGroup("Game")]
        [Button("Interact with...", ButtonStyle.FoldoutButton, Expanded = true)]
        private void InteractWithRequester(Constants.Requester whom) => QuestManager.Instance.InteractWithRequester(whom);

        [FoldoutGroup("Game")]
        [Button("Play sequence", ButtonStyle.FoldoutButton, Expanded = true)]
        [DisableInEditorMode]
        private void PlaySequence(Constants.Sequences sequence) => SequenceHandler.Instance.Play(sequence, null);

        #endregion // Game


        #region UI

        [FoldoutGroup("UI")]
        [DisableInEditorMode]
        [Button("Show Dialogue", ButtonStyle.FoldoutButton, Expanded = true)]
        private void UI_ShowDialogue(string id = "Probando_0") => Display.UIMenus.Instance.OpenDialogue(id);

        [FoldoutGroup("UI")]
        [Button("Open Overlay Order with Recipe", ButtonStyle.FoldoutButton, Expanded = true)]
        private void UI_OpenOverlayOrderRecipe(Constants.Recipe recipe) => Display.UIMenus.Instance.OpenOverlayOrder(recipe);

        #endregion // UI


        #region Inventory

        [FoldoutGroup("Inventory")]
        [HorizontalGroup("Inventory/Item")]
        [InlineButton("Add", "+")]
        [InlineButton("Remove", "-")]
        [ShowInInspector, LabelWidth(50)]
        private Constants.Item item = Constants.Item.Almond;

        [FoldoutGroup("Inventory")]
        [HorizontalGroup("Inventory/Item", Width = 20f)]
        [ShowInInspector, HideLabel]
        public int Quantity
        {
            get
            {
                if (item == Constants.Item.None)
                    item = Constants.Item.Almond;
                return Inventory.Instance[item];
            }
            set
            {
                int total = value - Inventory.Instance[item];
                Inventory.Instance.Editor_Add(item, total);
            }
        }

        [FoldoutGroup("Inventory")]
        [Button("Give 10 of all items")]
        public void GiveAllItems() => Inventory.Instance.Editor_GiveAllItems(10);

        [FoldoutGroup("Inventory")]
        [Button("Remove all")]
        public void RemoveAllItems() => Inventory.Instance.Clear();

        private void Add() => Inventory.Instance.Editor_Add(item, 1);
        private void Remove() => Inventory.Instance.Editor_Remove(item, 1);

        #endregion // Inventory


        //        #region Incursion

        //#if AUTUMNYARD_DEBUG

        //        [FoldoutGroup("Incursion")]
        //        [HorizontalGroup("Incursion/asd")]
        //        [Button("Add mine 1")]
        //        private void Incursion_QuickAddMine1() => Incursion.Instance.Debug_QuickAddMine1();

        //        [FoldoutGroup("Incursion")]
        //        [HorizontalGroup("Incursion/asd")]
        //        [Button("Add mine 2")]
        //        private void Incursion_QuickAddMine2() => Incursion.Instance.Debug_QuickAddMine2();

        //        [FoldoutGroup("Incursion")]
        //        [HorizontalGroup("Incursion/asd")]
        //        [Button("Add mine 3")]
        //        private void Incursion_QuickAddPicked1() => Incursion.Instance.Debug_QuickAddPicked1();

        //#endif

        //        #endregion // Incursion


        //#region Data

        //[FoldoutGroup("Data")]
        //[Button]
        //private void DownloadData() => DataViewer.Instance.DownloadData();

        //[FoldoutGroup("Data")]
        //[Button]
        //private void LoadData() => Data.Database.Instance.LoadAll();

        //#endregion // Data


        #region Editor

        [FoldoutGroup("Editor")]
        [Button(ButtonStyle.FoldoutButton, Expanded = true)]
        public void MakeBackup()
        {
            SaveManager.Copy(SaveManager.Slot.Main, SaveManager.Slot.Backup1);
        }

        [FoldoutGroup("Editor")]
        [Button(ButtonStyle.FoldoutButton, Expanded = true)]
        public static void SetDefineSymbol(string define, bool to)
        {
#if UNITY_SWITCH
            BuildTargetGroup target = BuildTargetGroup.Switch;
#elif UNITY_PS4
      BuildTargetGroup target = BuildTargetGroup.PS4;
#else
            BuildTargetGroup target = BuildTargetGroup.Standalone;
#endif
            if (to)
            {
                var defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(target);
                if (!defines.Contains($"{define}"))
                {
                    defines += $";{define}";
                }
                PlayerSettings.SetScriptingDefineSymbolsForGroup(target, defines);
            }
            else
            {
                var defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(target);
                if (defines.Contains($"{define}"))
                {
                    defines = defines.Replace($";{define}", "");
                    defines = defines.Replace($"; {define}", "");
                    defines = defines.Replace($"{define};", ""); // DEMO se suele poner al principio, el maldito
                }
                PlayerSettings.SetScriptingDefineSymbolsForGroup(target, defines);
            }
        }

        #endregion // Editor


        //#region Add tools

        //[FoldoutGroup("Add scripts")]
        //[Button("Add script: Screenshot Maker")]
        //private void AddScript_ScreenshotMaker()
        //{
        //    SceneManager.MoveGameObjectToScene(new GameObject("[Screenshot Maker]", typeof(ScreenshotMaker)), SceneManager.GetSceneByName("Loader"));
        //}

        //[FoldoutGroup("Add scripts")]
        //[Button("Add script: Terminal")]
        //private void AddScript_Terminal()
        //{
        //    SceneManager.MoveGameObjectToScene(new GameObject("[Terminal]", typeof(CommandTerminal.Terminal)), SceneManager.GetSceneByName("Loader"));
        //}

        //#endregion // Add tools

    }
}
