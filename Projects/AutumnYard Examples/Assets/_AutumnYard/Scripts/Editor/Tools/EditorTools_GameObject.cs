using UnityEditor;
using UnityEngine;

namespace AutumnYard.Editor
{
    public static partial class EditorTools
    {

        [MenuItem("Autumn Yard/Toggle selected GameObject %g", priority = 60)]
        private static void ToggleGameObject()
        {
            //var selected = Selection.transforms;
            if (Selection.transforms != null)
            {
                foreach (Transform t in Selection.transforms)
                    t.gameObject.SetActive(!t.gameObject.activeInHierarchy);
            }
        }

        [MenuItem("Autumn Yard/Toggle selected GameObject: All false %#g", priority = 60)]
        private static void ToggleGameObjectFalse()
        {
            //var selected = Selection.transforms;
            if (Selection.transforms != null)
            {
                foreach (Transform t in Selection.transforms)
                    t.gameObject.SetActive(false);
            }
        }

        [MenuItem("Autumn Yard/Toggle selected GameObject: All true %#&g", priority = 60)]
        private static void ToggleGameObjectTrue()
        {
            //var selected = Selection.transforms;
            if (Selection.transforms != null)
            {
                foreach (Transform t in Selection.transforms)
                    t.gameObject.SetActive(true);
            }
        }
    }
}
