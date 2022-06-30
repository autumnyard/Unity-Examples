using System;
using UnityEngine;

namespace AutumnYard.Example1
{
    //public sealed class GameDirector : SingleInstance<GameDirector>
    public sealed class GameDirector : MonoBehaviour
    {
        public enum State { Play, UI }
        private State _currentState;
        [SerializeField] private Player.PlayerActor playerPrefab;
        [SerializeField] private Player.PlayerConfiguration playerConfiguration;
        [SerializeField] private GameObject[] maps;
        private GameObject _currentMap;
        private Player.PlayerActor _currentPlayer;
        [SerializeField] private UI.UIManager _ui;

        public event Action<State> onChangeState;

        public State CurrentState => _currentState;

        private void Awake()
        {
            //_ui = UI.UIManager.Instance;
            SceneHandler.Instance.ForceSetCurrentContext(SceneHandler.Context.Game);
            _currentMap = LoadMap(0);
            LoadPlayer(Vector3.zero);

            Input.InputManager.Instance.Actions.GameCommands.Enable();

        }

        private void OnDestroy()
        {
            Input.InputManager.Instance.Actions.GameCommands.Disable();
        }

        private GameObject LoadMap(int index)
        {
            return GameObject.Instantiate(maps[index]);
        }
        private void LoadPlayer(Vector3 position)
        {
            _currentPlayer = GameObject.Instantiate(playerPrefab, position, Quaternion.identity);
            _currentPlayer.Configure(playerConfiguration, Input.InputManager.Instance);
        }

        private void Update()
        {
            if (_currentState == State.UI && Input.InputManager.Instance.Actions.Menu.Cancel.triggered)
            {
                ChangeMode_ToPlay();
            }

            if (_currentState == State.Play)
            {
                if (Input.InputManager.Instance.Actions.GameCommands.OpenMenuPause.triggered)
                {
                    Button_OpenPause();
                }
                else if (Input.InputManager.Instance.Actions.GameCommands.OpenMenuInventory.triggered)
                {
                    ChangeMode_ToUI(UI.UIManager.Menu.Inventory);
                }
                else if (Input.InputManager.Instance.Actions.GameCommands.OpenMenuStatus.triggered)
                {
                    ChangeMode_ToUI(UI.UIManager.Menu.Status);
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
            Input.InputManager.Instance.Actions.GameCommands.Enable();

            _currentState = State.Play;
            onChangeState?.Invoke(_currentState);
        }
        private void ChangeMode_ToUI(UI.UIManager.Menu menuToOpen)
        {
            if (_currentState == State.UI) return;

            // Exit previous
            Input.InputManager.Instance.Actions.GameCommands.Disable();
            _currentPlayer.ChangeState(Player.PlayerActor.State.Stopped);

            // Enter new
            switch (menuToOpen)
            {
                case UI.UIManager.Menu.Status:
                    _ui.OpenStatus(new UI.MenuStatus.Data(7, "Prueba"));
                    break;

                case UI.UIManager.Menu.Inventory:
                    _ui.OpenInventory(Inventory.Instance);
                    break;

                case UI.UIManager.Menu.Pause:
                    _ui.OpenPause();
                    break;
            }

            _currentState = State.UI;
            onChangeState?.Invoke(_currentState);
        }


        public void Button_OpenPause() => ChangeMode_ToUI(UI.UIManager.Menu.Pause);
        public void Button_Resume() => ChangeMode_ToPlay();
        public void Button_Exit() => SceneHandler.Instance.ChangeContext(SceneHandler.Context.Menu);
    }
}
