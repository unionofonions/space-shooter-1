
namespace Clockwork
{
	public sealed class PercentageAttribute : LimitedAttribute
	{
		public PercentageAttribute()
		: base(0.0f, 1.0f) { }

	}
}