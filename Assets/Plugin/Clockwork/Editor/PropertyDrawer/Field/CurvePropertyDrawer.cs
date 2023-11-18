
namespace Clockwork.Editor
{
	using UnityEngine;
	using UnityEditor;

	[CustomPropertyDrawer(typeof(Curve), useForChildren: true)]
	public class CurvePropertyDrawer : PropertyDrawer
	{
		protected override void Update(Rect rect, SerializedProperty property, GUIContent label)
		{
			var color = new Color(1.0f, 0.6f, 0.6f);
			var adapteeProp = property.GetPropertySafe("m_Adaptee");

			EditorGUI.CurveField(
				rect,
				adapteeProp,
				color,
				ranges: default(Rect),
				label);
		}

		protected override float GetHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUIUtility.singleLineHeight;
		}

	}
}