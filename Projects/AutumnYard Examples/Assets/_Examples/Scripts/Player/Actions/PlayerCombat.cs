using UnityEngine;

namespace AutumnYard.Example1.Player
{
    public sealed class PlayerCombat : MonoBehaviour, AutumnYard.Input.IInputHandler<Input.PlayerInputs>
    {
        private enum State { Blocked, Idle, Attack, Parry, Defense, Hurt }
        [SerializeField] private GameObject attack;
        [SerializeField] private GameObject parry;
        [SerializeField] private GameObject defense;
        [SerializeField] private PlayerConfiguration configuration;
        private Tools.CounterFloat _attackCounter;
        private Tools.CounterFloat _parryCounter;
        private State _state;
        private State _newState;

        private void Awake()
        {
            _attackCounter = new Tools.CounterFloat(configuration.attackDuration);
            _parryCounter = new Tools.CounterFloat(configuration.parryDuration);
        }
        private void OnEnable()
        {
            Clear();
        }

        [ContextMenu("Clear State")]
        public void Clear()
        {
            _attackCounter.Reset();
            _parryCounter.Reset();
            _state = State.Idle;
            _newState = State.Idle;
        }

        public void UpdateWithInputs(in Input.PlayerInputs inputs)
        {
            if (inputs.attackPressed)
            {
                _parryCounter.Reset();
                _attackCounter.Reset();
                _state = State.Attack;
            }
            else if (inputs.attackMaintained)
            {
                _state = State.Attack;
            }
            else if (inputs.defensePressed)
            {
                _attackCounter.Reset();
                _parryCounter.Reset();
                _state = State.Defense;
            }
            else if (inputs.defenseMaintained)
            {
                _state = State.Defense;
            }
            else
            {
                _attackCounter.Reset();
                _parryCounter.Reset();
                _state = State.Idle;
            }

            switch (_state)
            {
                case State.Attack:
                    _newState = _attackCounter.Tick(Time.deltaTime) ? State.Idle : State.Attack;
                    break;

                case State.Defense:
                    _newState = _parryCounter.Tick(Time.deltaTime) ? State.Defense : State.Parry;
                    break;

                default:
                    _newState = State.Idle;
                    break;
            }

            attack.SetActive(_newState == State.Attack);
            parry.SetActive(_newState == State.Parry);
            defense.SetActive(_newState == State.Defense);
        }
    }
}
