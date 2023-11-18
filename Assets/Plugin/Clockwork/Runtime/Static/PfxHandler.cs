
namespace Clockwork.Runtime
{
	using UnityEngine;

	static public class PfxHandler
	{
		static private SComponentMap<PfxProcessor> s_Map;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		static private void Initialize()
		{
			s_Map = new();
		}

		static public void Play(this PfxRef pfxRef, Vector3 position, Quaternion rotation)
		{
			if (pfxRef == null)
			{
				Diagnosis.LOGW("Argument 'pfxRef' should not be null.");
				return;
			}

			if (pfxRef.GetInfo(out var info))
			{
				var processor = s_Map.Provide(info.Scheme);
				processor.transform.SetPositionAndRotation(position, rotation);
				processor.Play();
			}
		}

		static public void Play(this PfxRef pfxRef, Vector3 position)
		{
			Play(pfxRef, position, Quaternion.identity);
		}

	}
}