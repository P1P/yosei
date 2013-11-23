using UnityEngine;
using System.Collections;

public class TileGrass : Tile {
	void Start ()
    {
        m_lookable.SetAppearance(
            "Grass",
            "Tiles/Material/Material",
            Game.Inst.m_textures.GetRandomGreyscaleTexture(2, 2, 0.9f, 0.01f),
            "Tiles/Mesh/Cube",
            Game.Inst.m_colors.GetColor(0.35f, 0.02f));
	}
	
	void Update ()
    {
	
	}
}
