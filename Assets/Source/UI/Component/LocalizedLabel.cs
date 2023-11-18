
namespace Bertis.UI
{
	using UnityEngine;

	using Label = TMPro.TextMeshProUGUI;

	[DisallowMultipleComponent]
	[RequireComponent(typeof(Label))]
	public sealed class LocalizedLabel : MonoBehaviour
	{
		[SerializeField]
		private string m_Keyword;

		private bool m_Dirty;
		private Label m_Label;

		private void Awake()
		{
			m_Label = GetComponent<Label>();

			m_Dirty = true;
			LocalizationHandler.OnLanguageChanged += OnLanguageChanged;
		}

		private void OnEnable()
		{
			if (m_Dirty)
			{
				UpdateText();
				m_Dirty = false;
			}
		}

		private void OnDestroy()
		{
			LocalizationHandler.OnLanguageChanged -= OnLanguageChanged;
		}

		private void OnLanguageChanged(Language language)
		{
			if (gameObject.activeInHierarchy)
			{
				UpdateText();
				m_Dirty = false;
			}
			else
			{
				m_Dirty = true;
			}
		}

		private void UpdateText()
		{
			m_Label.text = LocalizationHandler.GetLocalizedText(m_Keyword);
		}

	}
}