
namespace Clockwork
{
	using System;

	public readonly ref struct Result<TValue, TError>
	{
		private readonly TValue m_Value;
		private readonly TError m_Error;
		private readonly bool m_Success;

		public Result(TValue value)
		{
			m_Value = value;
			m_Success = true;
			m_Error = default;
		}

		public Result(TError error)
		{
			m_Error = error;
			m_Success = false;
			m_Value = default;
		}

		public TValue Value
		{
			get
			{
				if (!m_Success)
				{
					Diagnosis.LOGW(
						"Result.Success is false, but accessing Result.Value");
				}

				return m_Value;
			}
		}

		public TError Error
		{
			get
			{
				if (m_Success)
				{
					Diagnosis.LOGW(
						"Result.Success is true, but accessing Result.Error");
				}

				return m_Error;
			}
		}

		public bool Success
		{
			get => m_Success;
		}

		public void Match(
			Action<TValue> onSuccess,
			Action<TError> onError)
		{
			if (m_Success)
			{
				onSuccess?.Invoke(m_Value);
			}
			else
			{
				onError?.Invoke(m_Error);
			}
		}

		public override string ToString()
		{
			return m_Success
				? $"Success: {m_Value}"
				: $"Error: {m_Error}";
		}

		static public implicit operator Result<TValue, TError>(TValue value)
		{
			return new(value);
		}

		static public implicit operator Result<TValue, TError>(TError error)
		{
			return new(error);
		}

	}
}