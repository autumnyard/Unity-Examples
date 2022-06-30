using UnityEditor;
using UnityEngine;

namespace AutumnYard.Editor
{
    public static partial class EditorTools
    {
        [MenuItem("Autumn Yard/Clear console %t", priority = 60)]
        private static void ClearConsole()
        {
            // This simply does "LogEntries.Clear()" the long way:
            //var logEntries = System.Type.GetType("UnityEditorInternal.LogEntries,UnityEditor.dll");
            var logEntries = System.Type.GetType("UnityEditor.LogEntries, UnityEditor.dll");
            var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            clearMethod.Invoke(null, null);
        }
    }
}
