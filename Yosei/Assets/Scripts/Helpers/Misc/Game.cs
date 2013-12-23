using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
	public GameObject m_object_helpers;
	public GameObject m_object_population;
	public GameObject m_object_map;
	public GameObject m_object_brewer;
    public GameObject m_object_pathfinder;
	public GameObject m_object_observer;

    public Console m_console;
    public ColorFactory m_colors;
    public TextureFactory m_textures;
    public NameFactory m_names;
	public TimeHelper m_time_helper;
	public ScreenHelper m_screen_helper;
    public LayerHelper m_layer_helper;

	public Map m_map;
	public Brewer m_brewer;
    public AstarPath m_pathfinder;


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
		m_object_population = GameObject.Find("Population");
        m_object_map = GameObject.Find("Map");
		m_object_pathfinder = GameObject.Find("Pathfinding");
		m_object_brewer = GameObject.Find("Brewery");
		m_object_observer = GameObject.Find("Observer");

        // Finding Components
        m_console = m_object_helpers.GetComponent<Console>();
		m_time_helper = m_object_helpers.GetComponent<TimeHelper>();
        m_screen_helper = m_object_helpers.GetComponent<ScreenHelper>();
        m_colors = m_object_helpers.GetComponent<ColorFactory>();
        m_textures = m_object_helpers.GetComponent<TextureFactory>();
        m_names = m_object_helpers.GetComponent<NameFactory>();
        m_layer_helper = m_object_helpers.GetComponent<LayerHelper>();

        m_map = m_object_map.GetComponent<Map>();

		m_brewer = m_object_brewer.GetComponent<Brewer>();

        m_pathfinder = m_object_pathfinder.GetComponent<AstarPath>();
    }
}
