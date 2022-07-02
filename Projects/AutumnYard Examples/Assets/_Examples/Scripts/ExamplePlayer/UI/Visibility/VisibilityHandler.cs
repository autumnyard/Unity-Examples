
using UnityEngine;

namespace AutumnYard.ExamplePlayer.UI
{
    public abstract class VisibilityHandler : IVisibilityHandler
    {
        public abstract bool IsVisible { get; }
        public abstract void Hide();
        public abstract void HideImmediate();
        public abstract void Show();
        public abstract void ShowImmediate();
    }
}
