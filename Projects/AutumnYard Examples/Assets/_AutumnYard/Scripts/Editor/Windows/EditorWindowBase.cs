using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;

namespace AutumnYard.Editor
{
    public abstract class EditorWindowBase : EditorWindow
	{
		private GUIStyle horizontalLine;

		private void Awake()
		{
			horizontalLine = new GUIStyle();
			horizontalLine.normal.background = EditorGUIUtility.whiteTexture;
			horizontalLine.margin = new RectOffset(0, 0, 4, 4);
			horizontalLine.fixedHeight = 1;
		}

		protected void Header(string text)
		{
			GUILayout.Space(10f);
			GUILayout.Label(text, EditorStyles.boldLabel);
			HorizontalLine();
			GUILayout.Space(2f);
		}

		protected void HorizontalLine()
		{
			Color color = Color.gray;
			int thickness = 1;
			int padding = 0;
			int margins = 2;
			Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
			r.height = thickness;
			r.y += padding / 2;
			r.x += 0;
			r.width -= margins;
			EditorGUI.DrawRect(r, color);
		}


		protected void Label(string text, string tooltip = "")
		{
			EditorGUILayout.LabelField(text);
		}

		protected void Int(ref int data, string text, string tooltip = "")
		{
			data = EditorGUILayout.IntField(new GUIContent(text, tooltip), data);
		}

		protected void String(ref string data, string text, string tooltip = "")
		{
			data = EditorGUILayout.TextField(new GUIContent(text, tooltip), data);
		}

		protected void Bool(ref bool data, string text, string tooltip = "")
		{
			data = EditorGUILayout.Toggle(new GUIContent(text, tooltip), data);
		}
		protected void Enum<T>(ref T data, string text, string tooltip = "") where T : Enum
		{
			data = (T)EditorGUILayout.EnumPopup(new GUIContent(text, tooltip), data);
		}

		protected void Button(Action method, string text, int height = -1, int width = -1)
		{
			if (height.Equals(-1) && width.Equals(-1))
			{
				if (GUILayout.Button(text)) method();
			}
			else if (height.Equals(-1) && !width.Equals(-1))
			{
				if (GUILayout.Button(text, GUILayout.Width(width))) method();
			}
			else if (!height.Equals(-1) && width.Equals(-1))
			{
				if (GUILayout.Button(text, GUILayout.Height(height))) method();
			}
			else
			{
				if (GUILayout.Button(text, GUILayout.Width(width), GUILayout.Height(height))) method();
			}
		}
	}
}
