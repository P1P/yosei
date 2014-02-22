using UnityEngine;
using System.Collections;

public class TileGoal : Tile {
    public Goal Goal
    {
        get;
        private set;
    }

	private bool _reached = false;

    public void Awake()
    {
        // Creating the child goal entity gameobject
        GameObject gameobject_goal = new GameObject("Goal");
        gameobject_goal.transform.parent = transform;
        gameobject_goal.transform.position = transform.position + Vector3.Scale(Land.Instance._tile_size, Vector3.up);
        Goal = gameobject_goal.AddComponent<Goal>();

        base.Awake();
    }

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
}
