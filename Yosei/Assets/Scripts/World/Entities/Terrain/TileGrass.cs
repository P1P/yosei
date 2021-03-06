﻿using UnityEngine;
using System.Collections;

public class TileGrass : Tile {
	public void Start()
    {
        Lookable.SetAppearance(
            "Grass",
            "Tiles/Material/Material",
            TextureFactory.Instance.GetDummyTexture(),
            "Tiles/Mesh/Cube",
            ColorFactory.Instance.GetColor(0.35f, 0.02f, 0.85f, 0.01f));

        Walkability.SetWalkable(true);

        transform.localScale = new Vector3(1f, 1f, 1f);
	}
}
