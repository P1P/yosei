﻿using UnityEngine;
using System.Collections;

public class TileLava : Tile {
	public void Start()
    {
        Lookable.SetAppearance(
            "Lava",
            "Tiles/Material/Material",
            TextureFactory.Instance.GetRandomGrayscaleTexture(2, 2, 0.9f, 0.025f),
            "Tiles/Mesh/Cube",
            ColorFactory.Instance.GetColor(0.01f, 0.02f));

        Walkability.SetWalkable(false);

        transform.localScale = new Vector3(1f, 2f, 1f);
	}
}
