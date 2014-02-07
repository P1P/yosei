using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {
    public List<List<Tile>> m_matrix_tiles;

    public int m_width;
    public int m_depth;

    public float m_x_size;
    public float m_z_size;

    public Bounds m_bounds;

    private bool m_require_graph_update = false;

    public delegate System.Type MapGen(int p_x, int p_z, System.Random p_random);

	public void Start()
    {
        InitializeMap(0, 5, MapgenCurveLanes);
	}

    /// <summary>
    /// Initializes the Map with Tiles
    /// </summary>
    /// <param name="p_tile"></param>
    public void InitializeMap(int p_seed, int p_size, MapGen p_mapgen)
    {
        System.Random random = new System.Random(p_seed);

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

                tile_go.transform.position = new Vector3(x * m_x_size, 0, z * m_z_size);
                tile_go.transform.localScale = new Vector3(m_x_size, 1f, m_z_size);

                m_matrix_tiles[z].Add(tile_go.AddComponent(p_mapgen(x, z, random)) as Tile);

                tile_go.transform.parent = line_go.transform;
            }
        }

        m_require_graph_update = true;
    }

    void Update()
    {
        if (m_require_graph_update)
        {
            m_require_graph_update = false;
            UpdateGraph();
        }
	}

    private void UpdateGraph()
    {
        Game.Inst.m_pathfinder.UpdateGraphs(new Bounds(
                new Vector3(m_width * m_x_size / 2f, 0f, m_depth * m_z_size / 2f),
                new Vector3(m_width * m_x_size, 5f, m_depth * m_z_size)));
    }

    // Map generation delegates

    public System.Type MapgenRandom(int p_x, int p_z, System.Random p_random)
    {
        if (p_random.NextDouble() < 0.5)
        {
            return typeof(TileGrass);
        }
        else
        {
            return typeof(TileLava);
        }
    }

    public System.Type MapgenCurveLanes(int p_x, int p_z, System.Random p_random)
    {
        if (p_z % 4 == 0)
        {
            return typeof(TileLava);
        }

        if ((p_x + 2 * p_z) % 4 == 0)
        {
            if (p_x == 0)
            {
                return typeof(TileSpawn);
            }
            
            if (p_x == m_width - 1)
            {
                return typeof(TileGoal);
            }

            return typeof(TileLava);
        }

        return typeof(TileGrass);
    }
}
