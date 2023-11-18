#nullable enable

namespace Clockwork.Runtime
{
	using UnityEngine;

	public interface IComponentPool<T> : IPool<T>
	{
		T Scheme { get; }
		GameObject? Container => null;
	}
}