using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutumnYard.Example1.DevTools
{
    public sealed class DevGUI_GameExample1 : MonoBehaviour
    {
        private const int RectWidth = 400;
        private const int RectHeight = 400;
        [SerializeField] private bool show = true;
        [SerializeField] private GUISkin skin;

        private GameDirector _game;
        private UI.UIManager _ui;
        private UI.HUDManager _hud;
        private Player.PlayerActor _player;
        private Inventory _inventory;

        private void Start()
        {
            _game = FindObjectOfType<GameDirector>();
            _ui = FindObjectOfType<UI.UIManager>();
            _hud = FindObjectOfType<UI.HUDManager>();
            _player = FindObjectOfType<Player.PlayerActor>();
            _inventory = Inventory.Instance;
        }

        private void OnGUI()
        {
            if (!show) return;

            GUI.skin = skin != null ? skin : null;

            GUILayout.BeginArea(Core.DeveloperTools.GUITools.RectTopLeft);
            {
                GUILayout.BeginVertical();
                GUILayout.Label($"<b>FSM</b>");
                if (_game != null) GUILayout.Label($"Game: <i>{_game.CurrentState}</i>");
                if (_ui != null) GUILayout.Label($"UI: <i>{_ui.CurrentMenu}</i>");
                if (_hud != null) GUILayout.Label($"HUD: <i>{_hud.CurrentState}</i>");
                if (_player != null) GUILayout.Label($"Player: <i>{_player.CurrentState}</i>");
                //GUILayout.Label($"Control: {controlMode.Current}");
                //if(PlayerActor.HasInstance) GUILayout.Label($"Player: {PlayerActor.Instance.CurrentMode}");

                //#if !AUTUMNYARD_HIDE_HUDS
                //                GUILayout.Label($"HUD:");
                //                if (Display.HUD.HasInstance) Display.HUD.Instance.ShowGUI();
                //#endif

                //                GUILayout.Label($"Tools:");
                //                GUITools.Button("Add item", () => { Inventory.Instance.Editor_Add(Item.Almond, 1); });
                //                GUITools.Button("Notification", () => { SequenceHandler.Instance.Play(Sequences.IncursionInventoryNoSlots, null); });

                GUILayout.EndVertical();
            }
            GUILayout.EndArea();

            GUILayout.BeginArea(Core.DeveloperTools.GUITools.RectBottomLeft);
            {
                var items = _inventory.Items;
                GUILayout.FlexibleSpace();
                GUILayout.Label($"<b>Inventory</b>");
                for (int i = 0; i < items.Length; i++)
                {
                    GUILayout.Label($"Item {i} ({(Item)i}): {items[i]}");
                }
            }
            GUILayout.EndArea();
        }
    }
}