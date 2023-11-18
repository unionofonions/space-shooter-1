
namespace Bertis
{
	using UnityEngine;

	public interface IPlayerInputHandler
	{
		bool IsShooting();
		bool GetRotation(out Quaternion rotation);

	}
}