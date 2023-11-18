
namespace Bertis
{
	using UnityEngine;
	using Clockwork;

	[DisallowMultipleComponent]
	[RequireComponent(typeof(Rigidbody2D))]
	public class EnemyShip : Satellite
	{
		[SerializeField, NotDefault]
		private Gun m_Gun;
		[SerializeField, Unsigned]
		private float m_ShootingDistance;

		private bool m_IsNearPlayer;
		private Rigidbody2D m_Rigidbody;

		private void Awake()
		{
			m_Rigidbody = GetComponent<Rigidbody2D>();

			m_Gun.IsSourcePlayer = false;
		}

		private void FixedUpdate()
		{
			if (m_IsNearPlayer)
			{
				if (Player.Reference.Health.Depleted)
				{
					m_Gun.TriggerPulled = false;
				}
			}
			else
			{
				var distance = Vector3.Distance(
					Player.Reference.transform.position,
					transform.position);

				if (distance <= m_ShootingDistance)
				{
					m_Rigidbody.velocity = Vector2.zero;
					m_Gun.TriggerPulled = true;
					m_IsNearPlayer = true;
				}
			}
		}

		public override void Activate(Vector3 position)
		{
			transform.position = position;
			gameObject.SetActive(true);

			var direction = (Player.Reference.transform.position - position).normalized;
			m_Rigidbody.velocity = direction * LinearSpeed;

			transform.rotation = Quaternion.LookRotation(
				Vector3.forward,
				direction);

			ResetProperties();

			m_Gun.TriggerPulled = false;
			m_IsNearPlayer = false;
		}

	}
}