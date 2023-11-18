
namespace Bertis
{
	using UnityEngine;
	using Clockwork;

	[DisallowMultipleComponent]
	[RequireComponent(typeof(Player))]
	public sealed class PlayerController : MonoBehaviour
	{
		static private IPlayerInputHandler s_InputHandler;

		private Player m_Player;

		static public void SetHandler(IPlayerInputHandler handler)
		{
			if (handler == null)
			{
				Diagnosis.LOGW("Argument 'handler' should not be null");
				return;
			}

			s_InputHandler = handler;
		}

		private void Awake()
		{
			m_Player = GetComponent<Player>();
		}

		private void Start()
		{
			if (s_InputHandler == null)
			{
				s_InputHandler = new DefaultInputHandler();
			}
		}

		private void Update()
		{
			if (m_Player.Gun != null)
			{
				m_Player.Gun.TriggerPulled = s_InputHandler.IsShooting();
			}

			if (s_InputHandler.GetRotation(out var rotation))
			{
				transform.rotation = rotation;
			}
		}

		private class DefaultInputHandler : IPlayerInputHandler
		{
			public bool IsShooting()
			{
				return Input.GetMouseButton(0);
			}

			public bool GetRotation(out Quaternion rotation)
			{
				var direction = View.GetPointerWorldPosition() - Player.Reference.transform.position;
				var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
				rotation = Quaternion.AngleAxis(angle, Vector3.forward);
				return true;
			}
		}

	}
}