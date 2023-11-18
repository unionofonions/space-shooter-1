
namespace Bertis.UI
{
	using UnityEngine;
	using Clockwork;

	[DisallowMultipleComponent]
	public sealed class MobileInputHandler : MonoBehaviour, IPlayerInputHandler
	{
		[SerializeField]
		private bool m_OverrideInputHandler;
		[SerializeField, NotDefault]
		private VirtualJoystick m_RotationJoystick;
		[SerializeField, NotDefault]
		private GameButton m_ShootingButton;

		private void Awake()
		{
			if (m_OverrideInputHandler)
			{
				PlayerController.SetHandler(this);
			}

			m_RotationJoystick.gameObject.SetActive(m_OverrideInputHandler);
			m_ShootingButton.gameObject.SetActive(m_OverrideInputHandler);
		}

		public bool IsShooting()
		{
			return m_ShootingButton.Holding;
		}

		public bool GetRotation(out Quaternion rotation)
		{
			return m_RotationJoystick.GetRotation(out rotation);
		}

	}
}