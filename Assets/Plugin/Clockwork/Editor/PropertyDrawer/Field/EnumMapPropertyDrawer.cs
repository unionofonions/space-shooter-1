
namespace Clockwork.Editor
{
	using System;
	using System.Linq;
	using UnityEngine;
	using UnityEditor;

	[CustomPropertyDrawer(typeof(EnumMap<,>), useForChildren: true)]
	public class EnumMapPropertyDrawer : PropertyDrawer
	{
		private GUIContent[] m_IndexLabels;

		protected override void Initialize(SerializedProperty property, GUIContent label)
		{
			var enumType = this.GetPropertyFieldType()
				.GenericTypeArguments[0];

			m_IndexLabels = Enum.GetNames(enumType)
				.Select(name => new GUIContent(name))
				.ToArray();
		}

		protected override void Update(Rect rect, SerializedProperty property, GUIContent label)
		{
			var valuesProp = property.GetPropertySafe("m_Values");

			ValidateValuesArraySize(valuesProp);

			ext.EditorGUI.BeginVerticalLayout(rect);

			if (ext.EditorGUI.DrawPropertyFoldout(rect, property, label))
			{
				EditorGUI.indentLevel++;

				for (var i = 0; i < m_IndexLabels.Length; ++i)
				{
					ext.EditorGUI.DrawPropertyField(
						rect,
						valuesProp.GetArrayElementAtIndex(i),
						m_IndexLabels[i]);
				}

				EditorGUI.indentLevel--;
			}

			ext.EditorGUI.EndVerticalLayout();
		}

		protected override float GetHeight(SerializedProperty property, GUIContent label)
		{
			var valuesProp = property.GetPropertySafe("m_Values");

			ValidateValuesArraySize(valuesProp);

			var ret = EditorGUIUtility.singleLineHeight;

			if (property.isExpanded)
			{
				foreach (var elem in valuesProp.IterateElements())
				{
					ret += EditorGUI.GetPropertyHeight(elem);
				}
			}

			return ret;
		}

		private void ValidateValuesArraySize(SerializedProperty valuesProp)
		{
			if (valuesProp.arraySize != m_IndexLabels.Length)
			{
				valuesProp.arraySize = m_IndexLabels.Length;
			}
		}

	}
}