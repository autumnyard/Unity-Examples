using UnityEngine;
using AutumnYard.Common.UI;
using UnityEngine.UI;

namespace AutumnYard.ExamplePointAndClick
{
    public sealed class HUDCoins : MonoBehaviour
    {
        [SerializeField] private Text coins;

        private void OnValidate()
        {
            if (coins == null) coins = GetComponentInChildren<Text>();
        }

        private void OnEnable()
        {
            HandlePlayerStateChange(PlayerPointAndClick.Instance.State);
            PlayerPointAndClick.Instance.onPlayerStateChange += HandlePlayerStateChange;
        }
        private void OnDisable()
        {
            PlayerPointAndClick.Instance.onPlayerStateChange -= HandlePlayerStateChange;
        }

        private void HandlePlayerStateChange(PlayerPointAndClick.PlayerState playerState)
        {
            coins.text = playerState.Coins.ToString();
        }
    }
}
