
namespace Clockwork
{
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using System.Diagnostics;
	using UnityEngine;

	[EditorHandled]
	public class RecordLib<T> : ScriptableObject, IReadOnlyList<T> where T : RecordInfo
	{
		[SerializeField]
		private T m_Default;
		[SerializeField]
		private T[] m_Collection;
		[SerializeField, ReadOnly]
		private int m_NextId;

		private T[] m_OrderedCollection;

		public T this[int index]
		{
			get
			{
				Diagnosis.ASSERT(
					(uint)index < (uint)Count,
					 "Argument 'index' is out of range");

				return index == 0
					? m_Default
					: m_Collection[index - 1];
			}
		}

		public int Count
		{
			get => m_Collection.Length + 1;
		}

		[Conditional("UNITY_EDITOR")]
		protected void OnValidate()
		{
			m_OrderedCollection = null;
		}

		public T GetInfo(int id)
		{
			if (id == 0)
			{
				return m_Default;
			}

			if (m_OrderedCollection == null)
			{
				m_OrderedCollection = m_Collection.OrderBy(elem => elem.Id)
					.ToArray();
			}

			var low = 0;
			var high = m_OrderedCollection.Length - 1;

			while (low <= high)
			{
				var mid = low + ((high - low) >> 1);
				var _id = m_OrderedCollection[mid].Id;

				if (_id < id)
				{
					low = mid + 1;
				}
				else if (_id > id)
				{
					high = mid - 1;
				}
				else
				{
					return m_OrderedCollection[mid];
				}
			}

			return RecordInfo.Invalid<T>.value;
		}

		public IEnumerator<T> GetEnumerator()
		{
			return new Enumerator(this);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		private struct Enumerator : IEnumerator<T>
		{
			private readonly RecordLib<T> m_Reference;
			private T m_Current;
			private int m_Index;

			public Enumerator(RecordLib<T> reference)
			{
				m_Reference = reference;
				m_Current = default;
				m_Index = -1;
			}

			public T Current
			{
				get => m_Current;
			}

			object IEnumerator.Current
			{
				get => Current;
			}

			public bool MoveNext()
			{
				
				if (++m_Index < m_Reference.Count)
				{
					m_Current = m_Reference[m_Index];
					return true;
				}
				else
				{
					m_Index = m_Reference.Count;
					m_Current = default;
					return false;
				}

			}

			public void Reset()
			{
				m_Current = default;
				m_Index = -1;
			}

			public void Dispose() { }

		}

	}
}