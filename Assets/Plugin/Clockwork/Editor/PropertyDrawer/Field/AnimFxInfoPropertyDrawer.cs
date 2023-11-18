
namespace Clockwork.Editor
{
	using UnityEditor;
	using Clockwork.Runtime;

	[CustomPropertyDrawer(typeof(AnimFxInfo))]
	public sealed class AnimFxInfoPropertyDrawer : RecordInfoPropertyDrawer
	{
		protected override bool IsValid(SerializedProperty property)
		{
			var schemeProp = property.GetPropertySafe("m_Scheme");
			return schemeProp.objectReferenceValue != null;
		}

	}
}