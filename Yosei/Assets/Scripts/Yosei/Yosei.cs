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

    public string m_name;

    public void Awake()
    {
        m_pathfinder = GetComponent<Pathfinder>();
        m_bannerholder = GetComponent<BannerHolder>();

        base.Awake();
    }

    public void Start()
    {
        m_name = Game.Inst.m_names.GiveMeAName();

        m_lookable.SetAppearance(
            "Yosei",
            "Tiles/Material/Material",
            Game.Inst.m_textures.GetRandomGreyscaleTexture(1, 1, 0.9f, 0.1f),
            "Tiles/Mesh/Capsule",
            Game.Inst.m_colors.GetRandomColor());

        transform.localScale = Vector3.one * 0.25f;
    }

    public void Update()
    {
        WriteToBanner();
    }

    private void WriteToBanner()
    {
        m_bannerholder.SetTitleText(m_name + " the " + m_lookable.m_appearance);

        m_bannerholder.SetCoreColor(Game.Inst.m_colors.HighlightColor(m_lookable.m_base_color));

        int color = 0;
        m_bannerholder.ClearCoreText();
        m_bannerholder.AddCoreTextLine("Why hello there!", Game.Inst.m_colors.GetBaseColor(color++));
        m_bannerholder.AddCoreText("My name is ");
        m_bannerholder.AddCoreTextLine(m_name, m_lookable.m_base_color);
        m_bannerholder.AddCoreText("I'm going ");

        if (m_pathfinder.path != null)
        {
            m_bannerholder.AddCoreTextLine("to " + m_pathfinder.path.vectorPath[m_pathfinder.path.vectorPath.Count - 1].ToString(), Game.Inst.m_colors.GetBaseColor(color++));
        }
        else
        {
            m_bannerholder.AddCoreTextLine("Nowhere!", Game.Inst.m_colors.GetBaseColor(color++));
        }

    }
}
