
namespace Bertis.UI
{
	using UnityEngine;
	using UnityEngine.UI;
	using Clockwork;

	[DisallowMultipleComponent]
	public sealed class SettingsPanel : MonoBehaviour
	{
		[SerializeField, NotDefault]
		private Slider m_EffectVolumeSlider;
		[SerializeField, NotDefault]
		private Slider m_MusicVolumeSlider;
		[SerializeField, NotDefault]
		private Slider m_UIVolumeSlider;
		[SerializeField, NotDefault]
		private Toggle m_EnableCameraShakeToggle;

		private void Awake()
		{
			m_EffectVolumeSlider.value = Settings.EffectVolume;
			m_MusicVolumeSlider.value = Settings.MusicVolume;
			m_UIVolumeSlider.value = Settings.UIVolume;
			m_EnableCameraShakeToggle.isOn = Settings.EnableCameraShake;
		}

	}
}