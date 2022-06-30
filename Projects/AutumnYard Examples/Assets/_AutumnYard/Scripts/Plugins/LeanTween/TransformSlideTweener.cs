using System;
using UnityEngine;

namespace AutumnYard.Tools.Tweening
{
    public sealed class TransformSlideTweener : ITweener
    {
        //public enum Direction { Up, Right, Down, Left }
        private const float padding = 20;

        private readonly Vector2 _from;
        private readonly Vector2 _to;
        private readonly RectTransform _rect;
        private readonly float _duration;
        private bool isVisible;

        public TransformSlideTweener(RectTransform rect, DirectionFourAxis direction, float duration)
        {
            _rect = rect;
            _to = rect.anchoredPosition;
            _from = FromDirection(direction, rect);
            _duration = duration;

            Vector2 FromDirection(DirectionFourAxis direction, RectTransform position)
            {
                switch (direction)
                {
                    case DirectionFourAxis.Left: return new Vector2(-rect.rect.width + padding, 0);
                    case DirectionFourAxis.Right: return new Vector2(rect.rect.width + padding, 0);
                    case DirectionFourAxis.Up: return new Vector2(0, rect.rect.height + padding);
                    case DirectionFourAxis.Down: return new Vector2(0, -rect.rect.height + padding);
                    default: throw new System.ArgumentOutOfRangeException(nameof(direction), direction, null);
                }
            }
        }

        public bool IsShowing => isVisible;


        public void Initialize() { }

        public void Show()
        {
            LeanTween.move(_rect, _to, _duration)
                .setEaseInOutCirc();
            isVisible = true;
        }

        public void ShowInstantly()
        {
            LeanTween.cancel(_rect);
            _rect.anchoredPosition = _to;
        }

        public void Hide(Action onComplete = null)
        {
            LeanTween.move(_rect, _from, _duration)
              .setEaseInOutCirc()
              .setOnComplete(onComplete);
            isVisible = false;
        }

        public void HideInstantly()
        {
            LeanTween.cancel(_rect);
            _rect.anchoredPosition = _from;
        }

    }
}
