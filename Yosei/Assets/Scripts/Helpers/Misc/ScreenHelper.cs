using UnityEngine;
using System.Collections;

public class ScreenHelper : MonoBehaviour
{
    [HideInInspector]
    public int m_screen_width { get; private set; }
    [HideInInspector]
    public int m_screen_height { get; private set; }
    [HideInInspector]
    public float m_ratio { get; private set; }

    private Resolution m_screen_resolution;

    void Start()
    {
        InitializeScreen();
    }

    private void InitializeScreen()
    {
        m_screen_resolution = Screen.currentResolution;
        m_screen_width = (int)(m_screen_resolution.width);
        m_screen_height = (int)(m_screen_resolution.height);
        m_ratio = m_screen_width / m_screen_height;
    }

    public float W_to_px(float perc)
    {
        return (m_screen_width * perc);
    }

    public float H_to_px(float perc)
    {
        return (m_screen_height * perc);
    }
}
