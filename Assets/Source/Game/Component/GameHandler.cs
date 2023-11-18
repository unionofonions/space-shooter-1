
namespace Bertis
{
	using UnityEngine;

	[DisallowMultipleComponent]
	public sealed class GameHandler : MonoBehaviour
	{
		static private bool s_GamePaused;

		static public bool GamePaused
		{
			get => s_GamePaused;
		}

		static public void PauseGame()
		{
			if (!s_GamePaused)
			{
				Time.timeScale = 0.0f;
				s_GamePaused = true;
			}
		}

		static public void ResumeGame()
		{
			if (s_GamePaused)
			{
				Time.timeScale = 1.0f;
				s_GamePaused = false;
			}
		}

		static public void Quit()
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
		}

	}
}