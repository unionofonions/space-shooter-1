#nullable enable

namespace Clockwork.Editor
{
	using System;
	using System.Collections.Generic;
	using UnityEditor;

	static public class UnityExtension
	{
		static public Type GetPropertyFieldType(this UnityEditor.PropertyDrawer propertyDrawer)
		{
			if (propertyDrawer == null)
				throw new ArgumentNullException(nameof(propertyDrawer));

			var raw = propertyDrawer.fieldInfo.FieldType;
			return raw.IsArray ? raw.GetElementType() : raw;
		}

		static public IEnumerable<SerializedProperty> IterateElements(this SerializedProperty serializedProperty)
		{
			if (serializedProperty == null)
				throw new ArgumentNullException(nameof(serializedProperty));

			if (!serializedProperty.isArray)
			{
				throw new ArgumentException(
					$"Argument 'serializeProperty' must be an array",
					nameof(serializedProperty));
			}

			for (var i = 0; i < serializedProperty.arraySize; ++i)
			{
				yield return serializedProperty.GetArrayElementAtIndex(i);
			}
		}

		static public SerializedProperty GetPropertySafe(this SerializedProperty serializedProperty, string name)
		{
			if (serializedProperty == null)
				throw new ArgumentNullException(nameof(serializedProperty));

			var ret = serializedProperty.FindPropertyRelative(name);

			if (ret == null)
			{
				throw new ArgumentException(
					$"Argument 'serializedProperty' does not contain such named property. serializedProperty: {serializedProperty}, name: {name}",
					nameof(name));
			}

			return ret;
		}

		static public SerializedProperty GetPropertySafe(this SerializedObject serializedObject, string name)
		{
			if (serializedObject == null)
				throw new ArgumentNullException(nameof(serializedObject));

			var ret = serializedObject.FindProperty(name);

			if (ret == null)
			{
				throw new ArgumentException(
					$"Argument 'serializedObject' does not contain such named property. serializedObject: {serializedObject}, name: {name}",
					nameof(name));
			}

			return ret;
		}

	}
}