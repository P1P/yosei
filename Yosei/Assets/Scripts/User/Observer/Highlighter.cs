using UnityEngine;
using System.Collections;

public class Highlighter : MonoBehaviour {
    public int m_mouse_button_hl = 0;

    public LayerMask m_hl_mask;
    public Tile m_hl_tile;
    public bool m_new_tile;

    private Camera m_camera;

    void Awake()
    {
        m_camera = GetComponent<Camera>();
    }

	void Start()
    {
        m_hl_mask = Game.Inst.m_layer_helper.m_tiles_mask;
	}
	
	void Update ()
    {
        Tile tile = null;
        bool whiff = true;
        if (Input.GetMouseButton(m_mouse_button_hl) && !Screen.lockCursor)
        {
            RaycastHit hit;
            Physics.Raycast(m_camera.ScreenPointToRay(Input.mousePosition), out hit, 1000f, m_hl_mask);

            if (hit.distance != 0f)
            {
                tile = hit.collider.gameObject.GetComponent<Tile>(); // We know we can only hit entities in this layer
                whiff = false;
            }
        }

        if (whiff) // Nothing has hit, disable HL for old tile if he exists
        {
            if (m_hl_tile != null)
            {
                m_hl_tile.m_lookable.SetHighlight(false);
                m_hl_tile = null;
            }
        }
        else // We've hit something
        {
            if (m_hl_tile != null) // Someone was here last frame
            {
                if (tile != m_hl_tile) // It's something new, replace
                {
                    m_hl_tile.m_lookable.SetHighlight(false);

                    tile.m_lookable.SetHighlight(true);
                    m_hl_tile = tile;

                    m_new_tile = true;
                }
                else
                {
                    m_new_tile = false;
                }
            }
            else // It's something new, take the empty space
            {
                tile.m_lookable.SetHighlight(true);
                m_hl_tile = tile;

                m_new_tile = true;
            }
        }
	}
}
