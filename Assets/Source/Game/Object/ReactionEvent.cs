
namespace Bertis
{
	using Clockwork;

	public readonly ref struct ReactionEvent
	{
		public readonly float damage;
		public readonly bool criticalHit;

		public ReactionEvent(float damage)
		{
			this.damage = damage;
			criticalHit = false;
		}

		public ReactionEvent(
			float damage,
			float criticalHitChance,
			float criticalHitMultiplier)
		{
			criticalHit = RandomNumberGenerator.GetBoolean(criticalHitChance);

			this.damage = criticalHit
				? damage * criticalHitMultiplier
				: damage;
		}

	}
}