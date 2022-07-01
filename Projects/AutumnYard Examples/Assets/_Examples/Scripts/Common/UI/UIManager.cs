using UnityEngine;
using UnityEngine.UI;

namespace AutumnYard.Common.UI
{
    public abstract class UIManager : MonoBehaviour
    {
        protected int _currentMenu;
        protected MenuBase[] menus;

        [SerializeField] private GameObject background;
        [SerializeField] private MenuTouch _menuTouch;

        public int CurrentMenu => _currentMenu;

        private void OnValidate()
        {
            SetDependencies();
        }

        private void Awake()
        {
            SetDependencies();
        }

        private void Start()
        {
            background.SetActive(false);
#if UNITY_ANDROID
            _menuTouch.Open();
#else
            _menuTouch.Close();
#endif
        }

        [ContextMenu("Set Dependencies")]
        protected virtual void SetDependencies()
        {
            _menuTouch = GetComponentInChildren<MenuTouch>();
        }

        protected void Open(int which)
        {
            if (_currentMenu != 0) return;
            if (_currentMenu == which) return;

            background.SetActive(true);
            _menuTouch.Close();

            menus[(int)which].Open();
            _currentMenu = which;
        }

        public void Close()
        {
            if (_currentMenu == 0) return;

            menus[(int)_currentMenu].Close();
            _currentMenu = 0;

            background.SetActive(false);
            _menuTouch.Open();
        }


        #region Specific Menus


        #endregion // Specific Menus
    }
}
