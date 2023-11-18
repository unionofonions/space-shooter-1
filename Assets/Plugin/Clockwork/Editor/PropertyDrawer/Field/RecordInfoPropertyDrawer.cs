
namespace Clockwork.Editor
{
	using UnityEngine;
	using UnityEditor;

	[CustomPropertyDrawer(typeof(RecordInfo), useForChildren: true)]
	public class RecordInfoPropertyDrawer : PropertyDrawer
	{
		protected override void Update(Rect rect, SerializedProperty property, GUIContent label)
		{
			base.Update(rect, property, label);

			var validProp = property.GetPropertySafe("m_Valid");
			validProp.boolValue = IsValid(property);
		}

		protected virtual bool IsValid(SerializedProperty property)
		{
			return true;
		}

	}
}