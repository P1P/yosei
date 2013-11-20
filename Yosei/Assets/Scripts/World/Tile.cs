using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Lookable))]
[RequireComponent(typeof(Gaugeable))]

public abstract class Tile : MonoBehaviour
{
	protected Lookable m_lookable;
	protected Gaugeable m_gaugeable;

	protected TileObject m_tile_object;

	public void Start()
	{
		m_lookable = GetComponent<Lookable>();
		m_gaugeable = GetComponent<Gaugeable>();
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
	// Returns true if TileObject has been placed, otherwise returns false
	public bool Place_TileObject(TileObject p_tileobject)
	{
		if (m_tile_object != null)
		{
			m_tile_object = p_tileobject;
			return true;
		}

		return false;
	}
}
