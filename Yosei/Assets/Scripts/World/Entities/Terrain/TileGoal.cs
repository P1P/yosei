using UnityEngine;
using System.Collections;

public class TileGoal : Tile {
	private bool m_reached = false;

	public void Start()
    {
        m_lookable.SetAppearance(
            "Goal",
            "Tiles/Material/Material",
            Game.Inst.m_textures.GetRandomGreyscaleTexture(2, 2, 0.9f, 0.0f),
            "Tiles/Mesh/Cube",
            Game.Inst.m_colors.GetColor(0.7f, 0f));

        m_walkability.SetWalkable(true);

        transform.localScale = new Vector3(1f, 1f, 1f);
	}

	public bool FirstReached()
	{
		if (m_reached == false)
		{
			m_reached = true;
			return true;
		}

		return false;
	}

	public void ResetReached()
	{
		m_reached = false;
	}
}
