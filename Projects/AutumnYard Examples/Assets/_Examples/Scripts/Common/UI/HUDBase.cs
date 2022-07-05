using UnityEngine;
using AutumnYard.Common.UI;

namespace AutumnYard.Common.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class HUDBase : MonoBehaviour
    {
        private IVisibilityHandler _visibility;

        private void Awake()
        {
            var canvas = GetComponent<CanvasGroup>();
            if (canvas == null) Debug.LogError($"[MenuBase {name}] Can't find CanvasGroup component.");

            _visibility = new VisibilityHandler_CanvasGroup(canvas);
        }

        public void Show()
        {
            Configure();
            _visibility.Show();

        }
        public void ShowInstantly()
        {
            Configure();
            _visibility.Show();
        }
        public void Hide()
        {
            _visibility.Hide();
            Clear();
        }
        public void HideInstantly()
        {
            _visibility.Hide();
            Clear();
        }

        protected virtual void Clear() { }
        protected virtual void Configure() { }
        public virtual void HandleUpdate() { }
    }
}
