using UnityEditor;

[CustomEditor(typeof(BannerHolder))]
public class BannerHolderEditor : Editor
{
	public override void OnInspectorGUI()
	{
		BannerHolder banner_holder = (BannerHolder)target;

		banner_holder.m_banner_height = EditorGUILayout.Slider(
				"Height", banner_holder.m_banner_height, 0.1f, 10f);

		banner_holder.m_banner_scale = EditorGUILayout.Slider(
				"Scale", banner_holder.m_banner_scale, 0.1f, 50f);
	}
}