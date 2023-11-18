
namespace Clockwork.Editor
{
	using UnityEngine;
	using UnityEditor;

	[CustomPropertyDrawer(typeof(NotDefaultAttribute))]
	public sealed class NotDefaultAttributePropertyDrawer : PropertyDrawer
	{
		protected override void Initialize(SerializedProperty property, GUIContent label)
		{
			var valid = property.propertyType is
				SerializedPropertyType.String or
				SerializedPropertyType.ObjectReference;

			if (!valid)
			{
				Error = Error.revert;
			}
		}

		protected override void Update(Rect rect, SerializedProperty property, GUIContent label)
		{
			var @default = property.propertyType switch
			{
				SerializedPropertyType.String => property.stringValue.Length == default,
				SerializedPropertyType.ObjectReference => property.objectReferenceValue == default,
				_ => throw new EnumIndexException(property.propertyType)
			};

			if (@default)
			{
				ext.EditorGUI.BeginErrorArea();
			}

			EditorGUI.PropertyField(rect, property, label, includeChildren: true);

			if (@default)
			{
				ext.EditorGUI.EndErrorArea();
			}
		}

	}
}