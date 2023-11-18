
namespace Clockwork
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	[Serializable, EditorHandled]
	public sealed class Set<T> : IReadOnlyCollection<T>
	{
		[SerializeField]
		private T[] m_Collection;
		[SerializeField]
		private Mode m_Mode;
		[SerializeField, ReadOnly]
		private int m_Threshold;

		private int m_Index;

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
					return m_Collection[0];

				default:
					break;
			}

			switch (m_Mode)
			{
				case Mode.NonRepRandom:
					return ProvideNonRepRandom();

				case Mode.FullyRandom:
					return ProvideFullyRandom();

				case Mode.Sequential:
					return ProvideSequential();

				default:
					Diagnosis.LOGW($"Unknown Set<T>.Mode. Value: {m_Mode}");
					return m_Collection[0];
			}

		}

		private T ProvideNonRepRandom()
		{
			var index = RandomNumberGenerator.GetInt32(
				m_Threshold,
				m_Collection.Length);

			var ret = m_Collection[index];

			m_Collection[index] = m_Collection[m_Index];
			m_Collection[m_Index] = ret;

			if (++m_Index >= m_Threshold)
			{
				m_Index = 0;
			}

			return ret;
		}

		private T ProvideFullyRandom()
		{
			var index = RandomNumberGenerator.GetInt32(0, m_Collection.Length);
			return m_Collection[index];
		}

		private T ProvideSequential()
		{
			var ret = m_Collection[m_Index];

			if (++m_Index >= m_Collection.Length)
			{
				m_Index = 0;
			}

			return ret;
		}

		public IEnumerator<T> GetEnumerator()
		{
			return ((IEnumerable<T>)m_Collection).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public enum Mode
		{
			NonRepRandom,
			FullyRandom,
			Sequential
		}

	}
}