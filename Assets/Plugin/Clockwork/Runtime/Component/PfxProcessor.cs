
namespace Clockwork.Runtime
{
	using UnityEngine;

	[DisallowMultipleComponent]
	[RequireComponent(typeof(ParticleSystem))]
	public sealed class PfxProcessor : MonoBehaviour, IStatusProvider
	{
		private ParticleSystem m_ParticleSystem;

		public bool Active
		{
			get => m_ParticleSystem.isEmitting;
		}

		private void Awake()
		{
			m_ParticleSystem = GetComponent<ParticleSystem>();
		}

		internal void Play()
		{
			m_ParticleSystem.Play();
		}

	}
}