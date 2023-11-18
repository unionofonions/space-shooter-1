
namespace Clockwork.Editor
{
	using System.Linq;
	using UnityEngine;
	using UnityEditor;

	[CustomPropertyDrawer(typeof(WeightedSet<>))]
	public sealed class WeightedSetPropertyDrawer : PropertyDrawer
	{
		protected override void Update(Rect rect, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginChangeCheck();
			EditorGUI.PropertyField(rect, property, label, includeChildren: true);

			if (EditorGUI.EndChangeCheck())
			{
				var collectionProp = property.GetPropertySafe("m_Collection");
				var totalWeightProp = property.GetPropertySafe("m_TotalWeight");

				var coll = collectionProp.IterateElements()
					.ToArray();

				totalWeightProp.floatValue = coll.Sum(elem =>
				{
					return elem.GetPropertySafe("m_Weight").floatValue;
				});

				foreach (var elem in coll)
				{
					var weightProp = elem.GetPropertySafe("m_Weight");
					var chanceProp = elem.GetPropertySafe("m_Chance");

					chanceProp.floatValue = (weightProp.floatValue / totalWeightProp.floatValue) * 100f;
				}
			}
		}

	}
}