using UnityEditor;
using UnityEngine;

namespace AutumnYard.Editor
{
    [InitializeOnLoad]
    public static class CustomHierarchy_Headers
    {
        private static Color32 backgroundColor_header = new Color(0.16f, 0.16f, 0.16f);
        private static Color32 backgroundColor_normal = new Color(0.20f, 0.2f, 0.2f);

        private static readonly GUIStyle headerStyle = new GUIStyle()
        {
            normal = new GUIStyleState() { textColor = Color.white },
            fontStyle = FontStyle.Normal,
            alignment = TextAnchor.MiddleCenter,
            fontSize = 16,
        };

        private static readonly GUIStyle singletonStyle = new GUIStyle()
        {
            normal = new GUIStyleState() { textColor = new Color32(200, 255, 180, 255) },
            fontStyle = FontStyle.BoldAndItalic,
        };

        private static readonly GUIStyle prototypeEnabledStyle = new GUIStyle()
        {
            normal = new GUIStyleState() { textColor = new Color32(245, 200, 210, 255) },
            fontStyle = FontStyle.BoldAndItalic,
        };
        private static readonly GUIStyle prototypeDisabledStyle = new GUIStyle()
        {
            normal = new GUIStyleState() { textColor = new Color32(245, 200, 210, 120) },
            fontStyle = FontStyle.BoldAndItalic,
        };

        static CustomHierarchy_Headers()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
        }

        private static void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            //if (!GlobalSettings.Instance.CheckHierarchy(GlobalSettings.CustomHierarchy.Headers))
            //    return;

            var go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

            if (go == null) return;

            if (go.name.StartsWith("---", System.StringComparison.Ordinal))
            {
                go.tag = "EditorOnly";
                EditorGUI.DrawRect(selectionRect, backgroundColor_header);
                EditorGUI.DropShadowLabel(selectionRect, go.name.Replace("-", "").ToUpperInvariant(), headerStyle);
            }
            else if (go.name.StartsWith("[", System.StringComparison.Ordinal)
              && go.name.EndsWith("]", System.StringComparison.Ordinal))
            {
                EditorGUI.DrawRect(selectionRect, backgroundColor_normal);
                if (go.activeInHierarchy)
                    EditorGUI.LabelField(selectionRect, go.name.Replace("[", "").Replace("]", ""), prototypeEnabledStyle);
                else
                    EditorGUI.LabelField(selectionRect, go.name.Replace("[", "").Replace("]", ""), prototypeDisabledStyle);
            }
        }
    }
}
