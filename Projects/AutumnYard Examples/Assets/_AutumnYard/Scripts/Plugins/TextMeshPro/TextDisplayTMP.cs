using TMPro;

namespace AutumnYard.Core.Display.UI
{
    public sealed class TextDisplayTMP : TextDisplay
    {
        private TextMeshProUGUI label;

        public override void Initialize()
        {
            if (label == null) label = GetComponent<TextMeshProUGUI>();
        }

        public override void Clear()
        {
            Initialize();
            label.SetText("");
        }

        public override void Set<T>(T obj)
        {
            Set(obj.ToString());
        }

        public override void Set(string text)
        {
            Initialize();
            label.SetText(text);
        }

        public override void Set(string text, UnityEngine.Color color)
        {
            Initialize();
            label.SetText(text);
            label.color = color;
        }
    }
}
