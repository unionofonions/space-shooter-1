#nullable enable

using __buff = UnityEditor.EditorGUI;

namespace Clockwork.ext
{
	using System;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;
	using UnityEngine;
	using UnityEditor;
	using Clockwork.Editor;

	static public class EditorGUI
	{
		static private Layout s_CurrentLayout;
		static private Rect s_LayoutRect;

		static private readonly Stack<Color> s_ColorStack;

		static EditorGUI()
		{
			s_ColorStack = new();
		}

		static public void BeginVerticalLayout(Rect rect)
		{
			s_CurrentLayout = Layout.Vertical;
			s_LayoutRect = rect;
		}

		static public void EndVerticalLayout()
		{
			s_CurrentLayout = Layout.__none;
		}

		static public void BeginColorArea(Color color)
		{
			s_ColorStack.Push(GUI.backgroundColor);
			GUI.backgroundColor = color;
		}

		static public void EndColorArea()
		{
			if (s_ColorStack.TryPop(out var color))
			{
				GUI.backgroundColor = color;
			}
			else
			{
				Diagnosis.LOGW("Poor use of EditorGUI.EndColorArea()");
			}
		}

		static public void BeginErrorArea()
		{
			var color = new Color(1f, 0.5f, 0.5f);
			BeginColorArea(color);
		}

		static public void EndErrorArea()
		{
			EndColorArea();
		}

		static public void DrawLabel(
			Rect rect,
			Label content,
			[Optional] Label title)
		{
			var _rect = BeginDrawing(rect);

			__buff.LabelField(
				_rect,
				title.ContentOrEmpty,
				content.ContentOrEmpty,
				EditorStyles.toolbarButton);

			EndDrawing();
		}

		static public void DrawErrorLabel(
			Rect rect,
			Label content,
			[Optional] Label title)
		{
			BeginErrorArea();
			DrawLabel(rect, content, title);
			EndErrorArea();
		}

		static public void DrawPropertyField(
			Rect rect,
			SerializedProperty property,
			[Optional] Label label,
			bool includeBase = true)
		{
			if (property == null)
				throw new ArgumentNullException(nameof(property));

			var _rect = BeginDrawing(
				rect,
				property,
				includeBase);

			__buff.PropertyField(
				_rect,
				property,
				label,
				includeBase);

			EndDrawing();
		}

		static public bool DrawPropertyFoldout(
			Rect rect,
			SerializedProperty property,
			[Optional] Label label)
		{
			if (property == null)
				throw new ArgumentNullException(nameof(property));

			var _rect = BeginDrawing(rect);

			var ret = property.isExpanded = __buff.Foldout(
				_rect,
				property.isExpanded,
				label.Content ?? new Label(property.displayName),
				toggleOnLabelClick: true);

			EndDrawing();

			return ret;
		}

		static public int DrawPopup(
			Rect rect,
			GUIContent[] labels,
			int selectedIndex,
			[Optional] Label label)
		{
			if (labels == null)
				throw new ArgumentNullException(nameof(labels));

			var _rect = BeginDrawing(rect);

			var ret = __buff.Popup(
				_rect,
				label.ContentOrEmpty,
				selectedIndex,
				labels);

			EndDrawing();

			return ret;
		}

		static private Rect BeginDrawing(
			Rect rect,
			SerializedProperty property,
			bool includeBase)
		{
			switch (s_CurrentLayout)
			{
				case Layout.__none:
					return rect;

				case Layout.Vertical:
					s_LayoutRect.height = __buff.GetPropertyHeight(
						property,
						includeBase);

					return s_LayoutRect;

				default:
					throw new EnumIndexException(s_CurrentLayout);
			}

		}

		static private Rect BeginDrawing(Rect rect)
		{
			switch (s_CurrentLayout)
			{
				case Layout.__none:
					return rect;

				case Layout.Vertical:
					s_LayoutRect.height = EditorGUIUtility.singleLineHeight;
					return s_LayoutRect;

				default:
					throw new EnumIndexException(s_CurrentLayout);
			}
		}

		static private void EndDrawing()
		{
			switch (s_CurrentLayout)
			{
				case Layout.__none:
					return;

				case Layout.Vertical:
					s_LayoutRect.y += s_LayoutRect.height;
					return;

				default:
					throw new EnumIndexException(s_CurrentLayout);
			}
		}

		private enum Layout
		{
			__none,
			Vertical
		}

	}
}