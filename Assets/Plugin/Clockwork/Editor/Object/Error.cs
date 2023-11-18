#nullable enable

namespace Clockwork.Editor
{
	using System;

	public sealed class Error
	{
		static public readonly Error revert;

		public readonly string? message;

		static Error()
		{
			revert = new(message: null);
		}

		public Error(string? message)
		{
			this.message = message;
		}

		public override string ToString()
		{
			return message ?? String.Empty;
		}

		static public implicit operator Error(string? message)
		{
			return new(message);
		}

	}
}