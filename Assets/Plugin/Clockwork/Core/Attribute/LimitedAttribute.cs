
namespace Clockwork
{
	using System;
	using UnityEngine;

	[EditorHandled]
	public class LimitedAttribute : PropertyAttribute
	{
		public readonly float min;
		public readonly float max;

		public LimitedAttribute(float min, float max)
		{
			if (min > max)
			{
				this.min = max;
				this.max = min;
			}
			else
			{
				this.min = min;
				this.max = max;
			}
		}

		public LimitedAttribute(float min)
		: this(min, Single.PositiveInfinity) { }

	}
}