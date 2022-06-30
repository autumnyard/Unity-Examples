using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
//using Sirenix.OdinInspector; // TODO: Sirenix Odin dependency: ShowInInspector, DisableIf, InlineButton, Button

namespace AutumnYard.Tools
{
    public sealed class ScreenshotMaker : MonoBehaviour
    {
        public enum Format { jpg, png }
        [SerializeField] private Key key = Key.PrintScreen;
        [SerializeField] private string path = $"../../";
        [SerializeField] private new string name = $"Screenshot";
        [SerializeField] private Format format = Format.png;

        [SerializeField] private bool appendSequence = false;

        //[ShowInInspector, DisableIf("@appendSequence == false")]
        //[InlineButton("ResetSequence")]
        private int sequence = 0;

        [SerializeField] private bool appendDate = true;


        private void Update()
        {
            if (Keyboard.current[key].wasPressedThisFrame)
            {
                TakeScreenshot();
            }
        }


        [ContextMenu("Reset Sequence")]
        private void ResetSequence() => sequence = 0;

        //[Button]
        [ContextMenu("Take Screenshot")]
        private void TakeScreenshot()
        {
            StringBuilder final = new StringBuilder(path);
            final.Append(name);

            if (appendSequence)
            {
                final.Append("_").Append(sequence);
                sequence++;
            }

            if (appendDate)
            {
                final.Append("_").Append(System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
            }

            final.Append(".").Append(format.ToString());

            ScreenCapture.CaptureScreenshot(final.ToString());
        }
    }
}
