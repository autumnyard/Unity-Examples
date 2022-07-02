using System;
using UnityEngine;
using AutumnYard.Common;
using AutumnYard.Common.UI;
using AutumnYard.Common.Input;

namespace AutumnYard.ExamplePointAndClick
{
    public sealed class ExamplePointAndClickDirector : MonoBehaviour
    {
        public enum State { Play, UI }
        private State _currentState;
        public event Action<State> onChangeState;
        public State CurrentState => _currentState;

        [SerializeField] private UI.ExamplePointAndClickUI _ui;

        private void Awake()
        {
            if (_ui == null) _ui = FindObjectOfType<UI.ExamplePointAndClickUI>();

            SceneHandler.Instance.ForceSetCurrentContext(SceneHandler.Context.ExamplePointAndClick);

            InputManager.Instance.Actions.GameCommands.Enable();
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
            }
        }

        private void ChangeMode_ToPlay()
        {
            if (_currentState == State.Play) return;

            // Exit previouspues 
            _ui.Close();

            // Enter new
            InputManager.Instance.Actions.GameCommands.Enable();

            _currentState = State.Play;
            onChangeState?.Invoke(_currentState);
        }
        private void ChangeMode_ToUI(UI.ExamplePointAndClickUI.Menu menuToOpen)
        {
            if (_currentState == State.UI) return;

            // Exit previous
            InputManager.Instance.Actions.GameCommands.Disable();

            // Enter new
            switch (menuToOpen)
            {
                case UI.ExamplePointAndClickUI.Menu.Pause:
                    _ui.OpenPause();
                    break;
            }

            _currentState = State.UI;
            onChangeState?.Invoke(_currentState);
        }


        public void Button_OpenPause() => ChangeMode_ToUI(UI.ExamplePointAndClickUI.Menu.Pause);
        public void Button_Resume() => ChangeMode_ToPlay();
        public void Button_Exit() => SceneHandler.Instance.ChangeContext(SceneHandler.Context.Menu);
    }
}
