using AutumnYard.Common.UI;

namespace AutumnYard.ExampleWorld.UI
{
    public sealed class ExampleWorldUI : UIManager
    {
        public enum Menu { None, Pause }

        protected override void SetDependencies()
        {
            if (menus == null || menus.Length == 0 || menus.Length != typeof(Menu).GetLength())
            {
                menus = new MenuBase[typeof(Menu).GetLength()];
                menus[(int)Menu.Pause] = GetComponentInChildren<MenuPause>();
            }

            base.SetDependencies();
        }

        public void OpenPause()
        {
            if (_currentMenu != 0) return;

            Open((int)Menu.Pause);
        }
    }
}
