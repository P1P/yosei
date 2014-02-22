using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Land : MonoBehaviour
{
    #region SINGLETON
    private static Land _instance = null;
    public static Land Instance { get { return _instance; } }

    public void Awake()
    {
        _instance = this;
    }
    #endregion

    public delegate System.Type Landgen(int p_x, int p_z, System.Random p_random);

    public Vector3 _tile_size
    {
        get;
        private set;
    }

    public List<List<Tile>> Matrix_tiles { get; private set; }

    private int _land_width;
    private int _land_depth;
    
    public void InitializeLand(int p_seed, int p_width, int p_depth, Landgen p_landgen, Vector3? p_tile_size = null)
    {
        _tile_size = p_tile_size.HasValue ? p_tile_size.Value : Vector3.one;

        // Cleans up the pre-existing tiles
        foreach (Transform tile in transform.GetComponentInChildren<Transform>())
        {
            GameObject.Destroy(tile.gameObject);
        }

        System.Random random = new System.Random(p_seed);
        _land_width = p_width;
        _land_depth = p_depth;

        Matrix_tiles = new List<List<Tile>>();

        GameObject line_go;

        // Uses the delegate landgen method to generate tiles
        for (int z = 0; z < _land_depth; ++z)
        {
            Matrix_tiles.Add(new List<Tile>());

            line_go = new GameObject("Line " + z);
            line_go.transform.parent = this.transform;

            for (int x = 0; x < _land_width; ++x)
            {
                GameObject tile_go = new GameObject(x + " / " + z + " ");

                tile_go.transform.position = new Vector3(x * _tile_size.x, 0, z * _tile_size.z);
                tile_go.transform.localScale = _tile_size;

                Matrix_tiles[z].Add(tile_go.AddComponent(p_landgen(x, z, random)) as Tile);

                tile_go.transform.parent = line_go.transform;
            }
        }
    }

    /// <summary>
    /// Updates the navigation graph around the position provided, in dimensions equals to a tile's
    /// </summary>
    /// <param name="p_pos">The position around which to update the graph</param>
    public void UpdateGraphAtTile(Vector3 p_pos)
    {
        AstarPath.Instance.UpdateGraphs(new Bounds(p_pos, _tile_size));
    }

    // Land generation delegates
    public System.Type LandgenRandom(int p_x, int p_z, System.Random p_random)
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

    public System.Type LandgenCurveLanes(int p_x, int p_z, System.Random p_random)
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
            
            if (p_x == _land_width - 1)
            {
                return typeof(TileGoal);
            }

            return typeof(TileLava);
        }

        return typeof(TileGrass);
    }
}
