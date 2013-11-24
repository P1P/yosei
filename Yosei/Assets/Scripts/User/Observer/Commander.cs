using UnityEngine;
using System.Collections;

public class Commander : MonoBehaviour {
    public Highlighter m_highlighter;

	void Awake() {
        m_highlighter = GetComponent<Highlighter>();
	}
	
	void Update ()
    {
        if ((m_highlighter.m_hl_tile != null) && (m_highlighter.m_new_tile))
        {
            foreach (Yosei yosei in GameObject.Find("Population").GetComponentsInChildren<Yosei>())
            {
                yosei.m_pathfinder.GoTo(m_highlighter.m_hl_tile.transform.position);
            }
        }
	}
}
