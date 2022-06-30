using UnityEngine.UI;

namespace AutumnYard.Core.Display.UI
{
    public sealed class TextDisplayUnity : TextDisplay
    {
        private Text label;

        public override void Initialize()
        {
            if (label == null) label = GetComponent<Text>();
        }

        public override void Set<T>(T obj)
        {
            Set(obj.ToString());
        }

        public override void Set(string text)
        {
            Initialize();
            label.text = text;
        }

        public override void Set(string text, UnityEngine.Color color)
        {
            Initialize();
            label.text = text;
            label.color = color;
        }

        public override void Clear() => label.text = "";
    }
}
