using UnityEngine;
using System.Collections;

public class TileSpawn : Tile {
	public void Start()
    {
        Lookable.SetAppearance(
            "Spawn",
            "Tiles/Material/Material",
            TextureFactory.Instance.GetRandomGrayscaleTexture(2, 2, 0.9f, 0.0f),
            "Tiles/Mesh/Cube",
            ColorFactory.Instance.GetColor(0.9f, 0f));

        Walkability.SetWalkable(true);

        transform.localScale = new Vector3(1f, 1f, 1f);
	}
}
