
namespace Bertis
{
	using System;
	using UnityEngine;
	using Clockwork;

	[CreateAssetMenu(fileName = c_FileName, menuName = c_MenuName)]
	public sealed class Settings : ScriptableObject
	{
		private const string c_FileName = nameof(Settings);
		private const string c_MenuName = "Bertis/Game/" + c_FileName;

		static public event Action OnVolumeChanged;

		static private Settings s_This;
		static private bool s_Valid;

		[SerializeField]
		private bool m_EnableCameraShake;
		[SerializeField]
		private float m_EffectVolume;
		[SerializeField]
		private float m_MusicVolume;
		[SerializeField]
		private float m_UIVolume;

		static public bool EnableCameraShake
		{
			get
			{
				return s_Valid ? s_This.m_EnableCameraShake : true;
			}
			set
			{
				if (s_Valid)
				{
					s_This.m_EnableCameraShake = value;
				}
			}
		}

		static public float EffectVolume
		{
			get
			{
				return s_Valid
					? s_This.m_EffectVolume
					: 1.0f;
			}
			set
			{
				SetVolumeInternal(AudioType.Effect, value);
			}
		}

		static public float MusicVolume
		{
			get
			{
				return s_Valid
					? s_This.m_MusicVolume
					: 1.0f;
			}
			set
			{
				SetVolumeInternal(AudioType.Music, value);
			}
		}

		static public float UIVolume
		{
			get
			{
				return s_Valid
					? s_This.m_UIVolume
					: 1.0f;
			}
			set
			{
				SetVolumeInternal(AudioType.UI, value);
			}
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		static private void Initialize()
		{
			s_This = Resources.Load<Settings>(c_MenuName);
			s_Valid = s_This != null;

			if (!s_Valid)
			{
				Diagnosis.LOGW("No Settings asset in database");
				return;
			}
		}

		static private void SetVolumeInternal(AudioType audioType, float value)
		{
			if (s_Valid)
			{
				var _value = Mathf.Clamp01(value);

				switch (audioType)
				{
					case AudioType.Effect:
						s_This.m_EffectVolume = _value;
						break;
					case AudioType.Music:
						s_This.m_MusicVolume = _value;
						break;
					case AudioType.UI:
						s_This.m_UIVolume = _value;
						break;
				}

				OnVolumeChanged?.Invoke();
			}
		}

	}
}