using UnityEditor;
using UnityEngine;

namespace AutumnYard.Editor
{
    public static partial class EditorTools
    {
        // Sets the anchors of the RectTransform to it's corner bounds
        [MenuItem("Autumn Yard/UI/Anchors to Corners %[")]
        static void AnchorsToCorners()
        {
            RectTransform t = Selection.activeTransform as RectTransform;
            RectTransform pt = Selection.activeTransform.parent as RectTransform;

            if (t == null || pt == null)
                return;

            Vector2 newAnchorsMin = new Vector2(t.anchorMin.x + t.offsetMin.x / pt.rect.width,
                                                t.anchorMin.y + t.offsetMin.y / pt.rect.height);
            Vector2 newAnchorsMax = new Vector2(t.anchorMax.x + t.offsetMax.x / pt.rect.width,
                                                t.anchorMax.y + t.offsetMax.y / pt.rect.height);

            t.anchorMin = newAnchorsMin;
            t.anchorMax = newAnchorsMax;
            t.offsetMin = t.offsetMax = new Vector2(0, 0);
        }

        // Scale the bo9nds of the RestTransform to the anchors
        [MenuItem("Autumn Yard/UI/Corners to Anchors %]")]
        static void CornersToAnchors()
        {
            RectTransform t = Selection.activeTransform as RectTransform;

            if (t == null)
                return;

            t.offsetMin = t.offsetMax = new Vector2(0, 0);
        }
    }
}