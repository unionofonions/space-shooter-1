
namespace Bertis
{
	using UnityEngine;
	using Clockwork.Runtime;

	[DisallowMultipleComponent]
	[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
	public class Projectile : MonoBehaviour, IStatusProvider
	{
		private float m_Damage;
		private float m_CriticalHitChance;
		private float m_CriticalHitMultiplier;
		private bool m_IsSourcePlayer;

		private bool m_Active;
		private Rigidbody2D m_Rigidbody;

		public bool Active
		{
			get => m_Active;
		}

		private void Awake()
		{
			m_Rigidbody = GetComponent<Rigidbody2D>();
		}

		private void OnEnable()
		{
			m_Active = true;
		}

		private void OnDisable()
		{
			m_Active = false;
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			// When two projectiles collide, OnTriggerEnter2D is called twice.
			// This check prevents unnecessary calculations
			if (!m_Active)
			{
				return;
			}

			if (collision.TryGetComponent<Projectile>(out var projectile))
			{
				if (projectile.m_Active && m_IsSourcePlayer != projectile.m_IsSourcePlayer)
				{
					gameObject.SetActive(false);
					projectile.gameObject.SetActive(false);

					EffectHandler.ProjectileCollisionPfx.Play(transform.position);
				}
			}
			else if (collision.TryGetComponent<Actor>(out var actor))
			{
				if (m_IsSourcePlayer != actor is Player)
				{
					var e = new ReactionEvent(
						m_Damage,
						m_CriticalHitChance,
						m_CriticalHitMultiplier);

					actor.DealDamage(e);
					gameObject.SetActive(false);
				}
			}
		}

		public void Fire(ProjectileFireEvent e)
		{
			transform.SetPositionAndRotation(
				e.position,
				e.rotation);

			gameObject.SetActive(true);
			m_Rigidbody.velocity = transform.up * e.linearSpeed;

			m_Damage = e.damage;
			m_CriticalHitChance = e.criticalHitChance;
			m_CriticalHitMultiplier = e.criticalHitMultiplier;
			m_IsSourcePlayer = e.isSourcePlayer;
		}

	}
}