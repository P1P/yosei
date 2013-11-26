using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GroundlingMonoped))]
[RequireComponent(typeof(Hiker))]
[RequireComponent(typeof(Pathfinder))]
[RequireComponent(typeof(BannerHolder))]

public class Yosei : Entity
{
    public Pathfinder m_pathfinder;
	private BannerHolder m_bannerholder;

    public void Awake()
    {
        m_pathfinder = GetComponent<Pathfinder>();
		m_bannerholder = GetComponent<BannerHolder>();

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

		m_bannerholder.SetCoreText("I'm a happy dude");
		m_bannerholder.SetTitleText("Why hello there");
    }

	public void Update()
	{
	}
}
