
namespace Clockwork
{
	static public class RandomNumberGenerator
	{
		static public float GetSingle(float min, float max)
		{
			return UnityEngine.Random.Range(min, max);
		}

		static public int GetInt32(int low, int high)
		{
			return UnityEngine.Random.Range(low, high);
		}

		static public bool GetBoolean(float trueChance)
		{
			const float epsilon = 1e-6f;

			return GetSingle(epsilon, 1.0f) <= trueChance;
		}

	}
}