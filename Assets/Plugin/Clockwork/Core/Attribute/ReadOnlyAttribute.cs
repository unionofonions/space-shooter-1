
namespace Clockwork
{
	using System;
	using UnityEngine;

	[EditorHandled]
	public sealed class ReadOnlyAttribute : PropertyAttribute
	{
		public ReadOnlyAttribute()
		{
			order = Int32.MinValue;
		}

	}
}