
using System;
using UnityEngine;

namespace AutumnYard.Example1.UI
{
    public sealed partial class HUDManager : SingleInstance<HUDManager>
    {
        public enum State { Empty, Game, All }
        private State _currentState;

        public enum HUD { PlayerStats, }

        [NamedArray(typeof(HUD))]
        [SerializeField]
        private HUDBase[] huds;

        private Example1Director _director;
        private HUDCollection[] _states;

        public State CurrentState => _currentState;

        protected override void Awake()
        {
            base.Awake();

            SetDependencies();

            _states = new HUDCollection[]
            {
                new HUDCollection(), // Empty
                new HUDCollection(huds[(int)HUD.PlayerStats]), // Game
                new HUDCollection(huds[(int)HUD.PlayerStats]), // All
            };
        }

        [ContextMenu("Set Dependencies")]
        private void SetDependencies()
        {
            if (huds == null || huds.Length == 0 || huds.Length != typeof(HUD).GetLength())
            {
                huds = new HUDBase[typeof(HUD).GetLength()];
                //Debug.LogWarning("[HUD Manager] Panels is empty.");
                huds[(int)HUD.PlayerStats] = GetComponentInChildren<HUDPlayerStats>();
            }

            if (_director == null) _director = FindObjectOfType<Example1Director>();
        }

        private void Start()
        {
            _states[(int)State.All].Exit();
            SetState(State.Game);
            _director.onChangeState += HandleChangeGameState;
        }


        protected override void OnDestroy()
        {
            _director.onChangeState -= HandleChangeGameState;
            _states[(int)State.All].Exit();

            base.OnDestroy();
        }

        #region FSM

        private void HandleChangeGameState(Example1Director.State newGameState)
        {
            switch (newGameState)
            {
                case Example1Director.State.Play:
                    SetState(State.Game);
                    break;

                case Example1Director.State.UI:
                    SetState(State.Empty);
                    break;

                default:
                    Debug.LogError($"[HUD Manager] HUD not implemented for Game State {newGameState}");
                    SetState(State.Empty);
                    break;
            }
        }
        public void SetState(State newState)
        {
            if (_currentState == newState) return;

            _states[(int)_currentState].Exit();
            _currentState = newState;
            _states[(int)_currentState].Enter();
        }

        #endregion // FSM

#if UNITY_EDITOR
        [ContextMenu("Set Next State %h")]
        private void Editor_SetNextState()
        {
            State newState = _currentState + 1;
            if ((int)newState >= typeof(State).GetLength())
                newState = 0;

            SetState(newState);
        }
#endif
    }
}
