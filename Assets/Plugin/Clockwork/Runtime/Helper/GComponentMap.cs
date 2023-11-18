
namespace Clockwork.Runtime
{
	using UnityEngine;

	public class GComponentMap<T> : ComponentMap<T> where T : Component
	{
		protected override IComponentPool<T> Create(T scheme)
		{
			return new GNComponentPool<T>(scheme);
		}

	}
}