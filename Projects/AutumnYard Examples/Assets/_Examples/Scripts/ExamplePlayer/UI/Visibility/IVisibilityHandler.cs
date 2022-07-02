
namespace AutumnYard.ExamplePlayer.UI
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
