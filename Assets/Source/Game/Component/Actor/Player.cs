
namespace Bertis
{
	using System;
	using UnityEngine;

	[DisallowMultipleComponent]
	public sealed class Player : Actor
	{
		static public event Action<Player, Gun> OnGunChanged;

		static private Player s_Reference;

		private Gun m_Gun;

		static public Player Reference
		{
			get => s_Reference;
		}

		public Gun Gun
		{
			get => m_Gun;
			set
			{
				if (m_Gun != null)
				{
					m_Gun.transform.SetParent(null);
				}

				m_Gun = value;

				if (m_Gun != null)
				{
					m_Gun.IsSourcePlayer = true;

					m_Gun.transform.SetParent(
						transform,
						worldPositionStays: false);

					m_Gun.transform.SetLocalPositionAndRotation(
						Vector3.zero,
						Quaternion.identity);
				}

				OnGunChanged?.Invoke(this, value);
			}
		}

		private void Awake()
		{
			s_Reference = this;
		}

	}
}