using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutumnYard.ExamplePointAndClick
{
    public sealed class Interacter : MonoBehaviour
    {
        [SerializeField] private string targetTag = "Pickable";
        public event Action<Pickable> onInteract;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(targetTag)) return;

            onInteract?.Invoke(other.GetComponent<Pickable>());
        }


    }
}
