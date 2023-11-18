
namespace Bertis
{
	using System;
	using System.Collections;
	using UnityEngine;
	using Clockwork;
	using Clockwork.Runtime;

	public sealed class LevelHandler : MonoBehaviour
	{
		static public event Action<LevelInfo> OnStartNewGame;
		static public event Action<LevelInfo> OnFlush;

		[SerializeField, NotDefault]
		private LevelInfo m_LevelInfo;
		[SerializeField]
		private Set<Transform> m_SpawnPoints;

		private SComponentMap<Satellite> m_SatelliteMap;
		private int m_StageIndex;

		private void Awake()
		{
			m_SatelliteMap = new();
		}

		public void StartNewGame()
		{
			if (m_LevelInfo.Stages.Length == 0)
			{
				Diagnosis.LOGW("m_LevelInfo.Stages must not be empty");
				return;
			}

			Player.Reference.ResetProperties();
			Player.Reference.gameObject.SetActive(true);
			OnStartNewGame?.Invoke(m_LevelInfo);
			m_StageIndex = 0;
			StartNextStage();
		}

		public void Flush()
		{
			StopAllCoroutines();
			m_SatelliteMap.DeactivateAll();
			OnFlush?.Invoke(m_LevelInfo);
		}

		private void StartNextStage()
		{
			var stages = m_LevelInfo.Stages;
			var inEndLoop = m_StageIndex >= stages.Length;
			var index = inEndLoop ? stages.Length - 1 : m_StageIndex;
			var current = stages[index];

			StartCoroutine(StartStage(current, inEndLoop));

			if (!inEndLoop)
			{
				++m_StageIndex;
			}
		}

		private IEnumerator StartStage(StageInfo stageInfo, bool inEndLoop)
		{
			if (!inEndLoop)
			{
				var gun = Player.Reference.Gun;
				Player.Reference.Gun = Hierarchy.InstantiateScheme(stageInfo.PlayerGun);

				if (gun != null)
				{
					UnityEngine.Object.Destroy(gun.gameObject);
				}
			}

			yield return new WaitForSeconds(stageInfo.SpawnDelay);

			for (var i = stageInfo.SpawnCount; --i >= 0;)
			{
				var scheme = stageInfo.Satellites.Provide();
				var instance = m_SatelliteMap.Provide(scheme);
				var position = m_SpawnPoints.Provide().position;
				instance.Activate(position);

				var interval = stageInfo.SpawnInterval.Random;
				yield return new WaitForSeconds(interval);

				if (Player.Reference.Health.Depleted)
				{
					yield break;
				}
			}

			StartNextStage();
		}

	}
}