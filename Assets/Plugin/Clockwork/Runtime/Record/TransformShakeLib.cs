
namespace Clockwork.Runtime
{
	using System;
	using UnityEngine;

	[CreateAssetMenu(fileName = c_FileName, menuName = c_MenuName)]
	public sealed class TransformShakeLib : RecordLib<TransformShakeInfo>
	{
		private const string c_FileName = nameof(TransformShakeLib);
		private const string c_MenuName = "Clockwork/Runtime/" + c_FileName;

	}

	[Serializable, EditorHandled]
	public sealed class TransformShakeInfo : RecordInfo
	{
		[SerializeField]
		private Curve3 m_Position;
		[SerializeField]
		private Curve3 m_Rotation;

		public Curve3 Position
		{
			get => m_Position;
		}

		public Curve3 Rotation
		{
			get => m_Rotation;
		}

	}

	[Serializable]
	public sealed class TransformShakeRef : RecordRef<TransformShakeInfo>
	{
		[SerializeField]
		private EffectParams m_Params;

		public EffectParams Params
		{
			get => m_Params;
		}

		public new bool GetInfo(out TransformShakeInfo info)
		{
			return base.GetInfo(out info) && m_Params.Valid;
		}

	}
}