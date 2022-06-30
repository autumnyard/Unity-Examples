using System;
using System.Collections.Generic;
using UnityEngine;

// Source: https://gist.github.com/aarthificial/f2dbb58e4dbafd0a93713a380b9612af

namespace AutumnYard.Tools
{
    [Serializable]
    /// Requires Unity 2020.1+
    public struct Optional<T>
    {
        [SerializeField] private bool enabled;
        [SerializeField] private T value;

        public bool Enabled => enabled;
        public T Value => value;

        public Optional(T initialValue)
        {
            enabled = true;
            value = initialValue;
        }
    }
}

#if UNITY_EDITOR
namespace AutumnYard.Tools.Editor
{
    using UnityEditor;

    [CustomPropertyDrawer(typeof(Optional<>))]
    public class OptionalPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var valueProperty = property.FindPropertyRelative("value");
            return EditorGUI.GetPropertyHeight(valueProperty);
        }

        public override void OnGUI(
            Rect position,
            SerializedProperty property,
            GUIContent label
        )
        {
            var valueProperty = property.FindPropertyRelative("value");
            var enabledProperty = property.FindPropertyRelative("enabled");

            EditorGUI.BeginProperty(position, label, property);

            // Modo 1
            {
                //position.width -= 24;
                //EditorGUI.BeginDisabledGroup(!enabledProperty.boolValue);
                //EditorGUI.PropertyField(position, valueProperty, label, true);
                //EditorGUI.EndDisabledGroup();

                //int indent = EditorGUI.indentLevel;
                //EditorGUI.indentLevel = 0;
                //position.x += position.width + 24;
                //position.width = position.height = EditorGUI.GetPropertyHeight(enabledProperty);
                //position.x -= position.width;
                //EditorGUI.PropertyField(position, enabledProperty, GUIContent.none);
                //EditorGUI.indentLevel = indent;
            }

            // Modo 2
            {
                Rect initPos = position;
                position.width = position.height = EditorGUI.GetPropertyHeight(enabledProperty);
                EditorGUI.PropertyField(position, enabledProperty, GUIContent.none);

                position = initPos;
                position.width -= 24;
                position.x += 30;
                EditorGUI.BeginDisabledGroup(!enabledProperty.boolValue);
                EditorGUI.PropertyField(position, valueProperty, label, true);
                EditorGUI.EndDisabledGroup();
            }

            EditorGUI.EndProperty();
        }
    }
}
#endif