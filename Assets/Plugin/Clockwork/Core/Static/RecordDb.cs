
namespace Clockwork
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Runtime.InteropServices;
	using UnityEngine;

	static public class RecordDb
	{
		static private readonly Dictionary<Type, object[]> s_Map;

		static RecordDb()
		{
			s_Map = new();
		}

		static public Result<object[], string> GetLibraries(Type infoType, [Optional] bool errorIfEmpty)
		{
			if (infoType == null || !infoType.IsSubclassOf(typeof(RecordInfo)))
			{
				return "Invalid type";
			}

			if (!s_Map.TryGetValue(infoType, out var ret))
			{
				ret = LoadLibraries(infoType);
				s_Map.Add(infoType, ret);
			}

			if (ret == null)
			{
				return $"No corresponding RecordLib type exists in the assembly of {infoType}";
			}

			if (ret.Length == 0 && errorIfEmpty)
			{
				return $"No corresponding RecordLib asset exists in database";
			}

			return ret;
		}

		static public void ClearAll()
		{
			s_Map.Clear();
		}

		static public void Clear(Type infoType)
		{
			s_Map.Remove(infoType);
		}

		static private object[] LoadLibraries(Type infoType)
		{
			var libType = FindLibraryType(infoType);

			if (libType == null)
			{
				return null;
			}

			return Resources.LoadAll(
				path: String.Empty,
				libType);
		}

		static private Type FindLibraryType(Type infoType)
		{
			var baseType = typeof(RecordLib<>).MakeGenericType(infoType);

			return infoType.Assembly
				.GetTypes()
				.FirstOrDefault(type => type.IsSubclassOf(baseType));

		}

	}
}