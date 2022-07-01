using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AutumnYard.Common.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class MenuBase : MonoBehaviour
    {
        [SerializeField] private Selectable defaultSelection;
        private IVisibilityHandler _visibility;

        private void Awake()
        {
            var canvas = GetComponent<CanvasGroup>();
            if (canvas == null) Debug.LogError($"[MenuBase {name}] Can't find CanvasGroup component.");

            _visibility = new VisibilityHandler_CanvasGroup(canvas);
            Close();
        }

        public void Open()
        {
            _visibility.Show();

            if (defaultSelection != null)
                EventSystem.current.SetSelectedGameObject(defaultSelection.gameObject);
        }
        public void Close()
        {
            _visibility.Hide();
            Clear();
        }

        public virtual void Clear() { }
        protected virtual void Cancel() { }
    }
}
