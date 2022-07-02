using UnityEngine;

namespace AutumnYard.ExamplePlayer.Player
{
    public sealed class PlayerMovementWithCharacterController : AutumnYard.Input.IInputHandler<Input.PlayerInputs>
    {
        private CharacterController _target;
        private PlayerConfiguration _configuration;
        private Vector3 _initialPosition;

        public PlayerMovementWithCharacterController(CharacterController target, PlayerConfiguration configuration, Vector3 initialPosition)
        {
            _target = target;
            _configuration = configuration;
            _initialPosition = initialPosition;
        }

        public void Clear()
        {
            //_target.Move = _initialPosition;
        }

        public void UpdateWithInputs(in Input.PlayerInputs inputs)
        {
            Vector3 move = new Vector3(inputs.h, _initialPosition.y, inputs.v);
            _target.Move(move * Time.deltaTime * _configuration.speed);
        }
    }
}
