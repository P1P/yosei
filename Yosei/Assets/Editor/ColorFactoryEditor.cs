using UnityEditor;

[CustomEditor(typeof(ColorFactory))]
public class ColorFactoryEditor : Editor
{

    public override void OnInspectorGUI()
    {
        ColorFactory color_factory = (ColorFactory)target;

        color_factory.m_saturation = EditorGUILayout.Slider(
                "Saturation", color_factory.m_saturation, 0f, 1f);

        color_factory.m_value = EditorGUILayout.Slider(
                "Value", color_factory.m_value, 0f, 1f);

        color_factory.m_nb_colors = EditorGUILayout.IntSlider(
                "Nb colors base palette", color_factory.m_nb_colors, 10, 100);

        base.OnInspectorGUI();
    }
}