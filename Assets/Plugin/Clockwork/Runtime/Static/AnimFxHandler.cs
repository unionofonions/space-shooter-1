
namespace Clockwork.Runtime
{
	using UnityEngine;

	static public class AnimFxHandler
	{
		static private SComponentMap<AnimFxProcessor> s_Map;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		static private void Initialize()
		{
			s_Map = new();
		}

		static public void Play(this AnimFxRef animFxRef, Vector3 position, Quaternion rotation)
		{
			if (animFxRef == null)
			{
				Diagnosis.LOGW("Argument 'animFxRef' should not be null");
				return;
			}

			if (animFxRef.GetInfo(out var info))
			{
				var processor = s_Map.Provide(info.Scheme);
				processor.transform.SetPositionAndRotation(position, rotation);
				processor.Play();
			}
		}

		static public void Play(this AnimFxRef animFxRef, Vector3 position)
		{
			Play(animFxRef, position, Quaternion.identity);
		}

	}
}