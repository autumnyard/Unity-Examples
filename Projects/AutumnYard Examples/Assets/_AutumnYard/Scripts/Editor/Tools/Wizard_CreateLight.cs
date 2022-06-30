
namespace AutumnYard.Editor
{
    // Source: https://docs.unity3d.com/ScriptReference/ScriptableWizard.html

    // Creates a simple wizard that lets you create a Light GameObject
    // or if the user clicks in "Apply", it will set the color of the currently
    // object selected to red

    using UnityEditor;
    using UnityEngine;

    public class Wizard_CreateLight : ScriptableWizard
    {
        public float range = 500;
        public Color color = Color.red;

        [MenuItem("Autumn Yard/Wizards/Create Light", priority = 1000)]
        private static void CreateWizard()
        {
            ScriptableWizard.DisplayWizard<Wizard_CreateLight>("Create Light", "Create", "Apply");
            //If you don't want to use the secondary button simply leave it out:
            //ScriptableWizard.DisplayWizard<WizardCreateLight>("Create Light", "Create");
        }

        private void OnWizardCreate()
        {
            GameObject go = new GameObject("New Light");
            Light lt = go.AddComponent<Light>();
            lt.range = range;
            lt.color = color;
        }

        private void OnWizardUpdate()
        {
            helpString = "Please set the color of the light!";
        }

        // When the user presses the "Apply" button OnWizardOtherButton is called.
        private void OnWizardOtherButton()
        {
            if (Selection.activeTransform != null)
            {
                Light lt = Selection.activeTransform.GetComponent<Light>();

                if (lt != null)
                {
                    lt.color = Color.red;
                }
            }
        }
    }
}
