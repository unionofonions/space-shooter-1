
namespace Clockwork.Runtime
{
	using System;
	using UnityEngine;

	[CreateAssetMenu(fileName = c_FileName, menuName = c_MenuName)]
	public sealed class PfxLib : RecordLib<PfxInfo>
	{
		private const string c_FileName = nameof(PfxLib);
		private const string c_MenuName = "Clockwork/Runtime/" + nameof(PfxLib);

	}

	[Serializable, EditorHandled]
	public sealed class PfxInfo : RecordInfo
	{
		[SerializeField, NotDefault]
		private PfxProcessor m_Scheme;

		public PfxProcessor Scheme
		{
			get => m_Scheme;
		}

	}

	[Serializable]
	public sealed class PfxRef : RecordRef<PfxInfo> { }

}