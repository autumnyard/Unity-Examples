using UnityEditor;
using UnityEngine;

namespace AutumnYard.Editor
{
    [InitializeOnLoad]
    public static class CustomHierarchy_Tags
    {
        private static readonly GUIStyle style = new GUIStyle()
        {
            normal = new GUIStyleState() { textColor = Color.gray }, // new Color(0.16f, 0.16f, 0.16f);
            fontSize = 10,
            fontStyle = FontStyle.Italic,
            alignment = TextAnchor.MiddleRight
        };


        static CustomHierarchy_Tags()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
        }

        private static void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            //if (!GlobalSettings.Instance.CheckHierarchy(GlobalSettings.CustomHierarchy.Tags))
            //    return;

            var go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

            if (go == null) return;

            if (!go.CompareTag("Untagged"))
            {
                EditorGUI.LabelField(selectionRect, $"tag: {go.tag}", style);
            }
        }
    }
}
