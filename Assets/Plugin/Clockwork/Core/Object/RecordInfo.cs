
namespace Clockwork
{
	using System.Runtime.Serialization;
	using UnityEngine;
	
	[EditorHandled]
	public class RecordInfo
	{
		[SerializeField, NotDefault]
		private string m_Name;
		[SerializeField, ReadOnly]
		private int m_Id;
		[SerializeField, ReadOnly]
		private bool m_Valid;

		public string Name
		{
			get => m_Name;
		}

		public int Id
		{
			get => m_Id;
		}

		public bool Valid
		{
			get => m_Valid;
		}

		public override int GetHashCode()
		{
			return m_Id;
		}

		public override string ToString()
		{
			return m_Name;
		}

		static internal class Invalid<T> where T : RecordInfo
		{
			static public readonly T value;

			static Invalid()
			{
				value = (T)FormatterServices.GetUninitializedObject(typeof(T));
				{
					value.m_Name = "__invalid";
					value.m_Id = -1;
					value.m_Valid = false;
				}
			}

		}

	}
}