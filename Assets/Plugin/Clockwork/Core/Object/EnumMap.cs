
namespace Clockwork
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	[Serializable, EditorHandled]
	public class EnumMap<TEnum, TValue> : IReadOnlyList<TValue> where TEnum : Enum
	{
		[SerializeField]
		protected TValue[] m_Values;

		private protected EnumMap() { }

		public TValue this[int index]
		{
			get
			{
				Diagnosis.ASSERT(
					(uint)index < (uint)m_Values.Length,
					$"Argument 'index' is either out of range, or EnumMap instance is not up to date. index: {index}");

				return m_Values[index];
			}
		}

		public int Count
		{
			get => m_Values.Length;
		}

		public IEnumerator<TValue> GetEnumerator()
		{
			return ((IEnumerable<TValue>)m_Values).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

	}
}