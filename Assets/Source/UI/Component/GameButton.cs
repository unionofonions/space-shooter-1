
namespace Bertis.UI
{
	using UnityEngine;
	using UnityEngine.EventSystems;
	using UnityEngine.UI;

	[DisallowMultipleComponent]
	[RequireComponent(typeof(Image))]
	public sealed class GameButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
	{
		private bool m_Holding;

		public bool Holding
		{
			get => m_Holding;
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			m_Holding = true;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			m_Holding = false;
		}

	}
}