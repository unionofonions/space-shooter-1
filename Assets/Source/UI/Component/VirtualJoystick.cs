
namespace Bertis.UI
{
	using UnityEngine;
	using UnityEngine.EventSystems;
	using UnityEngine.UI;
	using Clockwork;

	[DisallowMultipleComponent]
	[RequireComponent(typeof(Image))]
	public sealed class VirtualJoystick : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
	{
		[SerializeField, NotDefault]
		private RectTransform m_Handle;

		private bool m_Holding;

		public bool GetRotation(out Quaternion rotation)
		{
			if (!m_Holding)
			{
				rotation = default;
				return false;
			}
			else
			{
				var position = m_Handle.localPosition;
				var angle = Mathf.Atan2(position.y, position.x) * Mathf.Rad2Deg - 90f;
				rotation = Quaternion.AngleAxis(angle, Vector3.forward);
				return true;
			}
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			m_Holding = true;
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			m_Holding = false;
			m_Handle.localPosition = Vector3.zero;
		}

		public void OnDrag(PointerEventData eventData)
		{
			m_Handle.position = eventData.position;
		}

	}
}