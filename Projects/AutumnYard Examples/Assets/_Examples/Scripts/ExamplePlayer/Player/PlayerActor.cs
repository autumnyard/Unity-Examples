using UnityEngine;
using AutumnYard.Common.Input;
using AutumnYard.ExamplePlayer.Input;

namespace AutumnYard.ExamplePlayer.Player
{
    public sealed class PlayerActor : MonoBehaviour
    {
        public enum State { Normal, Stopped }
        private State _currentState;
        private AutumnYard.Input.IInputProvider<PlayerInputs> _input;
        private AutumnYard.Input.IInputHandler<PlayerInputs> _movement;
        private PlayerCombat _combat;
        private InputManager _inputManager;

        public State CurrentState => _currentState;

        public void Configure(PlayerConfiguration configuration, InputManager inputManager)
        {
            _inputManager = inputManager;

            _combat = GetComponentInChildren<PlayerCombat>();
            _input = new PlayerInputProvider(_inputManager.Actions);
            _movement = new PlayerMovementWithTransform(transform, configuration);

            _inputManager.EnablePlayer();
        }
        public void ChangeState(State to)
        {
            if (_currentState == to) return;

            _currentState = to;
        }


        private void OnDestroy()
        {
            _inputManager.DisablePlayer();
        }

        private void Update()
        {
            if (_currentState == State.Stopped) return;

            var inputs = _input.GetInputs();
            _movement.UpdateWithInputs(inputs);
            if (_combat != null) _combat.UpdateWithInputs(inputs);
            //if (inputs.AnyInput) Debug.Log(inputs.ToString());
        }

    }
}
