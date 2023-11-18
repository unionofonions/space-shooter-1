
namespace Bertis
{
	using System;
	using UnityEngine;
	using Clockwork;

	[CreateAssetMenu(fileName = c_FileName, menuName = c_MenuName)]
	public sealed class LocalizationHandler : ScriptableObject
	{
		private const string c_FileName = nameof(LocalizationHandler);
		private const string c_MenuName = "Bertis/Game/" + c_FileName;

		static public event Action<Language> OnLanguageChanged;

		static private LocalizationHandler s_This;

		[SerializeField]
		private Language m_CurrentLanguage;
		[SerializeField]
		private TranslationData m_EnglishData;
		[SerializeField]
		private TranslationData m_GermanData;
		[SerializeField]
		private TranslationData m_SpanishData;
		[SerializeField]
		private TranslationData m_TurkishData;

		static public Language CurrentLanguage
		{
			get
			{
				return s_This != null
					? s_This.m_CurrentLanguage
					: Language.English;
			}
			set
			{
				if (s_This != null && s_This.m_CurrentLanguage != value)
				{
					s_This.m_CurrentLanguage = value;
					OnLanguageChanged?.Invoke(value);
				}
			}
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		static private void Initialize()
		{
			s_This = Resources.Load<LocalizationHandler>(c_MenuName);

			if (s_This == null)
			{
				Diagnosis.LOGE("No LocalizationHandler asset in database");
				return;
			}
		}

		static public string GetLocalizedText(string keyword)
		{
			if (String.IsNullOrEmpty(keyword))
			{
				return keyword;
			}

			var data = s_This.m_CurrentLanguage switch
			{
				Language.English => s_This.m_EnglishData,
				Language.German => s_This.m_GermanData,
				Language.Spanish => s_This.m_SpanishData,
				Language.Turkish => s_This.m_TurkishData,
				_ => s_This.m_EnglishData,
			};

			foreach (var pair in data.KeywordTextPairs)
			{
				if (keyword == pair.Keyword)
				{
					return pair.Text;
				}
			}

			Diagnosis.LOGW($"No localized text for keyword: {keyword}");

#if UNITY_EDITOR
			return "$invalid-keyword$";
#else
			return String.Empty;
#endif
		}

		static public void SetLanguageByIndex(int index)
		{
			if (s_This != null)
			{
				CurrentLanguage = (Language)index;
			}
		}

	}
}