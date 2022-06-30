using UnityEngine;

namespace AutumnYard.Core.Display.UI
{
    public abstract class TextDisplay : MonoBehaviour
    {
        public abstract void Initialize();
        public abstract void Set<T>(T text);
        public abstract void Set(string text);
        public abstract void Set(string text, Color color);
        public abstract void Clear();
    }
}
