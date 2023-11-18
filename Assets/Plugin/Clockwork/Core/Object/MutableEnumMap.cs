
namespace Clockwork
{
	using System;

	[Serializable]
	public sealed class MutableEnumMap<TEnum, TValue> : EnumMap<TEnum, TValue> where TEnum : Enum
	{
		public new TValue this[int index]
		{
			get => base[index];
			set
			{
				Diagnosis.ASSERT(
					(uint)index < (uint)m_Values.Length,
					$"Argument 'index' is either out of range, or EnumMap instance is not up to date. index: {index}");

				m_Values[index] = value;
			}
		}

	}
}