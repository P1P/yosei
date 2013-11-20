using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Lookable))]
[RequireComponent(typeof(Gaugeable))]

public abstract class TileObject : MonoBehaviour
{
	protected Lookable m_lookable;
	protected Gaugeable m_gaugeable;

	protected Tile m_tile;

	public void Start()
	{
		m_lookable = GetComponent<Lookable>();
		m_gaugeable = GetComponent<Gaugeable>();
	}

	// Returns the Tile where this TileObject is, or null if this TileObject isn't on a Tile
	public Tile Get_Tile()
	{
		if (m_tile != null)
		{
			return m_tile;
		}

		return null;
	}

	// Sets this TileObject to be on a Tile
	// Requires this TileObject to not be on a Tile
	public void Set_Tile(Tile p_tile, bool p_repercute = true)
	{
		if (m_tile == null)
		{
			if (p_repercute)
			{
				p_tile.Set_TileObject(this, false);
			}

			m_tile = p_tile;
		}
	}

	// Gets this TileObject out of its currente Tile
	public void Leave_Tile(bool p_repercute = true)
	{
		if (m_tile != null)
		{
			if (p_repercute)
			{
				m_tile.Empty_TileObject(false);
			}

			m_tile = null;
		}
	}

	// Leaves the current Tile and moves to a new one
	public void Move_To_Tile(Tile p_tile)
	{
		Leave_Tile();
		Set_Tile(p_tile);
	}
}
