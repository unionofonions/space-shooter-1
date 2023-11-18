
namespace Bertis
{
	using System;
	using UnityEngine;
	using Clockwork;
	using Clockwork.Runtime;

	[DefaultExecutionOrder(Int32.MinValue)]
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Camera))]
	public sealed class View : MonoBehaviour
	{
		static private View s_This;
		static private Camera s_Camera;
		static private float s_Depth;

		[SerializeField, NotDefault]
		private TransformShakeHandler m_ShakeHandler;

		static public Camera Camera
		{
			get => s_Camera;
		}

		static public Vector3 GetPointerWorldPosition()
		{
			Diagnosis.ASSERT(s_Camera != null);

			var screenPos = Input.mousePosition;
			screenPos.z = -s_Depth;
			return s_Camera.ScreenToWorldPoint(screenPos);

		}

		static public void Shake(TransformShakeRef transformShakeRef)
		{
			Diagnosis.ASSERT(transformShakeRef != null);
			Diagnosis.ASSERT(s_This != null);
			Diagnosis.ASSERT(s_This.m_ShakeHandler != null);

			if (Settings.EnableCameraShake)
			{
				s_This.m_ShakeHandler.Play(transformShakeRef);
			}
		}

		private void Awake()
		{
			s_This = this;
			s_Camera = GetComponent<Camera>();

			s_Depth = transform.position.z;
		}

	}
}