#nullable enable

namespace Clockwork.Runtime
{
	using System.Collections.Generic;

	public interface IPool<T> : IReadOnlyCollection<T>
	{
		T Provide();

	}
}