using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Land : MonoBehaviour
{
    #region SINGLETON
    private static Land _instance = null;
    public static Land Instance { get { return _instance; } }

    void Awake()
    {
        _instance = this;
    }
    #endregion

    public delegate System.Type Landgen(int p_x, int p_z, System.Random p_random);

    public List<List<Tile>> Matrix_tiles { get; private set; }

    private int _land_width;
    private int _land_depth;

    private float _tile_width = 1f;
    private float _tile_depth = 1f;
    
    public void InitializeLand(int p_seed, int p_width, int p_depth, Landgen p_landgen)
    {
        System.Random random = new System.Random(p_seed);
        _land_width = p_width;
        _land_depth = p_depth;

        Matrix_tiles = new List<List<Tile>>();

        GameObject line_go;

        for (int z = 0; z < _land_depth; ++z)
        {
            Matrix_tiles.Add(new List<Tile>());

            line_go = new GameObject("Line " + z);
            line_go.transform.parent = this.transform;

            for (int x = 0; x < _land_width; ++x)
            {
                GameObject tile_go = new GameObject(x + " / " + z + " ");

                tile_go.transform.position = new Vector3(x * _tile_width, 0, z * _tile_depth);
                tile_go.transform.localScale = new Vector3(_tile_width, 1f, _tile_depth);

                Matrix_tiles[z].Add(tile_go.AddComponent(p_landgen(x, z, random)) as Tile);

                tile_go.transform.parent = line_go.transform;
            }
        }
    }

    private void UpdateFullGraph()
    {
        AstarPath.Instance.UpdateGraphs(new Bounds(
                new Vector3(_land_width * _tile_width / 2f, 0f, _land_depth * _tile_depth / 2f),
                new Vector3(_land_width * _tile_width, 5f, _land_depth * _tile_depth)));
    }

    public void UpdateGraphAtTile(Vector3 p_pos)
    {
        AstarPath.Instance.UpdateGraphs(new Bounds(p_pos, new Vector3(_tile_width, 1f, _tile_depth)));
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
