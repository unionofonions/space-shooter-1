#nullable enable

namespace Clockwork
{
	using System;

	public sealed class EnumIndexException : Exception
	{
		private const string c_MessageIndexNull      = "Enum index is invalid";
		private const string c_MessageIndexUndefined = "Enum index undefined. Type: {0}, Index: {1}";
		private const string c_MessageSystemOutdated = "Enum index is defined but system is outdated. Type: {0}, Index: {1}";

		public EnumIndexException(Enum? index)
		: base(GetMessage(index)) { }

		static private string GetMessage(Enum? index)
		{
			if (index == null)
			{
				return c_MessageIndexNull;
			}

			var type = index.GetType();

			var message = Enum.IsDefined(type, index)
				? c_MessageSystemOutdated
				: c_MessageIndexUndefined;

			return String.Format(message, type, index);
		}

	}
}