using System;
using UnityEngine;
using DG.Tweening;

namespace AutumnYard.Tools.Tweening
{
    public sealed class CanvasGroupTweener : ITweener
    {
        private readonly CanvasGroup _canvasGroup;
        private readonly float _duration;
        private readonly float _visibilityThreshold = 0.01f;
        private Tweener _tweenIn;
        private Tweener _tweenOut;

        public bool IsShowing => _canvasGroup.alpha > _visibilityThreshold;

        public CanvasGroupTweener(CanvasGroup canvasGroup, float duration)
        {
            _canvasGroup = canvasGroup;
            _duration = duration;
            _tweenIn = _canvasGroup.DOFade(0f, _duration);
            _tweenIn.SetAutoKill(false).Pause();
            _tweenIn.SetEase(Ease.InQuad);

            _tweenOut = _canvasGroup.DOFade(1f, _duration);
            _tweenOut.SetAutoKill(false).Pause();
            _tweenOut.SetEase(Ease.OutQuad);
        }

        public void Initialize() => HideInstantly();

        public void Hide(Action onComplete = null)
        {
            ShowInstantly();
            _tweenIn.OnComplete(() => { onComplete.Invoke(); });
            _tweenIn.Restart();
        }

        public void HideInstantly()
        {
            //LeanTween.cancel(_id);
            _tweenIn.Pause();
            _tweenOut.Pause();
            _canvasGroup.alpha = 0;
        }

        public void Show()
        {
            HideInstantly();
            _tweenOut.Restart();
        }

        public void ShowInstantly()
        {
            //LeanTween.cancel(_id);
            _tweenIn.Pause();
            _tweenOut.Pause();
            _canvasGroup.alpha = 1;
        }

    }


}
