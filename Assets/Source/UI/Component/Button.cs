
namespace Bertis.UI
{
	using UnityEngine;
	using UnityEngine.Events;
	using UnityEngine.EventSystems;
	using UnityEngine.UI;

	[RequireComponent(typeof(Image))]
	public class Button : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField]
		private UnityEvent m_OnClick;
		[SerializeField]
		private AudioClipRef m_ClickSfx;

		public void AddListener(UnityAction action)
		{
			m_OnClick.AddListener(action);
		}

		public void RemoveListener(UnityAction action)
		{
			m_OnClick.RemoveListener(action);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			m_OnClick.Invoke();
			m_ClickSfx.Play1D();
		}

	}
}