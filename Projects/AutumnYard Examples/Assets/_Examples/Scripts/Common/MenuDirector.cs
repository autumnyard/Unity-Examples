using UnityEngine;
using UnityEngine.EventSystems;

namespace AutumnYard.Common.Menu
{
    public sealed class MenuDirector : MonoBehaviour
    {
        public enum Panel { Main, Options }
        private Panel _currentPanel;

        [NamedArray(typeof(Panel))]
        [SerializeField] private GameObject[] panels;

        [NamedArray(typeof(Panel))]
        [SerializeField] private GameObject[] defaultSelectionForPanel;

        [SerializeField] private GameObject buttonExit;

        private void OnValidate()
        {
            if (panels == null || panels.Length == 0)
            {
                panels = new GameObject[typeof(Panel).GetLength()];
                Debug.LogError("[MenuDirector] Panels is empty.");
            }

            if (defaultSelectionForPanel == null || defaultSelectionForPanel.Length == 0)
            {
                defaultSelectionForPanel = new GameObject[typeof(Panel).GetLength()];
                Debug.LogError("[MenuDirector] Default Selections for panels is empty!.");
            }
        }

        private void Awake()
        {
            SceneHandler.Instance.ForceSetCurrentContext(SceneHandler.Context.Menu);
            ChangeState(Panel.Main, true);

#if UNITY_ANDROID
            buttonExit.SetActive(false);
#else
            buttonExit.SetActive(true);
#endif
        }

        public void ChangeState(Panel newState, bool force = false)
        {
            if (!force && _currentPanel == newState) return;

            panels[(int)Panel.Main].SetActive(newState == Panel.Main);
            panels[(int)Panel.Options].SetActive(newState == Panel.Options);

            EventSystem.current.SetSelectedGameObject(defaultSelectionForPanel[(int)newState]);

            _currentPanel = newState;
        }

        public void Button_Main_PlayExample(int index) => SceneHandler.Instance.ChangeContext(SceneHandler.Context.ExamplePlayer + index);
        public void Button_Main_Options() => ChangeState(Panel.Options);
        public void Button_Main_Exit() => Application.Quit();
        public void Button_Back() => ChangeState(Panel.Main);

    }
}
