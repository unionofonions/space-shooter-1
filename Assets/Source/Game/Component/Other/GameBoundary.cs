
namespace Bertis
{
	using UnityEngine;

	[DisallowMultipleComponent]
	[RequireComponent(typeof(Collider2D))]
	public sealed class GameBoundary : MonoBehaviour
	{
		private void OnTriggerExit2D(Collider2D collision)
		{
			collision.gameObject.SetActive(false);
		}

	}
}