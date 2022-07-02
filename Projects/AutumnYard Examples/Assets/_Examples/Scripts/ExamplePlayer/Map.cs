using UnityEngine;

namespace AutumnYard.ExamplePlayer
{
    public sealed class Map : MonoBehaviour
    {
        [SerializeField] private Transform playerSpawn;

        public Transform PlayerSpawn => playerSpawn;
    }
}
