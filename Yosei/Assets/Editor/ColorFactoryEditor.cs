using UnityEditor;

[CustomEditor(typeof(ColorFactory))]
public class ColorFactoryEditor : Editor
{

    public override void OnInspectorGUI()
    {
        ColorFactory color_factory = (ColorFactory)target;

        color_factory.Saturation = EditorGUILayout.Slider(
                "Saturation", color_factory.Saturation, 0f, 1f);

        color_factory.Value = EditorGUILayout.Slider(
                "Value", color_factory.Value, 0f, 1f);

        color_factory.Nb_colors = EditorGUILayout.IntSlider(
                "Nb colors base palette", color_factory.Nb_colors, 10, 100);

        color_factory.Hl_offset = EditorGUILayout.Slider(
                "Highlight offset", color_factory.Hl_offset, -1f, 1f);

        base.OnInspectorGUI();
    }
}