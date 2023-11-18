
namespace Clockwork
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	[Serializable, EditorHandled]
	public sealed class WeightedSet<T> : IReadOnlyCollection<WeightedSet<T>.Item>
	{
		[SerializeField]
		private Item[] m_Collection;
		[SerializeField, ReadOnly]
		private float m_TotalWeight;

		public int Count
		{
			get => m_Collection.Length;
		}

		public T Provide()
		{
			switch (m_Collection.Length)
			{
				case 0:
					return default(T);

				case 1:
					return m_Collection[0].Value;

				default:
					break;
			}

			var random = RandomNumberGenerator.GetSingle(0f, m_TotalWeight);
			var acc = 0f;

			foreach (var elem in m_Collection)
			{
				acc += elem.Weight;

				if (random < acc)
				{
					return elem.Value;
				}
			}

			return m_Collection[^1].Value;
		}

		public IEnumerator<Item> GetEnumerator()
		{
			return ((IEnumerable<Item>)m_Collection).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		[Serializable]
		public sealed class Item
		{
			[SerializeField]
			private T m_Value;
			[SerializeField, MinEpsilon]
			private float m_Weight;
			[SerializeField, ReadOnly]
			[Limited(0f, 100f)]
			private float m_Chance;

			public T Value
			{
				get => m_Value;
			}

			public float Weight
			{
				get => m_Weight;
			}

			public float Chance
			{
				get => m_Chance;
			}

		}

	}
}