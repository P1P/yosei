using UnityEditor;

[CustomEditor(typeof(BannerHolder))]
public class BannerHolderEditor : Editor
{
	public override void OnInspectorGUI()
	{
		BannerHolder banner_holder = (BannerHolder)target;

		banner_holder.Banner_height = EditorGUILayout.Slider(
				"Height", banner_holder.Banner_height, 0.1f, 10f);

		banner_holder.Banner_scale = EditorGUILayout.Slider(
				"Scale", banner_holder.Banner_scale, 0.1f, 50f);
	}
}