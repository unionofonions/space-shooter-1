
namespace Bertis
{
	using UnityEngine;
	using Clockwork;
	using Clockwork.Runtime;

	[CreateAssetMenu(fileName = c_FileName, menuName = c_MenuName)]
	public sealed class EffectHandler : ScriptableObject
	{
		private const string c_FileName = nameof(EffectHandler);
		private const string c_MenuName = "Bertis/Game/" + c_FileName;

		static private EffectHandler s_This;

		[SerializeField]
		private PfxRef m_ProjectileCollisionPfx;

		static public PfxRef ProjectileCollisionPfx
		{
			get => s_This.m_ProjectileCollisionPfx;
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		static private void Initialize()
		{
			s_This = Resources.Load<EffectHandler>(c_MenuName);

			if (s_This == null)
			{
				Diagnosis.LOGW("No EffectHandler asset in database");
			}
		}

	}
}