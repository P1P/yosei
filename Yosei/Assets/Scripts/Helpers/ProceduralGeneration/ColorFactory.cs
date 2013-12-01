using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class ColorFactory : MonoBehaviour
{
    public List<Color> m_lst_colors = new List<Color>();

    [SerializeField]
    [HideInInspector]
    private int _nb_colors;
    public int m_nb_colors
    {
        get { return _nb_colors; }
        set { _nb_colors = value; UpdateColors(); }
    }

    [SerializeField]
    [HideInInspector]
    private float _saturation;
    public float m_saturation
    {
        get { return _saturation; }
        set { _saturation = value; UpdateColors(); }
    }

    [SerializeField]
    [HideInInspector]
    private float _value;
    public float m_value
    {
        get { return _value; }
        set { _value = value; UpdateColors(); }
    }

    [SerializeField]
    [HideInInspector]
    private float _hl_offset;
    public float m_hl_offset
    {
        get { return _hl_offset; }
        set { _hl_offset = value; }
    }

	void Awake()
    {
        UpdateColors();
	}

    private void UpdateColors()
    {
        m_lst_colors.Clear();

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

    public Color HighlightColor(Color p_color)
    {
        float h, s, v;

        UnityEditor.EditorGUIUtility.RGBToHSV(p_color, out h, out s, out v);

        return UnityEditor.EditorGUIUtility.HSVToRGB(h, Mathf.Min(1f, Mathf.Max(0f, s + m_hl_offset)), v);
    }

    public static string ToHexa(Color32 p_color)
    {
        return p_color.r.ToString("X2") + p_color.g.ToString("X2") + p_color.b.ToString("X2");
    }
}
