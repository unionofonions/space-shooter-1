using System;
using UnityEngine;

namespace Bertis
{
	using Clockwork;

	[Serializable]
	public sealed class StageInfo
	{
		[SerializeField, NotDefault]
		private Gun m_PlayerGun;
		[SerializeField, Unsigned]
		private float m_SpawnDelay;
		[SerializeField, MinOne]
		private int m_SpawnCount;
		[SerializeField]
		private Range m_SpawnInterval;
		[SerializeField]
		private WeightedSet<Satellite> m_Satellites;

		public Gun PlayerGun
		{
			get => m_PlayerGun;
		}

		public float SpawnDelay
		{
			get => m_SpawnDelay;
		}

		public int SpawnCount
		{
			get => m_SpawnCount;
		}

		public Range SpawnInterval
		{
			get => m_SpawnInterval;
		}

		public WeightedSet<Satellite> Satellites
		{
			get => m_Satellites;
		}

	}
}