
namespace Clockwork.Runtime
{
	using System;
	using UnityEngine;

	public class GNComponentPool<T> : NComponentPool<T> where T : Component
	{
		static private readonly Func<T, bool> s_GetActive;

		static GNComponentPool()
		{
			s_GetActive = obj => obj.gameObject.activeSelf;
		}

		public GNComponentPool(T scheme)
		: base(scheme, s_GetActive) { }

	}
}