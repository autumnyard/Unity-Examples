using System;
using UnityEngine;

namespace AutumnYard.Tools.Tweening
{
    public static class TweeningTools
    {
        public static void TweenAlpha(this RectTransform @this, in float endValue, in float duration)
        {
            LeanTween.alpha(@this, endValue, duration);
        }
        public static void TweenAlpha(this RectTransform @this, in float endValue, in float duration, in Action onComplete)
        {
            LeanTween.alpha(@this, endValue, duration)
                .setOnComplete(onComplete);
        }
        public static void TweenCancel(this RectTransform @this)
        {
            LeanTween.cancel(@this);
        }

        public static LTDescr TweenMoveY(this RectTransform @this, in float endValue, in float duration)
        {
            return @this.LeanMoveLocalY(endValue, duration);
        }
        public static LTDescr SetEase(this LTDescr @this, in LeanTweenType ease)
        {
            return @this.setEase(ease);
        }
        public static LTDescr SetLoop(this LTDescr @this)
        {
            return @this.setLoopPingPong();
        }
    }
}