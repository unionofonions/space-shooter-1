
namespace Clockwork
{
	using System;
	using UnityEngine;

	[Serializable, EditorHandled]
	public class RecordRef<T> : IEquatable<RecordRef<T>> where T : RecordInfo
	{
		[SerializeField, HideInInspector]
		private RecordLib<T> m_Library;
		[SerializeField, HideInInspector]
		private int m_Id;

		[NonSerialized]
		private T m_Info;

		public T Info
		{
			get
			{
				return m_Info ??= m_Library != null
					? m_Library.GetInfo(m_Id)
					: RecordInfo.Invalid<T>.value;
			}
		}

		public bool GetInfo(out T info)
		{
			return (info = Info).Valid;
		}

		public bool Equals(RecordRef<T> other)
		{
			return other != null && other.Info == Info;
		}

		public override int GetHashCode()
		{
			return Info.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as RecordRef<T>);
		}

		public override string ToString()
		{
			return Info.ToString();
		}

	}
}