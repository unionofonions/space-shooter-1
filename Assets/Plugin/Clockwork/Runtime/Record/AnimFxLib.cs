
namespace Clockwork.Runtime
{
	using System;
	using UnityEngine;

	[CreateAssetMenu(fileName = c_FileName, menuName = c_MenuName)]
	public sealed class AnimFxLib : RecordLib<AnimFxInfo>
	{
		private const string c_FileName = nameof(AnimFxLib);
		private const string c_MenuName = "Clockwork/Runtime/" + c_FileName;

	}

	[Serializable, EditorHandled]
	public sealed class AnimFxInfo : RecordInfo
	{
		[SerializeField, NotDefault]
		private AnimFxProcessor m_Scheme;

		public AnimFxProcessor Scheme
		{
			get => m_Scheme;
		}

	}

	[Serializable]
	public sealed class AnimFxRef : RecordRef<AnimFxInfo> { }

}