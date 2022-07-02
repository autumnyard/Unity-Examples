using System;
using UnityEngine;
using AutumnYard.Common;
using AutumnYard.Common.UI;
using AutumnYard.Common.Input;

namespace AutumnYard.ExamplePlayer
{
    public sealed class ExamplePlayerDirector : MonoBehaviour
    {
        public enum State { Play, UI }
        private State _currentState;
        public event Action<State> onChangeState;
        public State CurrentState => _currentState;

        [SerializeField] private Player.PlayerActor playerPrefab;
        [SerializeField] private Player.PlayerConfiguration playerConfiguration;
        [SerializeField] private GameObject[] maps;
        private GameObject _currentMap;
        private Player.PlayerActor _currentPlayer;
        [SerializeField] private UI.ExamplePlayerUI _ui;

        private void Awake()
        {
            //_ui = UIManager.Instance;
            if (_ui == null) _ui = FindObjectOfType<UI.ExamplePlayerUI>();

            SceneHandler.Instance.ForceSetCurrentContext(SceneHandler.Context.ExamplePlayer);
            _currentMap = LoadMap(0);
            LoadPlayer(Vector3.zero);

            InputManager.Instance.Actions.GameCommands.Enable();
        }

        private void OnDestroy()
        {
            InputManager.Instance.Actions.GameCommands.Disable();
        }

        private GameObject LoadMap(int index)
        {
            return GameObject.Instantiate(maps[index]);
        }
        private void LoadPlayer(Vector3 position)
        {
            _currentPlayer = GameObject.Instantiate(playerPrefab, position, Quaternion.identity);
            _currentPlayer.Configure(playerConfiguration, InputManager.Instance);
        }

        private void Update()
        {
            if (_currentState == State.UI && InputManager.Instance.Actions.Menu.Cancel.triggered)
            {
                ChangeMode_ToPlay();
            }

            if (_currentState == State.Play)
            {
                if (InputManager.Instance.Actions.GameCommands.OpenMenuPause.triggered)
                {
                    Button_OpenPause();
                }
                else if (InputManager.Instance.Actions.GameCommands.OpenMenuInventory.triggered)
                {
                    ChangeMode_ToUI(UI.ExamplePlayerUI.Menu.Inventory);
                }
                else if (InputManager.Instance.Actions.GameCommands.OpenMenuStatus.triggered)
                {
                    ChangeMode_ToUI(UI.ExamplePlayerUI.Menu.Status);
                }
            }
        }


        private void ChangeMode_ToPlay()
        {
            if (_currentState == State.Play) return;

            // Exit previouspues 
            _ui.Close();

            // Enter new
            _currentPlayer.ChangeState(Player.PlayerActor.State.Normal);
            InputManager.Instance.Actions.GameCommands.Enable();

            _currentState = State.Play;
            onChangeState?.Invoke(_currentState);
        }
        private void ChangeMode_ToUI(UI.ExamplePlayerUI.Menu menuToOpen)
        {
            if (_currentState == State.UI) return;

            // Exit previous
            InputManager.Instance.Actions.GameCommands.Disable();
            _currentPlayer.ChangeState(Player.PlayerActor.State.Stopped);

            // Enter new
            switch (menuToOpen)
            {
                case UI.ExamplePlayerUI.Menu.Status:
                    _ui.OpenStatus(new UI.MenuStatus.Data(7, "Prueba"));
                    break;

                case UI.ExamplePlayerUI.Menu.Inventory:
                    _ui.OpenInventory(Inventory.Instance);
                    break;

                case UI.ExamplePlayerUI.Menu.Pause:
                    _ui.OpenPause();
                    break;
            }

            _currentState = State.UI;
            onChangeState?.Invoke(_currentState);
        }


        public void Button_OpenPause() => ChangeMode_ToUI(UI.ExamplePlayerUI.Menu.Pause);
        public void Button_Resume() => ChangeMode_ToPlay();
        public void Button_Exit() => SceneHandler.Instance.ChangeContext(SceneHandler.Context.Menu);
    }
}
