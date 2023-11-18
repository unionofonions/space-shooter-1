
namespace Clockwork.Editor
{
	using UnityEngine;
	using UnityEditor;

	public abstract class PropertyDrawer : UnityEditor.PropertyDrawer
	{
		private bool m_Initialized;
		private Error m_Error;

		protected virtual Error Error
		{
			get => m_Error;
			set => m_Error = value;
		}

		public sealed override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
		{
			InitializeInternal(property, label);

			if (Error != null)
			{
				if (Error == Error.revert)
				{
					ext.EditorGUI.DrawPropertyField(rect, property, label);
				}
				else
				{
					ext.EditorGUI.DrawErrorLabel(rect, Error.ToString());
				}
			}
			else
			{
				Update(rect, property, label);
			}
		}

		public sealed override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			InitializeInternal(property, label);

			if (Error != null)
			{
				return Error == Error.revert
					? EditorGUI.GetPropertyHeight(property, label)
					: EditorGUIUtility.singleLineHeight;
			}
			else
			{
				return GetHeight(property, label);
			}
		}

		protected virtual void Update(Rect rect, SerializedProperty property, GUIContent label)
		{
			ext.EditorGUI.DrawPropertyField(rect, property, label);
		}

		protected virtual float GetHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUI.GetPropertyHeight(property, label);
		}

		protected virtual void Initialize(SerializedProperty property, GUIContent label) { }

		private void InitializeInternal(SerializedProperty property, GUIContent label)
		{
			if (!m_Initialized)
			{
				Initialize(property, label);
				m_Initialized = true;
			}
		}

	}
}