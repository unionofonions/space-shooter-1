
namespace Clockwork.Editor
{
	using System.Linq;
	using UnityEditor;

	[CustomEditor(typeof(RecordLib<>), editorForChildClasses: true)]
	public class RecordLibEditor : Editor
	{
		private SerializedProperty m_CollectionProp;
		private SerializedProperty m_NextIdProp;

		protected void OnEnable()
		{
			m_CollectionProp = serializedObject.GetPropertySafe("m_Collection");
			m_NextIdProp = serializedObject.GetPropertySafe("m_NextId");
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			var reg = m_CollectionProp.arraySize;

			base.OnInspectorGUI();

			if (reg != m_CollectionProp.arraySize)
			{
				UpdateIds();
			}

			serializedObject.ApplyModifiedProperties();
		}

		private void UpdateIds()
		{
			var ids = m_CollectionProp.IterateElements()
				.Reverse()
				.Select(elem => elem.GetPropertySafe("m_Id"))
				.ToArray();

			foreach (var id in ids)
			{
				if (!IsValid(id.intValue))
				{
					id.intValue = ++m_NextIdProp.intValue;
				}
			}

			bool IsValid(int id)
			{
				if (id == 0)
				{
					return false;
				}

				var flag = false;

				foreach (var _id in ids)
				{
					if (id == _id.intValue)
					{
						if (flag)
						{
							return false;
						}
						else
						{
							flag = true;
						}
					}
				}

				return true;
			}

		}

	}
}