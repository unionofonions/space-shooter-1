
namespace Clockwork.Runtime
{
	using System;
	
	public abstract class SNPool<T> : NPool<T> where T : IStatusProvider
	{
		static private readonly Func<T, bool> s_GetActive;

		static SNPool()
		{
			s_GetActive = obj => obj.Active;
		}

		public SNPool()
		: base(s_GetActive) { }

	}
}