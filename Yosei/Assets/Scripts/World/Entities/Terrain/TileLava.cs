using UnityEngine;
using System.Collections;

public class TileLava : Tile {
	void Start ()
    {
        m_lookable.SetAppearance(
            "Lava",
            "Tiles/Material/Material",
            Game.Inst.m_textures.GetRandomGreyscaleTexture(2, 2, 0.9f, 0.025f),
            "Tiles/Mesh/Cube",
            Game.Inst.m_colors.GetColor(0.01f, 0.02f));

        m_walkability.SetWalkable(false);

        transform.localScale = new Vector3(1f, 2f, 1f);
	}
}
