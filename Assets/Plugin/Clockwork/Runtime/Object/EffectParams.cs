
namespace Clockwork.Runtime
{
	using System;
	using UnityEngine;

	[Serializable]
	public struct EffectParams
	{
		[SerializeField]
		private bool m_Enabled;
		[SerializeField]
		private float m_Amplitude;
		[SerializeField]
		private float m_Duration;

		public bool Enabled
		{
			get => m_Enabled;
			set => m_Enabled = value;
		}

		public float Amplitude
		{
			get => m_Amplitude;
			set => m_Amplitude = value;
		}

		public float Duration
		{
			get => m_Duration;
			set => m_Duration = value;
		}

		public bool Valid
		{
			get
			{
				return m_Enabled
					&& m_Amplitude != 0f
					&& m_Duration > 0f;
			}
		}

	}
}