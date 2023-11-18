
namespace Clockwork.Editor
{
	using UnityEngine;
	using UnityEditor;
	using Clockwork.Runtime;

	[CustomPropertyDrawer(typeof(Curve3))]
	public sealed class Curve3PropertyDrawer : PropertyDrawer
	{
		protected override void Update(Rect rect, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginChangeCheck();
			base.Update(rect, property, label);

			if (EditorGUI.EndChangeCheck())
			{
				var amplitudeProp = property.GetPropertySafe("m_Amplitude");
				var xProp = GetAdapteeProp("m_X");
				var yProp = GetAdapteeProp("m_Y");
				var zProp = GetAdapteeProp("m_Z");
				var validProp = property.GetPropertySafe("m_Valid");

				var valid = 0;

				if (IsValid(xProp)) { valid |= 1; }
				if (IsValid(yProp)) { valid |= 2; }
				if (IsValid(zProp)) { valid |= 4; }

				if (amplitudeProp.floatValue != 0f && valid != 0)
				{
					valid |= 8;
				}

				validProp.intValue = valid;
			}

			static bool IsValid(SerializedProperty adapteeProp)
			{
				return adapteeProp.animationCurveValue.length > 0;
			}

			SerializedProperty GetAdapteeProp(string name)
			{
				return property.GetPropertySafe(name)
					.GetPropertySafe("m_Adaptee");
			}

		}

	}
}