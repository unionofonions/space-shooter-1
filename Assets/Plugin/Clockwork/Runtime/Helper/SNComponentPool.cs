
namespace Clockwork.Runtime
{
	using System;
	using UnityEngine;

	public class SNComponentPool<T> : NComponentPool<T> where T : Component, IStatusProvider
	{
		static private readonly Func<T, bool> s_GetActive;

		static SNComponentPool()
		{
			s_GetActive = obj => obj.Active;
		}

		public SNComponentPool(T scheme)
		: base(scheme, s_GetActive) { }

	}
}