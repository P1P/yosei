﻿using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class ColorFactory : MonoBehaviour
{
    #region SINGLETON
    private static ColorFactory _instance = null;
    public static ColorFactory Instance { get { return _instance; } }

    void Awake()
    {
        _instance = this;
    }
    #endregion

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

    public void Start()
    {
        UpdateColors();
    }

    private void UpdateColors()
    {
        m_lst_colors.Clear();

        for (int i = 0; i < m_nb_colors; ++i)
        {
            m_lst_colors.Add(ColorHSV.FromHsv((1f / (m_nb_colors + 1)) * i, m_saturation, m_value));
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
        return ColorHSV.FromHsv(Random.value, m_saturation, m_value);
    }

    public Color GetColor(float p_hue)
    {
        return ColorHSV.FromHsv(p_hue, m_saturation, m_value);
    }

    public Color GetColor(float p_hue, float p_random_offset)
    {
        return ColorHSV.FromHsv(p_hue + Random.Range(-p_random_offset, p_random_offset), m_saturation, m_value);
    }

    public Color GetRandomGrey(float p_base_value = 0.5f, float p_variance = 0.5f)
    {
        return ColorHSV.FromHsv(0f, 0f, p_base_value + Random.Range(-p_variance, p_variance));
    }

    public Color HighlightColor(Color p_color)
    {
        return ColorExtension.Offset(p_color, 0, m_hl_offset, 0);
    }

    public static string ToHexa(Color32 p_color)
    {
        return p_color.r.ToString("X2") + p_color.g.ToString("X2") + p_color.b.ToString("X2");
    }
}
