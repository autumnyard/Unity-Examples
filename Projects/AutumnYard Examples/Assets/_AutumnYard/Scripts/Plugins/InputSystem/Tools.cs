using UnityEngine.InputSystem;

namespace AutumnYard.Plugins.InputSystem
{
    public static class Tools
    {
        public static bool ButtonDown(InputAction action)
        {
            return action.ReadValue<float>() > 0.5f && action.triggered;
        }

        public static bool ButtonUp(InputAction action)
        {
            return action.ReadValue<float>() < 0.5f && action.triggered;
        }

        public static bool ButtonHeld(InputAction action)
        {
            return action.ReadValue<float>() > 0.5f;
        }
    }
}
