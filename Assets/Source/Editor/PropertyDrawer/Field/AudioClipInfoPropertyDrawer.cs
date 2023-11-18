
namespace Bertis.Editor
{
	using UnityEditor;
	using Clockwork.Editor;

	[CustomPropertyDrawer(typeof(AudioClipInfo))]
	public sealed class AudioClipInfoPropertyDrawer : RecordInfoPropertyDrawer
	{
		protected override bool IsValid(SerializedProperty property)
		{
			var assetProp = property.GetPropertySafe("m_Asset");
			return assetProp.objectReferenceValue != null;
		}

	}
}