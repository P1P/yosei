using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Lookable))]
[RequireComponent(typeof(Gaugeable))]

public class TileObject : MonoBehaviour
{
	protected Lookable m_lookable;
	protected Gaugeable m_gaugeable;

	protected Tile m_tile;

	public void Start()
	{
		m_lookable = GetComponent<Lookable>();
		m_gaugeable = GetComponent<Gaugeable>();
	}
}
