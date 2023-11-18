
namespace Bertis
{
	using UnityEngine;
	using Clockwork;
	using Clockwork.Runtime;

	public abstract class Satellite : Actor, IStatusProvider
	{
		[SerializeField, Unsigned]
		private float m_LinearSpeed;
		[SerializeField, Unsigned]
		private int m_Score;

		private bool m_Active;

		public bool Active
		{
			get => m_Active;
		}

		public float LinearSpeed
		{
			get => m_LinearSpeed;
			set => m_LinearSpeed = Mathf.Max(0f, value);
		}

		public int Score
		{
			get => m_Score;
		}


		protected void OnEnable()
		{
			m_Active = true;
		}

		protected void OnDisable()
		{
			m_Active = false;
		}

		public abstract void Activate(Vector3 position);

	}
}