
namespace Bertis.UI
{
	using UnityEngine;

	using Label = TMPro.TextMeshProUGUI;

	[DisallowMultipleComponent]
	[RequireComponent(typeof(Label))]
	public sealed class ScoreLabel : MonoBehaviour
	{
		private Label m_Label;

		private void Awake()
		{
			m_Label = GetComponent<Label>();

			ScoreHandler.OnScoreChanged += UpdateLabel;
		}

		private void OnEnable()
		{
			UpdateLabel(ScoreHandler.CurrentScore);
		}

		private void UpdateLabel(int score)
		{
			m_Label.text = score.ToString();
		}

	}
}