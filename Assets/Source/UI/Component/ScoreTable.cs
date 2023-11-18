
namespace Bertis.UI
{
	using UnityEngine;
	using Clockwork;

	using Label = TMPro.TextMeshProUGUI;

	[DisallowMultipleComponent]
	public sealed class ScoreTable : MonoBehaviour
	{
		[SerializeField, NotDefault]
		private Label m_HighestScoreLabel;

		private void OnEnable()
		{
			m_HighestScoreLabel.text = ScoreHandler.HighestScore.ToString();
		}

	}
}