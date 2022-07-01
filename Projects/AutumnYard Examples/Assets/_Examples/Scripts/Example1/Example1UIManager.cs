using AutumnYard.Common.UI;

namespace AutumnYard.Example1.UI
{
    public sealed class Example1UIManager : Common.UI.UIManager
    {
        public enum Menu { None, Pause, Status, Inventory }

        protected override void SetDependencies()
        {
            if (menus == null || menus.Length == 0 || menus.Length != typeof(Menu).GetLength())
            {
                menus = new MenuBase[typeof(Menu).GetLength()];
                menus[(int)Menu.Status] = GetComponentInChildren<MenuStatus>();
                menus[(int)Menu.Inventory] = GetComponentInChildren<MenuInventory>();
                menus[(int)Menu.Pause] = GetComponentInChildren<MenuPause>();
            }

            base.SetDependencies();
        }

        public void OpenInventory(Inventory data)
        {
            if (_currentMenu != 0) return;

            (menus[(int)Menu.Inventory] as MenuInventory).Configure(data);
            Open((int)Menu.Inventory);

        }
        public void OpenStatus(MenuStatus.Data data)
        {
            if (_currentMenu != 0) return;

            (menus[(int)Menu.Status] as MenuStatus).Configure(data);
            Open((int)Menu.Status);
        }
        public void OpenPause()
        {
            if (_currentMenu != 0) return;

            Open((int)Menu.Pause);
        }
    }
}
