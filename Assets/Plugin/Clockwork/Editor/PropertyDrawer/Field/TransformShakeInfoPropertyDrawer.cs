
namespace Clockwork.Editor
{
	using UnityEditor;
	using Clockwork.Runtime;

	[CustomPropertyDrawer(typeof(TransformShakeInfo))]
	public sealed class TransformShakeInfoPropertyDrawer : RecordInfoPropertyDrawer
	{
		protected override bool IsValid(SerializedProperty property)
		{
			return IsValid("m_Position") || IsValid("m_Rotation");

			bool IsValid(string name)
			{
				var validProp = property.GetPropertySafe(name)
					.GetPropertySafe("m_Valid");

				return (validProp.intValue & 8) != 0;
			}

		}

	}
}