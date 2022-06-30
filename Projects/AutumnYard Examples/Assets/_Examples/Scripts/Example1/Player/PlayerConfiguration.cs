using UnityEngine;

namespace AutumnYard.Example1.Player
{
    [CreateAssetMenu(fileName = "Player Configuration", menuName = "Autumn Yard/Player Configuration", order = 20)]
    public sealed class PlayerConfiguration : ScriptableObject
    {
        [Header("Movement")]
        public float speed = 2f; // 2f

        [Header("Combat")]
        public float attackDuration = .2f; // .2f
        public float parryDuration = .3f; // .3f
    }
}