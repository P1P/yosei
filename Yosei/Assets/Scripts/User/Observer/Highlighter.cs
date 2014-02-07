using UnityEngine;
using System.Collections;

public class Highlighter : MonoBehaviour {
    public Tile Hl_tile { get; private set; }
    public bool New_tile { get; private set; }

    private int _mouse_button_hl = 0;
    private LayerMask _hl_mask;
    private Camera _camera;

    public void Awake()
    {
        _camera = GetComponent<Camera>();
    }

	public void Start()
    {
        _hl_mask = LayerHelper.Instance.Tiles_mask;
	}
	
	public void Update()
    {
        Tile tile = null;
        bool whiff = true;
        if (Input.GetMouseButton(_mouse_button_hl) && !Screen.lockCursor)
        {
            RaycastHit hit;
            Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit, 1000f, _hl_mask);

            if (hit.distance != 0f)
            {
                tile = hit.collider.gameObject.GetComponent<Tile>(); // We know we can only hit entities in this layer
                whiff = false;
            }
        }

        if (whiff) // Nothing has hit, disable HL for old tile if he exists
        {
            if (Hl_tile != null)
            {
                Hl_tile.Lookable.SetHighlight(false);
                Hl_tile = null;
            }
        }
        else // We've hit something
        {
            if (Hl_tile != null) // Someone was here last frame
            {
                if (tile != Hl_tile) // It's something new, replace
                {
                    Hl_tile.Lookable.SetHighlight(false);

                    tile.Lookable.SetHighlight(true);
                    Hl_tile = tile;

                    New_tile = true;
                }
                else
                {
                    New_tile = false;
                }
            }
            else // It's something new, take the empty space
            {
                tile.Lookable.SetHighlight(true);
                Hl_tile = tile;

                New_tile = true;
            }
        }
	}
}
