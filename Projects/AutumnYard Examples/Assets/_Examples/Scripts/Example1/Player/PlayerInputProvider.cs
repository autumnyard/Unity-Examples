using UnityEngine;
using AutumnYard.Input;
using AutumnYard.Plugins.InputSystem;

namespace AutumnYard.Example1.Input
{
    public sealed class PlayerInputProvider :
        IInputProvider<PlayerInputs>
    {
        private GameInputActions.GamePlayerActions _actions;

        public PlayerInputProvider(GameInputActions actions)
        {
            _actions = actions.GamePlayer;
        }

        public PlayerInputs GetInputs()
            => new PlayerInputs(
                _actions.Move.ReadValue<Vector2>().x,
                _actions.Move.ReadValue<Vector2>().y,
                Plugins.InputSystem.Tools.ButtonDown(_actions.Attack),
                Plugins.InputSystem.Tools.ButtonHeld(_actions.Attack),
                Plugins.InputSystem.Tools.ButtonDown(_actions.Defense),
                Plugins.InputSystem.Tools.ButtonHeld(_actions.Defense),
                Plugins.InputSystem.Tools.ButtonDown(_actions.Jump),
                Plugins.InputSystem.Tools.ButtonHeld(_actions.Jump)
                );

        public void Subscribe() { }
        public void Unsubscribe() { }
        public void EndFrame() { }
        public void Clear() { }

    }
}
