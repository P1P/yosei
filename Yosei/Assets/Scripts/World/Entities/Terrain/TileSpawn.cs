using UnityEngine;
using System.Collections;

public class TileSpawn : Tile {
	void Start ()
    {
        m_lookable.SetAppearance(
            "Spawn",
            "Tiles/Material/Material",
            Game.Inst.m_textures.GetRandomGreyscaleTexture(2, 2, 0.9f, 0.0f),
            "Tiles/Mesh/Cube",
            Game.Inst.m_colors.GetColor(0.9f, 0f));

        m_walkability.SetWalkable(true);

        transform.localScale = new Vector3(1f, 1f, 1f);
	}
}
