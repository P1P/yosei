using UnityEngine;
using System.Collections;

public class TileGoal : Tile {
	private bool _reached = false;

	public void Start()
    {
        Lookable.SetAppearance(
            "Goal",
            "Tiles/Material/Material",
            TextureFactory.Instance.GetRandomGrayscaleTexture(2, 2, 0.9f, 0.0f),
            "Tiles/Mesh/Cube",
            ColorFactory.Instance.GetColor(0.7f, 0f));

        Walkability.SetWalkable(true);

        transform.localScale = new Vector3(1f, 1f, 1f);
	}

	public bool FirstReached()
	{
		if (_reached == false)
		{
			_reached = true;
			return true;
		}

		return false;
	}

	public void ResetReached()
	{
		_reached = false;
	}
}
