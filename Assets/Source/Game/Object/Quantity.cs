
namespace Bertis
{
	using System;
	using UnityEngine;

	[Serializable]
	public struct Quantity
	{
		[SerializeField]
		private float m_Current;
		[SerializeField]
		private float m_Maximum;

		public float Current
		{
			get => m_Current;
			set => m_Current = Mathf.Clamp(value, 0f, m_Maximum);
		}

		public float Maximum
		{
			get => m_Maximum;
			set => m_Maximum = Mathf.Max(0f, value);
		}

		public float Ratio
		{
			get => m_Current / m_Maximum;
		}

		public bool Depleted
		{
			get => m_Current <= 0f;
		}

		public bool Full
		{
			get => m_Current >= m_Maximum;
		}

		public override string ToString()
		{
			return $"{m_Current}/{m_Maximum}";
		}

	}
}