
namespace Clockwork.Editor
{
	using UnityEditor;
	using Clockwork.Runtime;

	[CustomPropertyDrawer(typeof(PfxInfo))]
	public sealed class PfxInfoPropertyDrawer : RecordInfoPropertyDrawer
	{
		protected override bool IsValid(SerializedProperty property)
		{
			var schemeProp = property.GetPropertySafe("m_Scheme");
			return schemeProp.objectReferenceValue != null;
		}

	}
}