using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AutumnYard.Common;
using AutumnYard.Common.Input;

namespace AutumnYard.Example2
{
    public sealed class GameDirector : MonoBehaviour
    {
        public enum State { Play, UI }
        private State _currentState;
        public event Action<State> onChangeState;
        public State CurrentState => _currentState;

        private void Awake()
        {
            //_ui = UI.UIManager.Instance;
            SceneHandler.Instance.ForceSetCurrentContext(SceneHandler.Context.Example1);

            InputManager.Instance.Actions.GameCommands.Enable();
        }
    }
}
