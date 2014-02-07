using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider))]

public class Walkability : MonoBehaviour
{
    private BoxCollider _box_collider;

	public void Awake()
    {
        _box_collider = GetComponent<BoxCollider>();
        _box_collider.size = Vector3.one;
	}

    public void SetWalkable(bool p_walkable)
    {
        gameObject.layer = p_walkable ? LayerHelper.Instance.Ground_layer : LayerHelper.Instance.Obstacle_layer;
    }
}
