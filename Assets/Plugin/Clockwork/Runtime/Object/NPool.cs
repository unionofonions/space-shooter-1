#nullable enable

namespace Clockwork.Runtime
{
	using System;
	using System.Collections;
	using System.Collections.Generic;

	public abstract class NPool<T> : IPool<T>, IReadOnlyList<T>
	{
		private readonly List<T> m_Collection;
		private readonly Func<T, bool> m_GetActive;

		protected NPool(Func<T, bool> getActive)
		{
			Diagnosis.ASSERT(
				getActive != null,
				"Argument 'getActive' cannot be null");

			m_Collection = new();
			m_GetActive = getActive;
		}

		public T this[int index]
		{
			get => m_Collection[index];
		}

		public int Count
		{
			get => m_Collection.Count;
		}

		public T Provide()
		{
			int index;

			for (var i = m_Collection.Count; --i >= 0;)
			{
				if (!m_GetActive(m_Collection[i]))
				{
					index = i;
					goto SWAP;
				}
			}

			CreateInternal();
			index = m_Collection.Count - 1;

		SWAP:
			var ret = m_Collection[index];

			if (index != 0)
			{
				m_Collection[index] = m_Collection[0];
				m_Collection[0] = ret;
			}

			return ret;
		}

		public IEnumerator<T> GetEnumerator()
		{
			return m_Collection.GetEnumerator();
		}

		protected abstract T Create();

		private void CreateInternal()
		{
			var reg = Create();

			Diagnosis.ASSERT(
				reg != null,
				"NPool.Create() cannot return null");

			m_Collection.Add(reg);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

	}
}