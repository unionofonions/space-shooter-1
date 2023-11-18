
namespace Bertis
{
	using UnityEngine;
	using Clockwork;

	[DisallowMultipleComponent]
	public class Gun : MonoBehaviour
	{
		private const float c_MinFireInterval = 0.1f;
		private const float c_MinProjectileSpeed = 5f;

		[Header("Self")]
		[SerializeField, Unsigned]
		private float m_Damage;
		[SerializeField, Limited(c_MinFireInterval)]
		private float m_FireInterval;
		[SerializeField, Percentage]
		private float m_CriticalHitChance;
		[SerializeField, MinOne]
		private float m_CriticalHitMultiplier;

		[Header("Projectile")]
		[SerializeField, Limited(c_MinProjectileSpeed)]
		private float m_ProjectileSpeed;
		[SerializeField, NotDefault]
		private Projectile m_ProjectileScheme;

		[Header("Effect")]
		[SerializeField]
		private AudioClipRef m_FireSfx;

		[Header("World")]
		[SerializeField, NotDefault]
		private Transform[] m_Muzzles;

		[Header("Other")]
		[SerializeField]
		private string m_Keyword;

		private bool m_IsSourcePlayer;
		private bool m_TriggerPulled;
		private float m_TimeSinceLastFire;

		public bool TriggerPulled
		{
			get => m_TriggerPulled;
			set
			{
				if (m_TriggerPulled != value)
				{
					m_TriggerPulled = value;
					enabled = value;
				}
			}
		}

		public float Damage
		{
			get => m_Damage;
			set => m_Damage = Mathf.Max(0f, value);
		}

		public float FireInterval
		{
			get => m_FireInterval;
			set => m_FireInterval = Mathf.Max(c_MinFireInterval, value);
		}

		public float CriticalHitChance
		{
			get => m_CriticalHitChance;
			set => m_CriticalHitChance = Mathf.Clamp01(value);
		}

		public float CriticalHitMultiplier
		{
			get => m_CriticalHitMultiplier;
			set => m_CriticalHitMultiplier = Mathf.Max(1f, value);
		}

		public float ProjectileSpeed
		{
			get => m_ProjectileSpeed;
			set => m_ProjectileSpeed = Mathf.Max(c_MinProjectileSpeed, value);
		}

		public string Keyword
		{
			get => m_Keyword;
		}

		public bool IsSourcePlayer
		{
			get => m_IsSourcePlayer;
			set => m_IsSourcePlayer = value;
		}

		private void Awake()
		{
			enabled = false;
		}

		private void FixedUpdate()
		{
			if (Time.time - m_TimeSinceLastFire >= m_FireInterval)
			{
				m_TimeSinceLastFire = Time.time;
				Fire();
			}
		}

		private void Fire()
		{
			foreach (var muzzle in m_Muzzles)
			{
				var e = new ProjectileFireEvent(
					muzzle.position,
					muzzle.rotation,
					m_ProjectileSpeed,
					m_Damage,
					m_CriticalHitChance,
					m_CriticalHitMultiplier,
					m_IsSourcePlayer);

				var projectile = ProjectileMap.Provide(m_ProjectileScheme);
				projectile.Fire(e);
			}

			m_FireSfx.Play1D();
		}

	}
}