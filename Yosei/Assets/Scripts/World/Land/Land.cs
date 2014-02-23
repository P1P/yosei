using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Land : MonoBehaviour
{
    #region SINGLETON
    private static Land _instance = null;
    public static Land Instance { get { return _instance; } }
    #endregion

    public delegate System.Type Landgen(int p_x, int p_z, System.Random p_random);

    public Vector3 _tile_size
    {
        get;
        private set;
    }

    public List<Chunk> Chunks { get; private set; }

    private int _land_width;
    private int _land_depth;

    public void Awake()
    {
        _instance = this;
        Chunks = new List<Chunk>();
    }
    
    public Chunk CreateChunkAt(Vector3 p_position, int p_seed, int p_width, int p_depth, Landgen p_landgen, Vector3? p_tile_size = null)
    {
        if (p_tile_size.HasValue)
        {
            _tile_size = p_tile_size.Value;
        }
        else
        {
            _tile_size = Vector3.one;
        }

        // Initializing the Chunk
        GameObject gameobject_chunk = new GameObject("Chunk");
        gameobject_chunk.transform.parent = transform;
        gameobject_chunk.transform.position = transform.position;
        Chunk chunk = gameobject_chunk.AddComponent<Chunk>();

        chunk.Initialize(p_position, p_seed, p_width, p_depth, p_landgen, _tile_size);

        Chunks.Add(chunk);

        return chunk;
    }

    public void DestroyChunk(Chunk p_chunk)
    {
        Chunks.Remove(p_chunk);
        Destroy(p_chunk.gameObject);
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

    public System.Type LandgenRaceChallenge(int p_x, int p_z, System.Random p_random)
    {
        if (p_x == 0 && p_z == 2)
        {
            return typeof(TileSpawn);
        }
        if (p_x == 9 && p_z == 2)
        {
            return typeof(TileGoal);
        }

        return typeof(TileGrass);
    }
}
