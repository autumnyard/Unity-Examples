using System;
using UnityEngine;

namespace AutumnYard.Tools.Tweening
{
    public sealed class CanvasGroupTweener : ITweener
    {
        private readonly CanvasGroup _canvasGroup;
        private readonly float _duration;
        private readonly float _visibilityThreshold = 0.01f;
        private int _id;

        public bool IsShowing => _canvasGroup.alpha > _visibilityThreshold;

        public CanvasGroupTweener(CanvasGroup canvasGroup, float duration)
        {
            _canvasGroup = canvasGroup;
            this._duration = duration;
        }

        public void Initialize() => HideInstantly();

        public void Show()
        {
            HideInstantly();
            _id = LeanTween.alphaCanvas(_canvasGroup, 1, _duration).id;
        }

        public void ShowInstantly()
        {
            LeanTween.cancel(_canvasGroup.gameObject);
            LeanTween.cancel(_id);
            _canvasGroup.alpha = 1;
        }


        public void Hide(Action onComplete = null)
        {
            ShowInstantly();
            _id = LeanTween.alphaCanvas(_canvasGroup, 0, _duration)
              .setOnComplete(onComplete)
              .id;
        }

        public void HideInstantly()
        {
            LeanTween.cancel(_canvasGroup.gameObject);
            LeanTween.cancel(_id);
            _canvasGroup.alpha = 0;
        }

    }
}
