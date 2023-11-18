
namespace Bertis
{
	using UnityEngine;
	using Clockwork;

	[RequireComponent(typeof(Rigidbody2D))]
	public class Asteroid : Satellite
	{
		private Rigidbody2D m_Rigidbody;

		private void Awake()
		{
			m_Rigidbody = GetComponent<Rigidbody2D>();
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.TryGetComponent<Player>(out var player))
			{
				var e = new ReactionEvent(Damage);
				player.DealDamage(e);
				Kill(byPlayer: false);
			}
		}

		public override void Activate(Vector3 position)
		{
			var rotation = new Quaternion(
				0f,
				0f,
				RandomNumberGenerator.GetSingle(0f, 1f),
				RandomNumberGenerator.GetSingle(-1f, 1f));

			transform.SetPositionAndRotation(
				position,
				rotation);

			gameObject.SetActive(true);

			var direction = (Player.Reference.transform.position - position).normalized;
			m_Rigidbody.velocity = direction * LinearSpeed;

			ResetProperties();
		}

	}
}