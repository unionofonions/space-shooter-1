#nullable enable

namespace Clockwork.Runtime
{
	using System.Runtime.InteropServices;
	using UnityEngine;

	using Object = UnityEngine.Object;

	static public class Hierarchy
	{
		static public GameObject CreateGameObject(
			[Optional] string? name,
			[Optional] bool permanent)
		{
			var ret = new GameObject(name);

			if (permanent)
			{
				Object.DontDestroyOnLoad(ret);
			}

			return ret;
		}

		static public T CreateComponent<T>(
			[Optional] string? name,
			[Optional] bool permanent) where T : Component
		{
			var gameObject = CreateGameObject(
				name ?? typeof(T).GetName(),
				permanent);

			return gameObject.AddComponent<T>();
		}

		static public T InstantiateScheme<T>(
			T scheme,
			[Optional] string? name,
			[Optional] bool permanent) where T : Component
		{
			Diagnosis.ASSERT(
				scheme != null,
				"Argument 'scheme' cannot be null");

			var ret = Object.Instantiate(scheme);
			{
				ret.name = name ?? scheme.name;
			}

			if (permanent)
			{
				Object.DontDestroyOnLoad (ret);
			}

			return ret;
		}

	}
}