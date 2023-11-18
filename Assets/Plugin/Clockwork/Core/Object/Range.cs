#nullable enable

namespace Clockwork
{
	using System;
	using UnityEngine;

	[Serializable]
	public struct Range : IEquatable<Range>, IFormattable
	{
		public float start;
		public float end;

		public Range(float start, float end)
		{
			this.start = start;
			this.end = end;
		}

		public float Delta
		{
			get => end - start;
		}

		public float Length
		{
			get => Math.Abs(Delta);
		}

		public float Random
		{
			get => RandomNumberGenerator.GetSingle(start, end);
		}

		public float Limit(float value)
		{
			return Mathf.Clamp(value, start, end);
		}

		public bool Limits(float value)
		{
			return value >= start && value <= end;
		}

		public bool Equals(Range other)
		{
			return start == other.start
				&& end == other.end;
		}

		public string ToString(string? format, IFormatProvider? formatProvider)
		{
			return String.Format(
				"[{0}..{1}]",
				start.ToString(format, formatProvider),
				start.ToString(format, formatProvider));

		}

		public override int GetHashCode()
		{
			return HashCode.Combine(start, end);
		}

		public override bool Equals(object? obj)
		{
			return obj is Range other && Equals(other);
		}

		public override string ToString()
		{
			return ToString(format: null, formatProvider: null);
		}

	}
}