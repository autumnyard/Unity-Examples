
namespace AutumnYard.Editor
{
    // Source: https://forum.unity.com/threads/editor-script-to-change-multiple-materials.46911/

    using UnityEditor;
    using UnityEngine;

    public sealed class Wizard_MassMaterialChange : ScriptableWizard
    {
        public enum colorProperty { MainColor = 1, SpecularColor = 2, EmissionColor = 3, ReflectionColor = 4 };
        public colorProperty ColorProperty;
        public Color newColor;

        private void OnWizardCreate()
        {
            ChangeColors((int)ColorProperty, newColor);
        }

        [MenuItem("Autumn Yard/Wizards/Change Material Colors", priority = 1000)]
        private static void CreateWizard()
        {
            ScriptableWizard.DisplayWizard(
                "Change Materials Colors", typeof(Wizard_MassMaterialChange), "Change"
            );
        }

        private static Object[] GetSelectedMaterials()
        {
            return Selection.GetFiltered(typeof(Material), SelectionMode.DeepAssets);
        }

        private static void TurnOff()
        {
            if (Selection.objects.Length > 0)
            {
                Object[] materiales = GetSelectedMaterials();
                if (materiales.Length > 0)
                {
                    foreach (Material mat in materiales)
                    {

                    }
                }
            }
        }

        private static void ChangeColors(int ColorProperty, Color newColor)
        {
            int counter = 0;

            if (Selection.objects.Length > 0)
            {
                Object[] materiales = GetSelectedMaterials();
                if (materiales.Length > 0)
                {
                    foreach (Material mat in materiales)
                    {
                        string property = "";
                        if (ColorProperty == 1) property = "_Color";
                        if (ColorProperty == 2) property = "_SpecColor";
                        if (ColorProperty == 3) property = "_Emission";
                        if (ColorProperty == 4) property = "_ReflectColor";
                        if (mat.HasProperty(property))
                        {
                            mat.SetColor(property, newColor);
                            counter++;
                        }
                    }

                }
            }

            EditorUtility.DisplayDialog("Message", "materials changed: " + counter, "OK");
        }

        private static void ChangeShaders(Shader newShader)
        {
            int counter = 0;
            if (Selection.objects.Length > 0)
            {
                Object[] materiales = GetSelectedMaterials();
                if (materiales.Length > 0)
                {
                    foreach (Material mat in materiales)
                    {
                        mat.shader = newShader;
                        counter++;
                    }

                }
            }
            EditorUtility.DisplayDialog("Message", "materials changed: " + counter, "OK");
        }

    }
}
