
namespace Clockwork.Runtime
{
	using System;
	using UnityEngine;

	[Serializable, EditorHandled]
	public sealed class Curve3
	{
		[SerializeField]
		private float m_Amplitude;
		[SerializeField]
		private Curve m_X;
		[SerializeField]
		private Curve m_Y;
		[SerializeField]
		private Curve m_Z;
		[SerializeField, ReadOnly]
		private int m_Valid;

		public Vector3 this[float x, Vector3 multiplier]
		{
			get
			{
				var ret = Vector3.zero;

				if ((m_Valid & 1) != 0) { ret.x = m_X[x] * m_Amplitude * multiplier.x; }
				if ((m_Valid & 2) != 0) { ret.y = m_Y[x] * m_Amplitude * multiplier.y; }
				if ((m_Valid & 4) != 0) { ret.z = m_Z[x] * m_Amplitude * multiplier.z; }

				return ret;
			}
		}

		/// <summary>
		/// Global multiplier for all axes.
		/// If zero, the object is considered invalid,
		/// and the indexer will return <see cref="Vector3.zero"/> no matter what axes are set.
		/// </summary>
		public float Amplitude
		{
			get => m_Amplitude;
		}

		public Curve X
		{
			get => m_X;
		}

		public Curve Y
		{
			get => m_Y;
		}

		public Curve Z
		{
			get => m_Z;
		}

		/// <summary>
		/// Returns <see langword="true"/> if <see cref="Amplitude"/> is not zero,
		/// and at least one axis (<see cref="Curve"/>) is not empty;
		/// <see langword="false"/> otherwise.
		/// </summary>
		public bool Valid
		{
			get => (m_Valid & 8) != 0;
		}

	}
}