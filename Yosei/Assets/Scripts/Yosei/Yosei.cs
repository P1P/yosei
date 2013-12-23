using UnityEngine;
using System.Collections;

using Teacup.Genetic;

[RequireComponent(typeof(GroundlingMonoped))]
[RequireComponent(typeof(Hiker))]
[RequireComponent(typeof(Pathfinder))]
[RequireComponent(typeof(BannerHolder))]

public class Yosei : Entity
{
	public Pathfinder m_pathfinder;
	public Genome<decimal> m_genome;

    private string m_name;

    private BannerHolder m_bannerholder;

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

		m_pathfinder.speed = (float)m_genome.GetChromosome("Movement").GetGene(0) * 250f + 100f;
    }

    public void Update()
    {
        WriteToBanner();

		if (m_groundling.m_tile_under is TileGoal)
		{
			if ((m_groundling.m_tile_under as TileGoal).FirstReached())
			{
				Game.Inst.m_brewer.ReachedGoal(this);
			}
		}
    }

	public string ToString()
	{
		return m_name + " (" + m_lookable.m_appearance + ")";
	}

    private void WriteToBanner()
    {
        m_bannerholder.SetTitleText(ToString());

        m_bannerholder.SetCoreColor(Game.Inst.m_colors.HighlightColor(m_lookable.m_base_color));

        int color = 0;
        m_bannerholder.ClearCoreText();
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

		m_bannerholder.AddCoreTextLine("My genome is " + m_genome.ToString());
		m_bannerholder.AddCoreText("Fitness " + m_genome.m_fitness);
    }

	public static Yosei InstantiateYosei(Vector3 p_position, Quaternion p_rotation, Genome<decimal> p_genome)
	{
		GameObject go = new GameObject("Yosei");
		go.transform.position = p_position + Vector3.up;
		go.transform.rotation = p_rotation;
		go.transform.parent = Game.Inst.m_object_population.transform;

		go.AddComponent<Seeker>();
		go.AddComponent<CharacterController>();

		Yosei yosei = go.AddComponent<Yosei>();

		yosei.m_genome = p_genome;

		return yosei;
	}
}
