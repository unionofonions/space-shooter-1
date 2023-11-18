
namespace Bertis
{
	using System;
	using UnityEngine;
	using Clockwork;

	[CreateAssetMenu(fileName = c_FileName, menuName = c_MenuName)]
	public sealed class AudioClipLib : RecordLib<AudioClipInfo>
	{
		private const string c_FileName = nameof(AudioClipLib);
		private const string c_MenuName = "Bertis/Game/" + c_FileName;

	}

	[Serializable, EditorHandled]
	public sealed class AudioClipInfo : RecordInfo
	{
		[SerializeField, NotDefault]
		private AudioClip m_Asset;
		[SerializeField]
		private AudioType m_AudioType;
		[SerializeField, Percentage]
		private float m_Volume;

		public AudioClip Asset
		{
			get => m_Asset;
		}

		public AudioType AudioType
		{
			get => m_AudioType;
		}

		public float Volume
		{
			get => m_Volume;
		}

	}

	[Serializable]
	public sealed class AudioClipRef : RecordRef<AudioClipInfo> { }

}