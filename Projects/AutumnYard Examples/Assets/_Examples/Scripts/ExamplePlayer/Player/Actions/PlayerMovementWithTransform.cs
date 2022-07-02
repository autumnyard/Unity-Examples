using UnityEngine;

namespace AutumnYard.ExamplePlayer.Player
{
    public sealed class PlayerMovementWithTransform : AutumnYard.Input.IInputHandler<Input.PlayerInputs>
    {
        private Transform _target;
        private PlayerConfiguration _configuration;
        private Vector3 _initialPosition;

        public PlayerMovementWithTransform(Transform target, PlayerConfiguration configuration)
        {
            _target = target;
            _configuration = configuration;
            _initialPosition = target.position;
        }

        public PlayerMovementWithTransform(Transform target, PlayerConfiguration configuration, Vector3 initialPosition)
        {
            _target = target;
            _configuration = configuration;
            _initialPosition = initialPosition;
        }

        public void Clear()
        {
            _target.position = _initialPosition;
        }

        public void UpdateWithInputs(in Input.PlayerInputs inputs)
        {
            if (inputs.v != 0f)
            {
                _target.position += Vector3.forward * inputs.v * _configuration.speed * Time.deltaTime;
            }

            if (inputs.h != 0f)
            {
                _target.position += Vector3.right * inputs.h * _configuration.speed * Time.deltaTime;
            }
        }
    }
}
