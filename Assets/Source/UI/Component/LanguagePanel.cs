
namespace Bertis.UI
{
	using UnityEngine;
	using UnityEngine.UI;
	using Clockwork;

	[DisallowMultipleComponent]
	public sealed class LanguagePanel : MonoBehaviour
	{
		[SerializeField]
		private EnumMap<Language, Image> m_ElementBorderMap;

		[SerializeField]
		private Color m_SelectedElementBorderColor;
		[SerializeField]
		private Color m_UnselectedElementBorderColor;

		private void Awake()
		{
			UpdateSelectedLanguage(LocalizationHandler.CurrentLanguage);
			LocalizationHandler.OnLanguageChanged += UpdateSelectedLanguage;
		}

		private void UpdateSelectedLanguage(Language language)
		{
			foreach (var elem in m_ElementBorderMap)
			{
				elem.color = m_UnselectedElementBorderColor;
			}

			m_ElementBorderMap[(int)language].color = m_SelectedElementBorderColor;
		}

	}
}