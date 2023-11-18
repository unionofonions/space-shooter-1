
namespace Bertis
{
	using UnityEngine;

	public readonly ref struct ProjectileFireEvent
	{
		public readonly Vector3 position;
		public readonly Quaternion rotation;
		public readonly float linearSpeed;
		public readonly float damage;
		public readonly float criticalHitChance;
		public readonly float criticalHitMultiplier;
		public readonly bool isSourcePlayer;

		public ProjectileFireEvent(
			Vector3 position,
			Quaternion rotation,
			float linearSpeed,
			float damage,
			bool isSourcePlayer)
		{
			this.position = position;
			this.rotation = rotation;
			this.linearSpeed = linearSpeed;
			this.damage = damage;
			this.criticalHitChance = 0f;
			this.criticalHitMultiplier = 0f;
			this.isSourcePlayer = isSourcePlayer;
		}

		public ProjectileFireEvent(
			Vector3 position,
			Quaternion rotation,
			float linearSpeed,
			float damage,
			float criticalHitChance,
			float criticalHitMultiplier,
			bool isSourcePlayer)
		{
			this.position = position;
			this.rotation = rotation;
			this.linearSpeed = linearSpeed;
			this.damage = damage;
			this.criticalHitChance = criticalHitChance;
			this.criticalHitMultiplier = criticalHitMultiplier;
			this.isSourcePlayer = isSourcePlayer;
		}

	}
}