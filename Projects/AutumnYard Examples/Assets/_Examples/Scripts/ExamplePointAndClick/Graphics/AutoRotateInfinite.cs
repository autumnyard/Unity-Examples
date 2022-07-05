using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutumnYard.ExamplePointAndClick
{
    public sealed class AutoRotateInfinite : MonoBehaviour
    {
        [SerializeField] private float multiplier = 90f;

        private void Update()
        {
            //transform.Rotate(Vector3.forward * Time.deltaTime * multiplier);
            transform.eulerAngles += new Vector3(0, Time.deltaTime * multiplier, 0);
        }
    }
}
