using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GroundlingMonoped))]
[RequireComponent(typeof(Hiker))]
[RequireComponent(typeof(Pathfinder))]

public class Yosei : Entity
{
    public Pathfinder m_pathfinder;

    public void Awake()
    {
        m_pathfinder = GetComponent<Pathfinder>();

        base.Awake();
    }

    public void Start()
    {
        m_lookable.SetAppearance(
            "Yosei",
            "Tiles/Material/Material",
            Game.Inst.m_textures.GetRandomGreyscaleTexture(1, 1, 0.9f, 0.1f),
            "Tiles/Mesh/Capsule",
            Game.Inst.m_colors.GetRandomColor());

        transform.localScale = Vector3.one * 0.5f;
    }
}
