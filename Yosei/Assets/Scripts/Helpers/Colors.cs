using UnityEngine;
using System.Collections.Generic;

public class Colors : MonoBehaviour
{
    public List<Color> m_lst_colors;
    public int m_nb_colors;

    public float m_saturation;
    public float m_value;

    public float m_green;

    public float m_random_range;

	void Awake()
    {
        for (int i = 0; i < m_nb_colors; ++i)
        {
            m_lst_colors.Add(UnityEditor.EditorGUIUtility.HSVToRGB((1f / (m_nb_colors + 1)) * i, m_saturation, m_value));
        }
	}

    public Color GetRandomBaseColor()
    {
        return m_lst_colors[Random.Range(0, m_nb_colors)];
    }

    public Color GetBaseColor(int color)
    {
        return m_lst_colors[color % m_nb_colors];
    }

    public Color GetRandomColor()
    {
        return UnityEditor.EditorGUIUtility.HSVToRGB(Random.value, m_saturation, m_value);
    }

    public Color GetColor(float p_hue)
    {
        return UnityEditor.EditorGUIUtility.HSVToRGB(p_hue, m_saturation, m_value);
    }

    public Color GetColor(float p_hue, float p_random_offset)
    {
        return UnityEditor.EditorGUIUtility.HSVToRGB(p_hue + Random.Range(-p_random_offset, p_random_offset), m_saturation, m_value);
    }

    public Color GetRandomGrey(float p_base_value = 0.5f, float p_variance = 0.5f)
    {
        return UnityEditor.EditorGUIUtility.HSVToRGB(0f, 0f, p_base_value + Random.Range(-p_variance, p_variance));
    }
}
