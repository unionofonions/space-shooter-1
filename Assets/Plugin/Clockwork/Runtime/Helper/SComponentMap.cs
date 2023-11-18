
namespace Clockwork.Runtime
{
	using UnityEngine;

	public class SComponentMap<T> : ComponentMap<T> where T : Component, IStatusProvider
	{
		protected override IComponentPool<T> Create(T scheme)
		{
			return new SNComponentPool<T>(scheme);
		}

	}
}