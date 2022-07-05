using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutumnYard.ExamplePointAndClick
{
    [RequireComponent(typeof(Collider))]
    public sealed class Pickable : MonoBehaviour
    {
        [SerializeField] private PickableType type;

        public PickableType Type => type;

        private void OnValidate()
        {
            if (!CompareTag("Pickable")) tag = "Pickable";
        }
    }
}
