
namespace Clockwork.Runtime
{
	using UnityEngine;

	public sealed class TransformShakeHandler : MonoBehaviour
	{
		private Pool m_Pool;

		private void Awake()
		{
			m_Pool = new(transform);

			enabled = false;
		}

		private void Update()
		{
			for (var i = m_Pool.Count; --i >= 0;)
			{
				if (m_Pool[i].Active && !m_Pool[i].Update() && --m_Pool.activeCount == 0)
				{
					transform.SetLocalPositionAndRotation(
						Vector3.zero,
						Quaternion.identity);

					enabled = false;
					return;
				}
			}
		}

		public void Play(TransformShakeRef transformShakeRef)
		{
			if (transformShakeRef == null)
			{
				Diagnosis.LOGW("Argument 'transformShakeRef' should not be null");
				return;
			}

			if (transformShakeRef.GetInfo(out var info))
			{
				var processor = m_Pool.Provide();

				processor.Restart(
					info.Position,
					info.Rotation,
					transformShakeRef.Params);

				if (++m_Pool.activeCount == 1)
				{
					enabled = true;
				}
			}
		}

		public void StopAll()
		{
			foreach (var elem in m_Pool)
			{
				elem.Active = false;
			}

			transform.SetLocalPositionAndRotation(
				Vector3.zero,
				Quaternion.identity);

			m_Pool.activeCount = 0;
			enabled = false;
		}

		private class Processor : IStatusProvider
		{
			private readonly Transform m_Transform;

			private Curve3 m_Position;
			private Curve3 m_Rotation;

			private float m_TMul;
			private float m_Timer;

			private Vector3 m_PosMul;
			private Vector3 m_RotMul;

			private Vector3 m_PrevPos;
			private Vector3 m_PrevRot;

			private bool m_Active;

			public Processor(Transform transform)
			{
				m_Transform = transform;
			}

			public bool Active
			{
				get => m_Active;
				set => m_Active = value;
			}

			static private Vector3 GetRandomVector3Sign()
			{
				return new Vector3(
					RandomNumberGenerator.GetInt32(0, 2) == 0 ? -1f : 1f,
					RandomNumberGenerator.GetInt32(0, 2) == 0 ? -1f : 1f,
					RandomNumberGenerator.GetInt32(0, 2) == 0 ? -1f : 1f);
			}

			public void Restart(Curve3 position, Curve3 rotation, EffectParams @params)
			{
				m_Position = position;
				m_Rotation = rotation;

				m_TMul  = 1f / @params.Duration;
				m_Timer = 0f;

				m_PosMul = GetRandomVector3Sign() * @params.Amplitude;
				m_RotMul = GetRandomVector3Sign() * @params.Amplitude;

				m_PrevPos = Vector3.zero;
				m_PrevRot = Vector3.zero;

				m_Active = true;
			}

			public bool Update()
			{
				m_Timer += Time.deltaTime * m_TMul;

				bool ret;

				if (m_Timer >= 1f)
				{
					m_Timer = 1f;
					ret = false;
					m_Active = false;
				}
				else
				{
					ret = true;
				}

				if (m_Position.Valid)
				{
					var curr = m_Position[m_Timer, m_PosMul];
					m_Transform.localPosition += curr - m_PrevPos;
					m_PrevPos = curr;
				}
				if (m_Rotation.Valid)
				{
					var curr = m_Rotation[m_Timer, m_RotMul];
					m_Transform.localEulerAngles += curr - m_PrevRot;
					m_PrevRot = curr;
				}

				return ret;
			}

		}

		private sealed class Pool : SNPool<Processor>
		{
			private readonly Transform m_Transform;
			public int activeCount;

			public Pool(Transform transform)
			{
				m_Transform = transform;
			}

			protected override Processor Create()
			{
				return new(m_Transform);
			}

		}

	}
}