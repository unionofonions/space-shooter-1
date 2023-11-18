
namespace Bertis.UI
{
	using UnityEngine;
	using UnityEngine.UI;
	using Clockwork;

	[DisallowMultipleComponent]
	public sealed class PlayerBarHandler : MonoBehaviour
	{
		[SerializeField, NotDefault]
		private Image m_Fill;

		private void OnEnable()
		{
			Actor.OnHealthChanged += OnActorHealthChanged;

			if (Player.Reference != null)
			{
				OnActorHealthChanged(
					Player.Reference,
					Player.Reference.Health);
			}
			else
			{
				m_Fill.fillAmount = 1.0f;
			}
		}

		private void OnDisable()
		{
			Actor.OnHealthChanged -= OnActorHealthChanged;
		}

		private void OnActorHealthChanged(Actor actor, Quantity health)
		{
			if (actor is Player)
			{
				m_Fill.fillAmount = health.Ratio;
			}
		}

	}
}