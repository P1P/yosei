using UnityEngine;
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

    [SerializeField]
    [HideInInspector]
    private int _nb_colors;
    public int Nb_colors
    {
        get { return _nb_colors; }
        set { _nb_colors = value; UpdateColors(); }
    }

    [SerializeField]
    [HideInInspector]
    private float _saturation;
    public float Saturation
    {
        get { return _saturation; }
        set { _saturation = value; UpdateColors(); }
    }

    [SerializeField]
    [HideInInspector]
    private float _value;
    public float Value
    {
        get { return _value; }
        set { _value = value; UpdateColors(); }
    }

    [SerializeField]
    [HideInInspector]
    private float _hl_offset;
    public float Hl_offset
    {
        get { return _hl_offset; }
        set { _hl_offset = value; }
    }

    // Inspector pretty debug display
    public List<Color> Lst_colors = new List<Color>();

    public void Start()
    {
        UpdateColors();
    }

    /// <summary>
    /// Updates the list of base colors
    /// </summary>
    private void UpdateColors()
    {
        Lst_colors.Clear();

        for (int i = 0; i < Nb_colors; ++i)
        {
            Lst_colors.Add(ColorHSV.FromHsv((1f / (Nb_colors + 1)) * i, Saturation, Value));
        }
    }

    /// <summary>
    /// Gets a random base color from the predefined hue list with predetermined saturation and value
    /// </summary>
    /// <returns>A random base color</returns>
    public Color GetRandomBaseColor()
    {
        return Lst_colors[Random.Range(0, Nb_colors)];
    }

    /// <summary>
    /// Gets the base color from the predifined hue list with predetermined saturation and value at the specified index
    /// </summary>
    /// <param name="p_color">The index of the base color in the list</param>
    /// <returns>The base color</returns>
    public Color GetBaseColor(int p_color)
    {
        return Lst_colors[p_color % Nb_colors];
    }

    /// <summary>
    /// Gets a color with random hue and predetermined saturation and value
    /// </summary>
    /// <returns>A random hue color</returns>
    public Color GetRandomColor()
    {
        return ColorHSV.FromHsv(Random.value, Saturation, Value);
    }

    /// <summary>
    /// Gets the color with a specified hue and predetermined saturation and value
    /// </summary>
    /// <param name="p_hue">The specified hue</param>
    /// <returns>The color with specified hue and predetermined saturation and value</returns>
    public Color GetColor(float p_hue)
    {
        return ColorHSV.FromHsv(p_hue, Saturation, Value);
    }

    /// <summary>
    /// Gets a color with a randomly offset hue (from base - offset to base + offset) and predetermined saturation and value
    /// </summary>
    /// <param name="p_hue">The base hue</param>
    /// <param name="p_hue_random_offset">The hue offset</param>
    /// <returns>The random offset color</returns>
    public Color GetColor(float p_hue, float p_hue_random_offset)
    {
        return ColorHSV.FromHsv(
            p_hue + Random.Range(-p_hue_random_offset, p_hue_random_offset),
            Saturation,
            Value);
    }

    /// <summary>
    /// Gets a color with a randomly offset hue and value (from base - offset to base + offset) and a predetemined saturation
    /// </summary>
    /// <param name="p_hue">The base hue</param>
    /// <param name="p_hue_random_offset">The hue offset</param>
    /// <param name="p_value">The base value</param>
    /// <param name="p_value_random_offset">The value offset</param>
    /// <returns>The random offset color</returns>
    public Color GetColor(float p_hue, float p_hue_random_offset, float p_value, float p_value_random_offset)
    {
        return ColorHSV.FromHsv(
            p_hue + Random.Range(-p_hue_random_offset, p_hue_random_offset),
            Saturation,
            p_value + Random.RandomRange(-p_value_random_offset, p_value_random_offset));
    }

    /// <summary>
    /// Gets a 0-hue, 0-saturation, random value color - a gray tone
    /// </summary>
    /// <param name="p_base_value">The base value</param>
    /// <param name="p_variance">The value offset</param>
    /// <returns>A gray tone color</returns>
    public Color GetRandomGrey(float p_base_value = 0.5f, float p_variance = 0.5f)
    {
        return ColorHSV.FromHsv(0f, 0f, p_base_value + Random.Range(-p_variance, p_variance));
    }

    /// <summary>
    /// Offsets the saturation of the color by a predetermined amount
    /// </summary>
    /// <param name="p_color">The color to highlight</param>
    /// <returns>The highlighted color</returns>
    public Color HighlightColor(Color p_color)
    {
        return ColorExtension.Offset(p_color, 0, Hl_offset, 0);
    }

    /// <summary>
    /// Prints the color in a format accepted for NGUI text formating
    /// </summary>
    /// <param name="p_color">The color from which to get an hexadecimal formating</param>
    /// <returns>The hexadecimal formating</returns>
    public static string ToHexa(Color32 p_color)
    {
        return p_color.r.ToString("X2") + p_color.g.ToString("X2") + p_color.b.ToString("X2");
    }
}
