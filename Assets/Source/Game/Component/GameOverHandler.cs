
namespace Bertis
{
	using UnityEngine;
	using Clockwork;

	[DisallowMultipleComponent]
	public sealed class GameOverHandler : MonoBehaviour
	{
		static private GameOverHandler s_This;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		static private void Initialize()
		{
			s_This = FindObjectOfType<GameOverHandler>(includeInactive: true);

			if (s_This == null)
			{
				Diagnosis.LOGE("No GameOverHandler object in scene");
				return;
			}

			Actor.OnDie += (actor, byPlayer) =>
			{
				if (actor is Player)
				{
					GameHandler.PauseGame();
					s_This.gameObject.SetActive(true);
				}
			};
		}

	}
}