#nullable enable

namespace Clockwork.Runtime
{
	using System;
	using UnityEngine;

	public class NComponentPool<T> : NPool<T>, IComponentPool<T> where T : Component
	{
		private const string c_ContainerName = "[NPOOL:{0}]";

		private readonly T m_Scheme;
		private readonly GameObject m_Container;

		public NComponentPool(T scheme, Func<T, bool> getActive)
		: base(getActive)
		{
			Diagnosis.ASSERT(
				scheme != null,
				"Argument 'scheme' cannot be null");

			m_Scheme = scheme;

			m_Container = Hierarchy.CreateGameObject(
				name: String.Format(c_ContainerName, m_Scheme.name),
				permanent: true);
		}

		public T Scheme
		{
			get => m_Scheme;
		}

		public GameObject Container
		{
			get => m_Container;
		}

		protected override T Create()
		{
			var ret = Hierarchy.InstantiateScheme(
				m_Scheme,
				name: Count.ToString(),
				permanent: true);

			ret.transform.SetParent(
				m_Container.transform,
				worldPositionStays: false);

			return ret;
		}

	}
}