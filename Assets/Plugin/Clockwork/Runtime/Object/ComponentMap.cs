#nullable enable

namespace Clockwork.Runtime
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public abstract class ComponentMap<T> : IReadOnlyCollection<IComponentPool<T>> where T : Component
	{
		private const string c_ContainerName = "[MAP:{0}]";

		private readonly List<IComponentPool<T>> m_Collection;
		private readonly GameObject m_Container;

		private IComponentPool<T>? m_Cache;

		protected ComponentMap()
		{
			m_Collection = new();

			m_Container = Hierarchy.CreateGameObject(
				name: String.Format(c_ContainerName, typeof(T).Name),
				permanent: true);
		}

		public int Count
		{
			get => m_Collection.Count;
		}

		static private int Compare(IComponentPool<T> pool, T scheme)
		{
			return pool.Scheme
				.GetInstanceID()
				.CompareTo(scheme.GetInstanceID());
		}

		public T Provide(T scheme)
		{
			Diagnosis.ASSERT(
				scheme != null,
				"Argument 'scheme' cannot be null");

			if (m_Cache == null || Compare(m_Cache, scheme) != 0)
			{
				var index = IndexOf(scheme);
				if (index >= 0)
				{
					m_Cache = m_Collection[index];
				}
				else
				{
					m_Cache = CreateInternal(scheme);
					m_Collection.Insert(~index, m_Cache);
				}
			}

			return m_Cache.Provide();
		}

		public void DeactivateAll()
		{
			foreach (var pool in m_Collection)
			{
				foreach (var elem in pool)
				{
					elem.gameObject.SetActive(false);
				}
			}
		}

		public IEnumerator<IComponentPool<T>> GetEnumerator()
		{
			return ((IEnumerable<IComponentPool<T>>)m_Collection).GetEnumerator();
		}

		protected abstract IComponentPool<T> Create(T scheme);

		private IComponentPool<T> CreateInternal(T scheme)
		{
			var ret = Create(scheme);

			Diagnosis.ASSERT(
				ret != null,
				"ComponentMap.Create() cannot return null");

			Diagnosis.ASSERT(
				ret.Scheme != null,
				"ComponentMap.Create().Scheme cannot be null");

			if (ret.Container != null)
			{
				ret.Container.transform.SetParent(
					m_Container.transform,
					worldPositionStays: false);
			}

			return ret;
		}

		private int IndexOf(T scheme)
		{
			var low = 0;
			var high = m_Collection.Count - 1;

			while (low <= high)
			{
				var mid = low + ((high - low) >> 1);
				var comp = Compare(m_Collection[mid], scheme);

				if (comp < 0)
				{
					low = mid + 1;
				}
				else if (comp > 0)
				{
					high = mid - 1;
				}
				else
				{
					return mid;
				}
			}

			return ~low;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

	}
}