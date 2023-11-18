
namespace Bertis
{
	using UnityEngine;
	using Clockwork;
	using Clockwork.Runtime;

	static public class ProjectileMap
	{
		static private SComponentMap<Projectile> s_Map;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		static private void Initialize()
		{
			s_Map = new();

			LevelHandler.OnFlush += _ =>
			{
				s_Map.DeactivateAll();
			};
		}

		static public Projectile Provide(Projectile scheme)
		{
			Diagnosis.ASSERT(s_Map != null);
			Diagnosis.ASSERT(scheme != null);

			return s_Map.Provide(scheme);
		}

	}
}