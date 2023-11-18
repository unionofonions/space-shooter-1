
namespace Bertis.UI
{
	using UnityEngine;
	using UnityEngine.Scripting;
	using Clockwork;
	using Clockwork.Runtime;

	using Label = TMPro.TextMeshProUGUI;

	[DisallowMultipleComponent]
	[RequireComponent(typeof(Canvas), typeof(Animator))]
	public sealed class FloatingTextProcessor : MonoBehaviour, IStatusProvider
	{
		static private readonly int s_BasicDamageHash;
		static private readonly int s_CriticalDamageHash;
		static private readonly int s_HealHash;

		[SerializeField, NotDefault]
		private Label m_Label;

		private bool m_Active;
		private Animator m_Animator;

		static FloatingTextProcessor()
		{
			s_BasicDamageHash    = Animator.StringToHash("basic_damage");
			s_CriticalDamageHash = Animator.StringToHash("critical_damage");
			s_HealHash           = Animator.StringToHash("heal");
		}

		public bool Active
		{
			get => m_Active;
		}

		private void Awake()
		{
			m_Animator = GetComponent<Animator>();

			var canvas = GetComponent<Canvas>();
			canvas.worldCamera = View.Camera;
		}

		public void PlayReactionAnimation(Vector3 position, in ReactionEvent e)
		{
			int hash;
			string text;

			if (e.criticalHit)
			{
				hash = s_CriticalDamageHash;
				text = ((int)e.damage).ToString() + "!";
			}
			else
			{
				hash = s_BasicDamageHash;
				text = ((int)e.damage).ToString();
			}

			PlayAnimation(hash, text, position);
		}

		public void PlayHealAnimation(Vector3 position, float heal)
		{
			PlayAnimation(
				s_HealHash,
				"+" + ((int)heal).ToString(),
				position);
		}

		private void PlayAnimation(int hash, string text, Vector3 position)
		{
			m_Active = true;
			m_Label.text = text;
			transform.position = position;
			gameObject.SetActive(true);
			m_Animator.Play(hash);
		}

		[Preserve]
		private void Deactivate()
		{
			gameObject.SetActive(false);
			m_Active = false;
		}

	}
}