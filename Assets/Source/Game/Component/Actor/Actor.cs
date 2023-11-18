
namespace Bertis
{
	using System;
	using UnityEngine;
	using Clockwork;
	using Clockwork.Runtime;

	public class Actor : MonoBehaviour
	{
		static public event ReactionDelegate OnReaction;
		static public event Action<Actor, float> OnHeal;
		static public event Action<Actor, Quantity> OnHealthChanged;
		static public event Action<Actor, bool> OnDie;

		[Header("Self")]
		[SerializeField, Unsigned]
		private float m_Damage;
		[SerializeField]
		private Quantity m_Health;

		[Header("Effect")]
		[SerializeField]
		private PfxRef m_OnDiePfx;
		[SerializeField]
		private AnimFxRef m_OnDieAfx;
		[SerializeField]
		private AudioClipRef m_OnDieSfx;
		[SerializeField]
		private TransformShakeRef m_OnDieShake;

		public float Damage
		{
			get => m_Damage;
			set => m_Damage = Mathf.Max(0f, value);
		}

		public Quantity Health
		{
			get => m_Health;
			set
			{
				m_Health = value;
				OnHealthChanged?.Invoke(this, value);
			}
		}

		public void DealDamage(in ReactionEvent e)
		{
			var health = m_Health;
			health.Current -= e.damage;
			Health = health;

			OnReaction?.Invoke(this, e);

			if (m_Health.Depleted)
			{
				Kill(byPlayer: true);
			}
		}

		public void Heal(float amount)
		{
			if (amount > 0f)
			{
				var _amount = Mathf.Min(
					amount,
					m_Health.Maximum - m_Health.Current);

				var health = m_Health;
				health.Current += _amount;
				m_Health = health;

				OnHeal?.Invoke(this, _amount);
			}
		}

		public void Kill(bool byPlayer, bool playEffects = true)
		{
			OnDie?.Invoke(this, byPlayer);
			gameObject.SetActive(false);

			if (playEffects)
			{
				PlayEffects();
			}
		}

		public void ResetProperties()
		{
			var health = m_Health;
			health.Current = health.Maximum;
			Health = health;
		}

		private void PlayEffects()
		{
			m_OnDiePfx.Play(transform.position);
			m_OnDieAfx.Play(transform.position);
			m_OnDieSfx.Play1D();
			View.Shake(m_OnDieShake);
		}

	}
}