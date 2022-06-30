using UnityEngine;
using UnityEngine.InputSystem;
using AutumnYard.Plugins.InputSystem;

namespace AutumnYard.Example1.Input
{
    public enum ControlMode { None, Menu, Game, UI }

    /// <summary> Facade for Input System's InputMap </summary>
    public sealed class InputManager : SingletonPOCO<InputManager>
    {
        public ControlMode _controlMode;
        private GameInputActions _actions;

        public GameInputActions Actions => _actions;

        public InputManager()
        {
            _actions = new GameInputActions();
            _actions.Enable();

            Debug.Log("[InputManager] Created!");
        }

        public void EnablePlayer()
        {
            _actions.GamePlayer.Enable();
        }
        public void DisablePlayer()
        {
            _actions.GamePlayer.Disable();
        }

    }
}
