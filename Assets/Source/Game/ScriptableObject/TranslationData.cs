
namespace Bertis
{
	using System;
	using UnityEngine;

	[CreateAssetMenu(fileName = c_FileName, menuName = c_MenuName)]
	public sealed class TranslationData : ScriptableObject
	{
		private const string c_FileName = nameof(TranslationData);
		private const string c_MenuName = "Bertis/Game/" + c_FileName;

		[SerializeField]
		private KeywordTextPair[] m_KeywordTextPairs;

		public KeywordTextPair[] KeywordTextPairs
		{
			get => m_KeywordTextPairs;
		}

		[Serializable]
		public sealed class KeywordTextPair
		{
			[SerializeField]
			private string m_Keyword;
			[SerializeField, TextArea]
			private string m_Text;

			public string Keyword
			{
				get => m_Keyword;
			}

			public string Text
			{
				get => m_Text;
			}

		}

	}
}