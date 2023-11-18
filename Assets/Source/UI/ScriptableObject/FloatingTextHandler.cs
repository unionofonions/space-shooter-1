
namespace Bertis.UI
{
	using UnityEngine;
	using Clockwork;
	using Clockwork.Runtime;

	[CreateAssetMenu(fileName = c_FileName, menuName = c_MenuName)]
	public sealed class FloatingTextHandler : ScriptableObject
	{
		private const string c_FileName = nameof(FloatingTextHandler);
		private const string c_MenuName = "Bertis/UI/" + c_FileName;

		static private FloatingTextHandler s_This;
		static private SNComponentPool<FloatingTextProcessor> s_Pool;

		[SerializeField, NotDefault]
		private FloatingTextProcessor m_ProcessorScheme;
		[SerializeField, Unsigned]
		private float m_RandomPositionRadius;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		static private void Initialize()
		{
			s_This = Resources.Load<FloatingTextHandler>(c_MenuName);

			if (s_This == null)
			{
				Diagnosis.LOGW("No FloatingTextHandler asset in database");
				return;
			}

			if (s_This.m_ProcessorScheme == null)
			{
				Diagnosis.LOGW("FloatingTextHandler.m_ProcessorScheme should not be null");
				return;
			}

			s_Pool = new(s_This.m_ProcessorScheme);

			// First instance causes lag, creating one on load prevents it
			s_Pool.Provide().gameObject.SetActive(false);

			Actor.OnReaction += OnReaction;
			Actor.OnHeal += OnHeal;
		}

		static private void OnReaction(Actor target, in ReactionEvent e)
		{
			var processor = s_Pool.Provide();
			var position = target.transform.position + RandomPositionProvider.Provide();
			processor.PlayReactionAnimation(position, e);
		}

		static private void OnHeal(Actor actor, float amount)
		{
			var processor = s_Pool.Provide();
			processor.PlayHealAnimation(actor.transform.position, amount);
		}

		static private class RandomPositionProvider
		{
			private const int c_BufferSize = 16;

			static private readonly Vector3[] s_Buffer;
			static private int s_Index;

			static RandomPositionProvider()
			{
				s_Buffer = new Vector3[c_BufferSize];
				for (var i = s_Buffer.Length; --i >= 0;)
				{
					s_Buffer[i] = Random.insideUnitCircle * s_This.m_RandomPositionRadius;
				}
			}

			static public Vector3 Provide()
			{
				return s_Buffer[s_Index = (s_Index + 1) % c_BufferSize];
			}

		}

	}
}