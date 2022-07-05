using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace AutumnYard.ExamplePointAndClick
{
    public sealed class Tweener_UpDown : MonoBehaviour
    {
        private Vector3 _initialPos;
        private Vector3 _finalPos;

        private void Awake()
        {
            _initialPos = transform.position;

            _finalPos = _initialPos + Vector3.one * .7f;
        }

        private void OnEnable()
        {
            transform.DOMoveY(_finalPos.y, .5f)
                .SetEase(Ease.OutCirc)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}
