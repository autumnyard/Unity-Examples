using System;
using UnityEngine;

namespace AutumnYard.Core.DeveloperTools
{
    public static class GUITools
    {
        public const int MarginLeft = 10;
        public const int MarginTop = 30;
        public const int SizeX = 140;
        public const int SizeY = 27;
        public const int RectWidth = 400;
        public const int RectHeight = 400;

        public readonly static Rect RectTopLeft = new Rect(0, 0, RectWidth, RectHeight);
        public readonly static Rect RectTopRight = new Rect(Screen.width - RectWidth, 0, RectWidth, RectHeight);
        public readonly static Rect RectBottomLeft = new Rect(0, Screen.height - RectHeight, RectWidth, RectHeight);
        public readonly static Rect RectBottomRight = new Rect(Screen.width - RectWidth, Screen.height - RectHeight, RectWidth, RectHeight);


        public static Rect CalculateRect(int indexY, int indexX)
        {
            return new Rect(MarginLeft + indexX * SizeX, MarginTop + indexY * SizeY, SizeX, SizeY);
        }


        public static void Button(Rect rect, string name, Action action)
        {
            if (GUI.Button(rect, name))
                action();
        }

        public static void Button(string name, Action action)
        {
            if (GUILayout.Button(name))
                action();
        }

        public static void String(Rect rect, string text)
        {
            GUI.Label(rect, text);
        }


        public static void Foldout(ref bool show, in int index, in (string name, Action action)[] commands)
        {
            int i = 0;

            show = GUI.Toggle(CalculateRect(i, index), show, "Contexts");

            if (!show)
                return;

            foreach (var command in commands)
            {
                i++;
                Button(CalculateRect(i, index), command.name, command.action);
            }
        }


        public static int Enum(Rect rect, int currentValue, string[] values)
        {
            return GUI.Toolbar(rect, currentValue, values);
        }
        public static int Enum(int currentValue, string[] values)
        {
            return GUILayout.Toolbar(currentValue, values);
        }

    }
}
