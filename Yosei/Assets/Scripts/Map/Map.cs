using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {
    public List<List<Tile>> m_matrix_tiles;

    public int m_width;
    public int m_depth;

    public float m_x_size;
    public float m_z_size;

    public int m_ground_layer;

	void Start ()
    {
        InitializeMap();
	}

    /// <summary>
    /// Initializes the Map with Tiles
    /// </summary>
    /// <param name="p_tile"></param>
    private void InitializeMap()
    {
        m_matrix_tiles = new List<List<Tile>>();

        GameObject line_go;

        for (int z = 0; z < m_depth; ++z)
        {
            m_matrix_tiles.Add(new List<Tile>());

            line_go = new GameObject("Line " + z);
            line_go.transform.parent = this.transform;

            for (int x = 0; x < m_width; ++x)
            {
                GameObject tile_go = new GameObject(x + " / " + z + " ");

                tile_go.layer = m_ground_layer;
                tile_go.transform.position = new Vector3(x * m_x_size, 0, z * m_z_size);
                tile_go.transform.localScale = new Vector3(m_x_size, 1f, m_z_size);

                if (Random.value < 0.75f)
                {
                    tile_go.AddComponent<TileGrass>();
                }
                else
                {
                    tile_go.AddComponent<TileLava>();
                }

                m_matrix_tiles[z].Add(tile_go.GetComponent<Tile>());

                tile_go.transform.parent = line_go.transform;
            }
        }
    }

    void Update()
    {
	
	}
}
