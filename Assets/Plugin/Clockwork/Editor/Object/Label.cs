#nullable enable

namespace Clockwork.Editor
{
	using System;
	using System.Runtime.InteropServices;
	using UnityEngine;

	public readonly ref struct Label
	{
		private readonly GUIContent? m_Content;

		public Label(GUIContent? content)
		{
			if (content == null)
			{
				m_Content = null;
			}
			else
			{
				m_Content = Pool.Provide();
				{
					m_Content.text = content.text;
					m_Content.tooltip = content.tooltip;
					m_Content.image = content.image;
				}
			}
		}

		public Label(string? text, [Optional] string? tooltip, [Optional] Texture? image)
		{
			m_Content = Pool.Provide();
			{
				m_Content.text = text;
				m_Content.tooltip = tooltip;
				m_Content.image = image;
			}
		}

		static public Label Null
		{
			get => new(content: null);
		}

		static public Label Empty
		{
			get => new(text: String.Empty);
		}

		public GUIContent? Content
		{
			get => m_Content;
		}

		public GUIContent ContentOrEmpty
		{
			get => m_Content ?? GUIContent.none;
		}

		public override string ToString()
		{
			return m_Content?.text ?? String.Empty;
		}

		static public implicit operator Label(GUIContent? content)
		{
			return new(content);
		}

		static public implicit operator Label(string? text)
		{
			return new(text);
		}

		static public implicit operator GUIContent(Label label)
		{
			return label.ContentOrEmpty;
		}

		static private class Pool
		{
			private const int c_Size = 2;

			static private readonly GUIContent[] s_Collection;
			static private int s_Index;

			static Pool()
			{
				s_Collection = new GUIContent[c_Size];
				for (var i = s_Collection.Length; --i >= 0;)
				{
					s_Collection[i] = new();
				}
			}

			static public GUIContent Provide()
			{
				return s_Collection[s_Index = (s_Index + 1) % s_Collection.Length];
			}

		}

	}
}