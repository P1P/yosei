using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
    public GameObject m_object_helpers;

    public Console m_console;
    public ColorFactory m_colors;
    public TextureFactory m_textures;
	public TimeHelper m_time_helper;
	public ScreenHelper m_screen_helper;
    public LayerHelper m_layer_helper;

    private static Game instance;
    public static Game Inst
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("ReferenceHelper").AddComponent<Game>();
                instance.transform.parent = GameObject.Find("Helper").transform;
                instance.Initialize();
            }
            return instance;
        }
    }

    public void OnApplicationQuit()
    {
        instance = null;
    }

    public void DestroyInstance()
    {
        instance = null;
    }

    public void Initialize()
    {
        // Finding GameObjects
        m_object_helpers = GameObject.Find("Helper");

        // Finding Components
        m_console = m_object_helpers.GetComponent<Console>();
		m_time_helper = m_object_helpers.GetComponent<TimeHelper>();
        m_screen_helper = m_object_helpers.GetComponent<ScreenHelper>();
        m_colors = m_object_helpers.GetComponent<ColorFactory>();
        m_textures = m_object_helpers.GetComponent<TextureFactory>();
        m_layer_helper = m_object_helpers.GetComponent<LayerHelper>();
    }
}
