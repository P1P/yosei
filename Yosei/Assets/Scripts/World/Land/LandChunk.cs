using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Chunk : MonoBehaviour
{
    public List<List<Tile>> Matrix_tiles { get; private set; }
    
    public void Initialize(Vector3 p_position, int p_seed, int p_width, int p_depth, Land.Landgen p_landgen, Vector3 p_tile_size)
    {
        System.Random random = new System.Random(p_seed);

        Matrix_tiles = new List<List<Tile>>();

        GameObject line_go;

        // Uses the delegate landgen method to generate tiles
        for (int z = 0; z < p_depth; ++z)
        {
            Matrix_tiles.Add(new List<Tile>());

            line_go = new GameObject("Line " + z);
            line_go.transform.parent = this.transform;

            for (int x = 0; x < p_width; ++x)
            {
                GameObject tile_go = new GameObject(x + " / " + z + " ");

                tile_go.transform.position = new Vector3(x * p_tile_size.x, 0, z * p_tile_size.z) + p_position;
                tile_go.transform.localScale = p_tile_size;

                Matrix_tiles[z].Add(tile_go.AddComponent(p_landgen(x, z, random)) as Tile);

                tile_go.transform.parent = line_go.transform;
            }
        }
    }

    /// <summary>
    /// Returns the first Tile of type T found in this Chunk
    /// </summary>
    /// <typeparam name="T">The type of the selected Tile</typeparam>
    /// <returns></returns>
    public T FindUniqueTile<T>()
    {
        foreach (Tile tile in Matrix_tiles.SelectMany<List<Tile>, Tile>(k => k))
        {
            if (tile is T)
            {
                return (T)(object)tile;
            }
        }
        throw new System.Exception ("Couldn't find Tile of type " +  typeof(T).ToString() + " in Chunk " + gameObject);
    }
}
