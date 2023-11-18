
namespace Clockwork.Editor
{
	using UnityEngine;
	using UnityEditor;

	[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
	public sealed class ReadOnlyAttributePropertyDrawer : PropertyDrawer
	{
		protected override void Update(Rect rect, SerializedProperty property, GUIContent label)
		{
			var reg = GUI.enabled;
			GUI.enabled = false;
			EditorGUI.PropertyField(rect, property, label, includeChildren: true);
			GUI.enabled = reg;
		}

	}
}