
namespace Clockwork.Runtime
{
	using UnityEngine;
	using UnityEngine.Scripting;

	[DisallowMultipleComponent]
	[RequireComponent(typeof(Animator))]
	public sealed class AnimFxProcessor : MonoBehaviour, IStatusProvider
	{
		private bool m_Active;

		public bool Active
		{
			get => m_Active;
		}

		public void Play()
		{
			m_Active = true;
			gameObject.SetActive(true);
		}

		[Preserve]
		private void Deactivate()
		{
			gameObject.SetActive(false);
			m_Active = false;
		}

	}
}