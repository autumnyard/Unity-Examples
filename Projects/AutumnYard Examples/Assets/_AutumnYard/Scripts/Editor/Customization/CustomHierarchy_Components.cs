using UnityEditor;
using UnityEngine;
//using AutumnYard.Game.Interactables;

namespace AutumnYard.Editor
{

    [InitializeOnLoad]
    public static class CustomHierarchy_Components
    {
        static CustomHierarchy_Components()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
        }

        private static void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            //if (!GlobalSettings.Instance.CheckHierarchy(GlobalSettings.CustomHierarchy.Components))
            //    return;

            var go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

            if (go == null) return;


            //if (go.GetComponent<Interactable>() != null)
            //{
            //    EditorGUI.LabelField(selectionRect, "Interactable", new GUIStyle()
            //    {
            //        normal = new GUIStyleState() { textColor = Color.green },
            //        fontStyle = FontStyle.Normal,
            //        alignment = TextAnchor.MiddleRight
            //    });
            //}

            //if (go.GetComponent<Game.DirectorGame>() != null
            //    || go.GetComponent<Game.DirectorMainMenu>() != null
            //    || go.GetComponent<Game.DirectorCredits>() != null
            //    || go.GetComponent<Game.DirectorSaveEditor>() != null)
            //{
            //    EditorGUI.LabelField(selectionRect, "Director", new GUIStyle()
            //    {
            //        normal = new GUIStyleState() { textColor = Color.yellow },
            //        fontStyle = FontStyle.Normal,
            //        alignment = TextAnchor.MiddleRight
            //    });
            //}

            //if (go.GetComponent<SingleInstance<UI>>() != null)
            //{
            //    EditorGUI.LabelField(selectionRect, "SingleInstance", new GUIStyle()
            //    {
            //        normal = new GUIStyleState() { textColor = Color.green },
            //        fontStyle = FontStyle.Normal,
            //        alignment = TextAnchor.MiddleRight
            //    });
            //}

            //if (go.GetComponent<SingleInstance<Game.Player.PlayerActor>>() != null)
            //{
            //    EditorGUI.LabelField(selectionRect, "SingleInstance", new GUIStyle()
            //    {
            //        normal = new GUIStyleState() { textColor = Color.green },
            //        fontStyle = FontStyle.Normal,
            //        alignment = TextAnchor.MiddleRight
            //    });
            //}
        }
    }
}
