using UnityEngine;
using UnityEngine.UI;

namespace AutumnYard.Example1.UI
{
    public sealed class UIManager : SingleInstance<UIManager>
    {
        public enum Menu { None, Status, Inventory, Pause }
        private Menu _currentMenu;

        [NamedArray(typeof(Menu))]
        [SerializeField] private MenuBase[] menus;

        [SerializeField] private Image background;
        private MenuTouch _menuTouch;

        public Menu CurrentMenu => _currentMenu;

        private void OnValidate()
        {
            SetDependencies();
        }

        protected override void Awake()
        {
            base.Awake();

            SetDependencies();

#if UNITY_ANDROID
            _menuTouch.Open();
#else
            _menuTouch.Close();
#endif
        }

        [ContextMenu("Set Dependencies")]
        private void SetDependencies()
        {
            if (menus == null || menus.Length == 0 || menus.Length != typeof(Menu).GetLength())
            {
                menus = new MenuBase[typeof(Menu).GetLength()];
                //Debug.LogWarning("[UI Manager] Panels is empty.");
                menus[(int)Menu.Status] = GetComponentInChildren<MenuStatus>();
                menus[(int)Menu.Inventory] = GetComponentInChildren<MenuInventory>();
                menus[(int)Menu.Pause] = GetComponentInChildren<MenuPause>();
            }

            _menuTouch = GetComponentInChildren<MenuTouch>();
        }

        private void Open(Menu which)
        {
            if (_currentMenu != Menu.None) return;
            if (_currentMenu == which) return;

            background.enabled = true;

            menus[(int)which].Open();
            _currentMenu = which;
        }

        public void Close()
        {
            if (_currentMenu == Menu.None) return;

            menus[(int)_currentMenu].Close();
            _currentMenu = Menu.None;

            background.enabled = false;
        }


        #region Specific Menus

        public void OpenInventory(Inventory data)
        {
            if (_currentMenu != Menu.None) return;

            (menus[(int)Menu.Inventory] as MenuInventory).Configure(data);
            Open(Menu.Inventory);

        }
        public void OpenStatus(MenuStatus.Data data)
        {
            if (_currentMenu != Menu.None) return;

            (menus[(int)Menu.Status] as MenuStatus).Configure(data);
            Open(Menu.Status);
        }
        public void OpenPause()
        {
            if (_currentMenu != Menu.None) return;

            Open(Menu.Pause);
        }

        #endregion // Specific Menus
    }
}
