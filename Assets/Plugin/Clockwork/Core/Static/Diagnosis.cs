#nullable enable

namespace Clockwork
{
	using System;
	using System.Runtime.InteropServices;
	using System.Diagnostics;
	using System.Diagnostics.CodeAnalysis;

	using Debug = UnityEngine.Debug;

	static public class Diagnosis
	{
		private const string c_ConditionalDirective = "UNITY_EDITOR";

		[Conditional(c_ConditionalDirective)]
		static public void ASSERT(
			[DoesNotReturnIf(false)] bool condition,
			[Optional] string? message)
		{
			if (!condition)
			{
				throw new AssertionException(message ?? "UNKNOWN");
			}
		}

		[Conditional(c_ConditionalDirective)]
		static public void LOGM(string message)
		{
			Debug.Log(message);
		}

		[Conditional(c_ConditionalDirective)]
		static public void LOGW(string message)
		{
			Debug.LogWarning(message);
		}

		[Conditional(c_ConditionalDirective)]
		static public void LOGE(string message)
		{
			Debug.LogError(message);
		}

		private sealed class AssertionException : Exception
		{
			private const string c_Message = "Assertion failed. Message: {0}";

			public AssertionException(string message)
			: base(String.Format(c_Message, message)) { }

		}

	}
}