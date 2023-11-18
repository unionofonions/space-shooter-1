
namespace Bertis
{
	using Clockwork;
	using Clockwork.Runtime;
	using UnityEngine;

	static public class AudioHandler
	{
		static private GameObject s_Container;
		static private AudioSource s_EffectPlayer;
		static private AudioSource s_MusicPlayer;
		static private AudioSource s_UIPlayer;

		static private float s_MusicVolumeScale;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		static private void Initialize()
		{
			s_Container = Hierarchy.CreateGameObject(
				name: "AudioHandler",
				permanent: true);

			s_EffectPlayer = Hierarchy.CreateComponent<AudioSource>(
				name: "Effect",
				permanent: true);

			s_MusicPlayer = Hierarchy.CreateComponent<AudioSource>(
				name: "Music",
				permanent: true);

			s_UIPlayer = Hierarchy.CreateComponent<AudioSource>(
				name: "UI",
				permanent: true);

			s_EffectPlayer.transform.SetParent(s_Container.transform, worldPositionStays: false);
			s_MusicPlayer.transform.SetParent(s_Container.transform, worldPositionStays: false);
			s_UIPlayer.transform.SetParent(s_Container.transform, worldPositionStays: false);

			s_MusicVolumeScale = 1.0f;

			UpdateVolumes();

			LevelHandler.OnStartNewGame += PlayLevelThemeMusic;
			LevelHandler.OnFlush += StopLevelThemeMusic;
			Settings.OnVolumeChanged += UpdateVolumes;
		}

		static public void Play1D(this AudioClipRef audioClipRef, float volumeScale = 1.0f)
		{
			if (audioClipRef == null)
			{
				Diagnosis.LOGW("Argument 'audioClipRef' should not be null");
				return;
			}

			if (audioClipRef.GetInfo(out var info) && volumeScale > 0.0f)
			{
				var source = GetAudioSourceByType(info.AudioType);
				var volume = info.Volume * volumeScale;
				source.PlayOneShot(info.Asset, volume);
			}
		}

		static public void StopAll(AudioType type)
		{
			var source = GetAudioSourceByType(type);
			source.Stop();
		}

		static private AudioSource GetAudioSourceByType(AudioType type)
		{
			switch (type)
			{
				case AudioType.Effect:
					return s_EffectPlayer;

				case AudioType.Music:
					return s_MusicPlayer;

				case AudioType.UI:
					return s_UIPlayer;

				default:
					Diagnosis.LOGW($"Unknown AudioType. Value: {type}");
					return s_EffectPlayer;
			}
		}

		static private void PlayLevelThemeMusic(LevelInfo levelInfo)
		{
			if (levelInfo.ThemeMusic.GetInfo(out var info))
			{
				s_MusicVolumeScale = info.Volume;
				UpdateVolumes();

				s_MusicPlayer.clip = info.Asset;
				s_MusicPlayer.loop = true;
				s_MusicPlayer.Play();
			}
		}

		static private void StopLevelThemeMusic(LevelInfo levelInfo)
		{
			s_MusicPlayer.Stop();
		}

		static private void UpdateVolumes()
		{
			s_EffectPlayer.volume = Settings.EffectVolume;
			s_MusicPlayer.volume = Settings.MusicVolume * s_MusicVolumeScale;
			s_UIPlayer.volume = Settings.UIVolume;
		}

	}
}