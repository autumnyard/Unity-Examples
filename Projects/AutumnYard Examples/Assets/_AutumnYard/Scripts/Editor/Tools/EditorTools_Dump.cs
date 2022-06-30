using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AutumnYard.Editor
{
    public static partial class EditorTools
    {
        private static EditorWindow _mouseOverWindow;

        //[MenuItem("Autumn Yard/Select all objects with component MaterialSwapper %#m", priority = 60)]
        //private static void SelectObjectsComponentMaterialSwapper()
        //{
        //    UnityEngine.Object[] go = GameObject.FindObjectsOfType(typeof(Graphics.MaterialSwapper));
        //    Selection.objects = go;
        //}

        //[MenuItem("Autumn Yard/Toggle window lock %q", priority = 60)]
        private static void ToggleInspectorLock()
        {
            if (_mouseOverWindow == null)
            {
                if (!EditorPrefs.HasKey("LockableInspectorIndex"))
                    EditorPrefs.SetInt("LockableInspectorIndex", 0);
                int i = EditorPrefs.GetInt("LockableInspectorIndex");

                Type type = Assembly.GetAssembly(typeof(UnityEditor.Editor)).GetType("UnityEditor.InspectorWindow");
                UnityEngine.Object[] findObjectsOfTypeAll = Resources.FindObjectsOfTypeAll(type);
                _mouseOverWindow = (EditorWindow)findObjectsOfTypeAll[i];
            }

            if (_mouseOverWindow != null && _mouseOverWindow.GetType().Name == "InspectorWindow")
            {
                Type type = Assembly.GetAssembly(typeof(UnityEditor.Editor)).GetType("UnityEditor.InspectorWindow");
                PropertyInfo propertyInfo = type.GetProperty("isLocked");
                bool value = (bool)propertyInfo.GetValue(_mouseOverWindow, null);
                propertyInfo.SetValue(_mouseOverWindow, !value, null);
                _mouseOverWindow.Repaint();
            }
        }

        //[MenuItem("Autumn Yard/Reset scene camera", priority = 60)]
        private static void ResetSceneViewCamera()
        {
            SceneView.lastActiveSceneView.camera.transform.position = Vector3.zero;
            SceneView.lastActiveSceneView.camera.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        }


        //[MenuItem("Autumn Yard/Graphics/Turn Cast shadows OFF recursively", priority = 60)]
        private static void TurnShadowsRecursively_Off()
        {
            if (Selection.transforms == null)
                return;

            foreach (var go in Selection.gameObjects)
            {
                var renderers = go.GetComponentsInChildren<MeshRenderer>(true);
                foreach (var renderer in renderers)
                {
                    renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                }
            }
        }

        //[MenuItem("Autumn Yard/Graphics/Turn Cast shadows ON recursively", priority = 60)]
        private static void TurnShadowsRecursively_On()
        {
            if (Selection.transforms == null)
                return;

            foreach (var go in Selection.gameObjects)
            {
                var renderers = go.GetComponentsInChildren<MeshRenderer>(true);
                foreach (var renderer in renderers)
                {
                    renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                }
            }
        }
    }
}
