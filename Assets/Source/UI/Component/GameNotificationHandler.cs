
namespace Bertis.UI
{
	using UnityEngine;
	using UnityEngine.Scripting;
	using Clockwork;

	using Label = TMPro.TextMeshProUGUI;

	[DisallowMultipleComponent]
	[RequireComponent(typeof(Animator))]
	public sealed class GameNotificationHandler : MonoBehaviour
	{
		static private readonly int s_NewRecordAchievedHash;
		static private readonly int s_ItemAcquiredHash;

		[SerializeField, NotDefault]
		private Label m_Label;

		private Animator m_Animator;

		static GameNotificationHandler()
		{
			s_NewRecordAchievedHash = Animator.StringToHash("new_record_achieved");
			s_ItemAcquiredHash      = Animator.StringToHash("item_acquired");
		}

		private void Awake()
		{
			m_Animator = GetComponent<Animator>();

			ScoreHandler.OnNewRecordAchieved += OnNewRecordAchieved;
			Player.OnGunChanged += OnGunChanged;

			gameObject.SetActive(false);
		}

		private void OnNewRecordAchieved(int score)
		{
			var text = LocalizationHandler.GetLocalizedText("new-record-achieved");
			PlayAnimation(s_NewRecordAchievedHash, text);
		}

		private void OnGunChanged(Player player, Gun gun)
		{
			var text = LocalizationHandler.GetLocalizedText("item-acquired");
			PlayAnimation(s_ItemAcquiredHash, text);
		}

		private void PlayAnimation(int hash, string text)
		{
			gameObject.SetActive(true);
			m_Label.text = text;
			m_Animator.Play(hash);
		}

		[Preserve]
		private void Deactivate()
		{
			gameObject.SetActive(false);
		}

	}
}