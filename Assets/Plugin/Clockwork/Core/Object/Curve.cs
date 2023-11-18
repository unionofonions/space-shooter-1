
namespace Clockwork
{
	using System;
	using UnityEngine;

	[Serializable, EditorHandled]
	public class Curve : IFunction
	{
		[SerializeField]
		private AnimationCurve m_Adaptee;

		public float this[float x]
		{
			get => m_Adaptee.Evaluate(x);
		}

		public int KeyCount
		{
			get => m_Adaptee.length;
		}

	}
}