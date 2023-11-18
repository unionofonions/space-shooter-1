
namespace Bertis
{
	using UnityEngine;

	[CreateAssetMenu(fileName = c_FileName, menuName = c_MenuName)]
	public sealed class LevelInfo : ScriptableObject
	{
		private const string c_FileName = nameof(LevelInfo);
		private const string c_MenuName = "Bertis/Game/" + c_FileName;

		[SerializeField]
		private StageInfo[] m_Stages;
		[SerializeField]
		private AudioClipRef m_ThemeMusic;

		public StageInfo[] Stages
		{
			get => m_Stages;
		}

		public AudioClipRef ThemeMusic
		{
			get => m_ThemeMusic;
		}

	}
}