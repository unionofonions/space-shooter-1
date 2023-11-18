
namespace Clockwork.Editor
{
	using System.Collections.Generic;
	using System.Linq;
	using UnityEngine;
	using UnityEditor;

	using Object = UnityEngine.Object;

	[CustomPropertyDrawer(typeof(RecordRef<>), useForChildren: true)]
	public class RecordRefPropertyDrawer : PropertyDrawer
	{
		private GUIContent[] m_LibraryLabels;
		private Object[] m_LibraryValues;
		private Dictionary<Object, LibraryContent> m_LibraryContentMap;

		protected override void Initialize(SerializedProperty property, GUIContent label)
		{
			var infoType = this.GetPropertyFieldType().
				IterateHierarchy()
				.ToArray()[^2]
				.GenericTypeArguments[0];

			RecordDb.Clear(infoType);

			var result = RecordDb.GetLibraries(infoType, errorIfEmpty: true);
			if (result.Success)
			{
				m_LibraryValues = result.Value
					.Cast<Object>()
					.ToArray();
			}
			else
			{
				Error = result.Error;
				return;
			}

			// Replacing '.' with '/' allows Unity to group labels
			m_LibraryLabels = m_LibraryValues
				.Select(elem => new GUIContent(elem.name.Replace('.', '/')))
				.ToArray();

			m_LibraryContentMap = new();
		}

		protected override void Update(Rect rect, SerializedProperty property, GUIContent label)
		{
			var idProp = property.GetPropertySafe("m_Id");

			ext.EditorGUI.BeginVerticalLayout(rect);

			if (ext.EditorGUI.DrawPropertyFoldout(rect, idProp, label))
			{
				EditorGUI.indentLevel++;

				var libraryProp = property.GetPropertySafe("m_Library");

				var libraryIndex = ext.EditorGUI.DrawPopup(
					rect,
					m_LibraryLabels,
					GetLibraryIndex(libraryProp),
					label: "Library");

				SetLibraryIndex(libraryProp, libraryIndex);

				var libraryContent = GetLibraryContent(libraryProp);

				var infoIndex = ext.EditorGUI.DrawPopup(
					rect,
					libraryContent.labels,
					GetInfoIndex(idProp, libraryContent),
					label: "Info");

				SetInfoIndex(idProp, libraryContent, infoIndex);

				if (property.hasVisibleChildren)
				{
					ext.EditorGUI.DrawPropertyField(
						rect,
						property,
						label: "Params");
				}

				EditorGUI.indentLevel--;
			}

			ext.EditorGUI.EndVerticalLayout();
		}

		protected override float GetHeight(SerializedProperty property, GUIContent label)
		{
			var ret = EditorGUIUtility.singleLineHeight;

			var idProp = property.GetPropertySafe("m_Id");

			if (idProp.isExpanded)
			{
				ret += EditorGUIUtility.singleLineHeight * 2f;

				if (property.hasVisibleChildren)
				{
					ret += EditorGUI.GetPropertyHeight(property, label);
				}
			}

			return ret;
		}

		private int GetLibraryIndex(SerializedProperty libraryProp)
		{
			var value = libraryProp.objectReferenceValue;
			for (var i = m_LibraryValues.Length; --i >= 0;)
			{
				if (value == m_LibraryValues[i])
				{
					return i;
				}
			}

			return 0;
		}

		private void SetLibraryIndex(SerializedProperty libraryProp, int index)
		{
			libraryProp.objectReferenceValue = m_LibraryValues[index];
		}

		private LibraryContent GetLibraryContent(SerializedProperty libraryProp)
		{
			var value = libraryProp.objectReferenceValue;
			if (!m_LibraryContentMap.TryGetValue(value, out var ret))
			{
				ret = new(value);
				m_LibraryContentMap.Add(value, ret);
			}

			return ret;
		}

		private int GetInfoIndex(SerializedProperty infoProp, LibraryContent libraryContent)
		{
			var value = infoProp.intValue;
			var values = libraryContent.values;
			for (var i = values.Length; --i >= 0;)
			{
				if (value == values[i].Id)
				{
					return i;
				}
			}

			return 0;
		}

		private void SetInfoIndex(SerializedProperty infoProp, LibraryContent libraryContent, int index)
		{
			infoProp.intValue = libraryContent.values[index].Id;
		}

		private class LibraryContent
		{
			public readonly GUIContent[] labels;
			public readonly RecordInfo[] values;

			public LibraryContent(Object library)
			{
				values = ((IEnumerable<RecordInfo>)library).ToArray();
				labels = values.Select(elem => new GUIContent(elem.Name)).ToArray();
			}

		}

	}
}