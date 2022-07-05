using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutumnYard.ExamplePointAndClick
{
    public sealed class PlayerPointAndClick : MonoBehaviour
    {
        public struct PlayerState
        {
            public int Coins;

            public void AddCoin(int quantity) => Coins += quantity;
        }

        private PlayerState _state;
        private Interacter _interacter;

        private void Awake()
        {
            if (_interacter == null) _interacter = GetComponentInChildren<Interacter>();
        }

        private void Start()
        {
            _state = new PlayerState();
        }

        private void OnEnable()
        {
            _interacter.onInteract += HandleInteraction;
        }
        private void OnDisable()
        {
            _interacter.onInteract -= HandleInteraction;
        }

        private void HandleInteraction(Pickable target)
        {
            switch (target.Type)
            {
                case PickableType.Coin:
                    PickCoin(target);
                    break;
            }
        }

        private void PickCoin(Pickable target)
        {
            _state.AddCoin(1);
            Destroy(target.gameObject);
        }

        private void OnGUI()
        {
            GUILayout.Label($"Coints: {_state.Coins}");
        }

    }
}
