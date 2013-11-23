using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Lookable))]
[RequireComponent(typeof(Gaugeable))]
[RequireComponent(typeof(BoxCollider))]

public abstract class Tile : MonoBehaviour
{
    public Lookable m_lookable { get; protected set; }
    public Gaugeable m_gaugeable { get; protected set; }

    protected MeshFilter m_mesh_filter;

	protected TileObject m_tile_object;

	public void Awake()
	{
		m_lookable = GetComponent<Lookable>();
        m_gaugeable = GetComponent<Gaugeable>();

        m_mesh_filter = GetComponent<MeshFilter>();
	}

	// Returns the TileObject placed on this Tile, or null if no TileObject on Tile
	public TileObject Get_TileObject()
	{
		if (m_tile_object != null)
		{
			return m_tile_object;
		}

		return null;
	}

	// Places the TileObject on this Tile
	// Requires the Tile to be Empty
	public void Set_TileObject(TileObject p_tileobject, bool p_repercute = true)
	{
		if (m_tile_object == null)
		{
			if (p_repercute)
			{
				p_tileobject.Set_Tile(this, false);
			}

			m_tile_object = p_tileobject;
		}
	}

	// Empties this Tile of its Tile Object
	public void Empty_TileObject(bool p_repercute = true)
	{
		if (m_tile_object != null)
		{
			if (p_repercute)
			{
				m_tile_object.Leave_Tile(false);
			}

			m_tile_object = null;
		}
	}

	// Gets the current TileObject out of this Tile, and replaces it with a new one
	public void Replace_TileObject(TileObject p_tileobject)
	{
		Empty_TileObject();
		Set_TileObject(p_tileobject);
	}
}
