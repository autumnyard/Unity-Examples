using UnityEditor;
using UnityEngine;

namespace AutumnYard.Editor
{
    [InitializeOnLoad]
    public static class CustomHierarchy_Layers
    {
        static CustomHierarchy_Layers()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
        }

        private static void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            //if (!GlobalSettings.Instance.CheckHierarchy(GlobalSettings.CustomHierarchy.Layers))
            //    return;

            var go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

            if (go == null) return;


            if (go.layer != 0)
            {

                string layerName = LayerMask.LayerToName(go.layer);
                if (layerName.Contains("SmallObj") || layerName.Contains("BigObj"))
                {
                    EditorGUI.LabelField(selectionRect, LayerMask.LayerToName(go.layer), new GUIStyle()
                    {
                        normal = new GUIStyleState() { textColor = Color.yellow },
                        fontStyle = FontStyle.Italic,
                        alignment = TextAnchor.MiddleRight
                    });
                }
                else if
                   (layerName.Contains("CamColli"))
                {
                    EditorGUI.LabelField(selectionRect, LayerMask.LayerToName(go.layer), new GUIStyle()
                    {
                        normal = new GUIStyleState() { textColor = Color.cyan },
                        fontStyle = FontStyle.Italic,
                        alignment = TextAnchor.MiddleRight
                    });
                }
                else if (layerName.Contains("Interac"))
                {
                    EditorGUI.LabelField(selectionRect, LayerMask.LayerToName(go.layer), new GUIStyle()
                    {
                        normal = new GUIStyleState() { textColor = Color.green },
                        fontStyle = FontStyle.Italic,
                        alignment = TextAnchor.MiddleRight
                    });
                }
                else
                {
                    EditorGUI.LabelField(selectionRect, LayerMask.LayerToName(go.layer), new GUIStyle()
                    {
                        normal = new GUIStyleState() { textColor = Color.gray },
                        fontStyle = FontStyle.Italic,
                        alignment = TextAnchor.MiddleRight
                    });
                }
            }
        }

    }
}
