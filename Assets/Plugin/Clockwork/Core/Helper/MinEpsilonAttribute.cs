
namespace Clockwork
{
	public sealed class MinEpsilonAttribute : LimitedAttribute
	{
		private const float c_Epsilon = 1e-6f;

		public MinEpsilonAttribute()
		: base(c_Epsilon) { }

	}
}