using UnityEngine;

namespace AutumnYard.Example1
{
    public sealed class GlobalEvents : MonoBehaviour
    {
        [SerializeField] private bool _emitLogs = false;


        private void Log(string text)
        {
            if (_emitLogs) Debug.Log($" --- <b>Global Events: {text}.</b>");
        }

        private void Awake()
        {
            Application.wantsToQuit += HandleWantsToQuit;
            Application.focusChanged += HandleFocusChange;
            Application.quitting += HandleQuitting;
        }

        private void OnApplicationFocus(bool focus)
        {
            Log($"OnApplicationFocus to {focus}");
        }

        private void OnApplicationPause(bool pause)
        {
            Log($"OnApplicationPause to {pause}");
        }

        private void OnApplicationQuit()
        {
            Log("OnApplicationQuit");
        }


        private void HandleFocusChange(bool focus)
        {
            Log($"Focus change to {focus}");
        }

        private bool HandleWantsToQuit()
        {
            Log("Player wants to quit. Allowing it");
            return true;
        }

        private void HandleQuitting()
        {
            Log("Player quitting");
        }
    }
}
