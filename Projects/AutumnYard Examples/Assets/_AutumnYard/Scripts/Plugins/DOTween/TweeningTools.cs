using System;
using UnityEngine;
using DG.Tweening;

namespace AutumnYard.Tools.Tweening
{
    public static class TweeningTools
    {
        public static Tweener TweenMoveY2(this RectTransform @this, in float to, in float duration)
        {
            return @this.DOLocalMoveY(to, duration);
        }
        public static Tweener TweenAlpha(this CanvasGroup @this, in float endValue, in float duration)
        {
            return @this.DOFade(endValue, duration);
        }
        public static Tweener TweenSize(this RectTransform @this, in Vector3 to, in float duration)
        {
            return @this.DOScale(to, duration);
        }

        public static Tweener SetEase2(this Tweener @this, in Ease ease)
        {
            return @this.SetEase(ease);
        }
        public static Tweener SetDelay2(this Tweener @this, in float delay)
        {
            return @this.SetDelay(delay);
        }
        public static Tweener SetOnComplete(this Tweener @this, Action onComplete)
        {
            return @this.OnComplete(() => { onComplete.Invoke(); });
        }
        public static Tweener SetLoop2(this Tweener @this)
        {
            return @this.SetLoops(-1);
        }
    }
}
