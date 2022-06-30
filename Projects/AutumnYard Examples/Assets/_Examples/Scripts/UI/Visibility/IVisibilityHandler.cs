
namespace AutumnYard.Example1.UI
{
    public interface IVisibilityHandler
    {
        public bool IsVisible { get; }
        public void Show();
        public void ShowImmediate();
        public void Hide();
        public void HideImmediate();
    }
}
