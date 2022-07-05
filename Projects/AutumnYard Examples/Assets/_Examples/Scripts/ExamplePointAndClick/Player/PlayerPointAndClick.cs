
using System;

namespace AutumnYard.ExamplePointAndClick
{
    public sealed class PlayerPointAndClick : SingleInstance<PlayerPointAndClick>
    {
        public struct PlayerState
        {
            private int _coins;

            public int Coins => _coins;

            public void AddCoin(int quantity) => _coins += quantity;
        }

        private PlayerState _state;
        private Interacter _interacter;

        public PlayerState State => _state;
        public event Action<PlayerState> onPlayerStateChange;

        protected override void Awake()
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
                case PickableType.Coin: PickCoin(target); break;
            }

            void PickCoin(Pickable target)
            {
                _state.AddCoin(1);
                Destroy(target.gameObject);
                onPlayerStateChange?.Invoke(_state);
            }
        }

    }
}
