using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider))]

public class Walkable : MonoBehaviour
{
    public BoxCollider m_box_collider;

	void Awake()
    {
        m_box_collider = GetComponent<BoxCollider>();
        m_box_collider.size = Vector3.one;

        gameObject.layer = Game.Inst.m_layer_helper.m_ground_layer;
	}
}
