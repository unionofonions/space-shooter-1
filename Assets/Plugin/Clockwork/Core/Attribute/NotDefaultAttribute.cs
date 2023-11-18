
namespace Clockwork
{
	using System;
	using UnityEngine;

	[EditorHandled]
	public sealed class NotDefaultAttribute : PropertyAttribute
	{
		public NotDefaultAttribute()
		{
			order = Int32.MinValue;
		}

	}
}