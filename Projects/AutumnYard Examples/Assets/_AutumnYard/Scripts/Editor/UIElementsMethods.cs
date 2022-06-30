using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements; // TODO: Some wrong dependency with UI Elements system, throws lots of errors

namespace AutumnYard.Editor
{
    public static class UIElementsMethods
    {
        //public static Label Header(string text, int left, int extraPaddingTop = 0)
        //{
        //    var label = new Label(text);
        //    label.style.flexDirection = FlexDirection.Row;
        //    label.style.backgroundColor = UnityEngine.Color.clear;
        //    label.style.marginLeft = left;
        //    label.style.paddingTop = 5;
        //    label.style.paddingBottom = 2;
        //    label.style.alignSelf = Align.Center;
        //    label.style.fontSize = 20f;
        //    label.style.unityFontStyleAndWeight = UnityEngine.FontStyle.Bold;
        //    label.style.color = UnityEngine.Color.white;
        //    return label;
        //}

        //public static Label Subheader(string text, int left)
        //{
        //    var label = new Label(text);
        //    label.style.marginLeft = left;
        //    label.style.paddingBottom = 10f;
        //    label.style.alignSelf = Align.Center;
        //    return label;
        //}

        //public static Label Label(string text, int left, int right, int width, TextAnchor anchor)
        //{
        //    var label = new Label();
        //    label.text = text;
        //    label.style.unityTextAlign = anchor;
        //    label.style.paddingLeft = left;
        //    label.style.paddingRight = right;
        //    label.style.width = width;
        //    return label;
        //}

        //public static Box Group(int left, params VisualElement[] elements)
        //{
        //    var group = new Box();
        //    group.style.flexDirection = FlexDirection.Row;
        //    group.style.marginLeft = left;
        //    group.style.backgroundColor = Color.clear;

        //    foreach (var element in elements)
        //    {
        //        group.Add(element);
        //    }

        //    return group;
        //}

        //public static Foldout Foldout(int left, params VisualElement[] elements)
        //{
        //    var foldout = new Foldout();
        //    foldout.style.flexDirection = FlexDirection.Column;
        //    foldout.style.marginLeft = left;
        //    foldout.style.backgroundColor = Color.clear;

        //    foreach (var element in elements)
        //    {
        //        foldout.Add(element);
        //    }

        //    return foldout;
        //}
    }
}
