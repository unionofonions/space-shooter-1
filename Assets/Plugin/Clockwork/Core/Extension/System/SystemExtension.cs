#nullable enable

namespace Clockwork
{
	using System;
	using System.Collections.Generic;

	static public class SystemExtension
	{
		static public string GetName(this Type type)
		{
			Diagnosis.ASSERT(type != null);

			const string nestedTypeSeparator = "+";

			return type.IsNested
				? $"{type.DeclaringType.GetName()}{nestedTypeSeparator}{type.Name}"
				: type.Name;
		}

		static public IEnumerable<Type> IterateHierarchy(this Type type)
		{
			Diagnosis.ASSERT(type != null);

			var iterator = type;
			do
			{
				yield return iterator;
			} while ((iterator = iterator.BaseType) != null);
		}
		
	}
}