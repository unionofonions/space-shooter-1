
namespace Bertis
{
	using System;
	using UnityEngine;
	using Clockwork;

	[CreateAssetMenu(fileName = c_FileName, menuName = c_MenuName)]
	public sealed class ScoreHandler : ScriptableObject
	{
		private const string c_FileName = nameof(ScoreHandler);
		private const string c_MenuName = "Bertis/Game/" + c_FileName;

		static public event Action<int> OnScoreChanged;
		static public event Action<int> OnNewRecordAchieved;

		[SerializeField, Unsigned]
		private int m_HighestScore;

		static private ScoreHandler s_This;
		static private int s_CurrentScore;
		static private bool s_NewRecordAchieved;

		static public int HighestScore
		{
			get
			{
				return s_This != null
					? s_This.m_HighestScore
					: 0;
			}
			set
			{
				if (s_This != null)
				{
					s_This.m_HighestScore = value;
				}
			}
		}

		static public int CurrentScore
		{
			get => s_CurrentScore;
		}

		static public bool NewRecordAchieved
		{
			get => s_NewRecordAchieved;
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		static private void Initialize()
		{
			s_This = Resources.Load<ScoreHandler>(c_MenuName);

			if (s_This == null)
			{
				Diagnosis.LOGE("No ScoreHandler asset in database");
				return;
			}
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		static private void RegisterHooks()
		{
			if (s_This == null)
			{
				return;
			}

			LevelHandler.OnStartNewGame += _ =>
			{
				s_NewRecordAchieved = false;
				SetCurrentScore(0);
			};

			Actor.OnDie += (actor, fromPlayer) =>
			{
				if (actor is Satellite satellite && fromPlayer)
				{
					SetCurrentScore(s_CurrentScore + satellite.Score);
				}
			};
		}

		static private void SetCurrentScore(int value)
		{
			s_CurrentScore = value;

			if (s_CurrentScore > s_This.m_HighestScore)
			{
				if (s_This.m_HighestScore != 0 && !s_NewRecordAchieved)
				{
					OnNewRecordAchieved?.Invoke(s_CurrentScore);
					s_NewRecordAchieved = true;
				}

				s_This.m_HighestScore = s_CurrentScore;
			}

			OnScoreChanged?.Invoke(s_CurrentScore);
		}

	}
}