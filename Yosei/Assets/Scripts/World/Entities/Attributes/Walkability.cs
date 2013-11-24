using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider))]

public class Walkability : MonoBehaviour
{
    public BoxCollider m_box_collider;

	void Awake()
    {
        m_box_collider = GetComponent<BoxCollider>();
        m_box_collider.size = Vector3.one;
	}

    public void SetWalkable(bool p_walkable)
    {
        gameObject.layer = p_walkable ? Game.Inst.m_layer_helper.m_ground_layer : Game.Inst.m_layer_helper.m_obstacle_layer;
    }
}
