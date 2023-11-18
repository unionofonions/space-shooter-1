
namespace Clockwork.Editor
{
	using UnityEngine;
	using UnityEditor;

	[CustomPropertyDrawer(typeof(Set<>))]
	public sealed class SetPropertyDrawer : PropertyDrawer
	{
		private const float c_ThresholdSizeRatio = 0.5f;

		protected override void Update(Rect rect, SerializedProperty property, GUIContent label)
		{
			var collectionProp = property.GetPropertySafe("m_Collection");

			EditorGUI.BeginChangeCheck();

			if (collectionProp.arraySize > 1)
			{
				EditorGUI.PropertyField(
					rect,
					property,
					label,
					includeChildren: true);
			}
			else
			{
				EditorGUI.PropertyField(
					rect,
					collectionProp,
					label,
					includeChildren: true);
			}

			if (EditorGUI.EndChangeCheck())
			{
				var thresholdProp = property.GetPropertySafe("m_Threshold");
				thresholdProp.intValue = (int)(collectionProp.arraySize * c_ThresholdSizeRatio);
			}
		}

		protected override float GetHeight(SerializedProperty property, GUIContent label)
		{
			var collectionProp = property.GetPropertySafe("m_Collection");

			return collectionProp.arraySize > 1
				? EditorGUI.GetPropertyHeight(property, label)
				: EditorGUI.GetPropertyHeight(collectionProp, label);
		}


	}
}