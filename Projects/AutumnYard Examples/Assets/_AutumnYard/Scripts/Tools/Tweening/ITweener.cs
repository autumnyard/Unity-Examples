using System;

namespace AutumnYard.Tools.Tweening
{
    public interface ITweener
    {
        bool IsShowing { get; }
        void Initialize();
        void Show();
        void ShowInstantly();
        void Hide(Action onComplete = null);
        void HideInstantly();
    }
}
