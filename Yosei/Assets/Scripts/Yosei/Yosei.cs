using UnityEngine;
using System.Collections;

using Teacup.Genetic;

[RequireComponent(typeof(Pathfinder))]
[RequireComponent(typeof(BannerHolder))]
[RequireComponent(typeof(GroundlingMonoped))]

public class Yosei : Entity
{
    public Pathfinder Pathfinder { get; private set; }
    public GroundlingMonoped Groundling { get; private set; }
    public Genome<decimal> Genome { get; private set; }

    private string _name;
    private BannerHolder _bannerholder;

    public void Awake()
    {
        Pathfinder = GetComponent<Pathfinder>();
        Groundling = GetComponent<GroundlingMonoped>();
        _bannerholder = GetComponent<BannerHolder>();
        _bannerholder.BannerEnabled = false;

        base.Awake();
    }

    public void Start()
    {
        // Give the Yosei a name and appearance
        _name = NameFactory.Instance.GiveMeAName();


        // Express the genome
        Pathfinder.Speed = (float)Genome.GetChromosome("Movement").GetGene(0) * 250f + 100f;
        float size = (float)Genome.GetChromosome("Size").GetGene(0) * 0.15f + 0.1f;
        Color color = new Color(
            (float)Genome.GetChromosome("Appearance").GetGene(0),
            (float)Genome.GetChromosome("Appearance").GetGene(1),
            (float)Genome.GetChromosome("Appearance").GetGene(2));

        Lookable.SetAppearance(
            "Yosei",
            "Tiles/Material/Material",
            TextureFactory.Instance.GetRandomGrayscaleTexture(1, 1, 0.9f, 0.1f),
            "Tiles/Mesh/Capsule",
            color);

        transform.localScale = Vector3.one * size;
        
    }

    public void Update()
    {
        WriteToBanner();
    }

	public string ToString()
	{
		return _name + " (" + Lookable.Appearance + ")";
	}

    private void WriteToBanner()
    {
        if (_bannerholder.BannerEnabled)
        {
            // Display useful debug info on the banner
            _bannerholder.SetTitleText(ToString());

            _bannerholder.SetCoreColor(ColorFactory.Instance.HighlightColor(Lookable.Base_color));

            int color = 0;
            _bannerholder.ClearCoreText();
            _bannerholder.AddCoreText("My name is ");
            _bannerholder.AddCoreTextLine(_name, Lookable.Base_color);
            _bannerholder.AddCoreText("I'm going ");

            if (Pathfinder.Path != null)
            {
                _bannerholder.AddCoreTextLine("to " + Pathfinder.Path.vectorPath[Pathfinder.Path.vectorPath.Count - 1].ToString(), ColorFactory.Instance.GetBaseColor(color++));
            }
            else
            {
                _bannerholder.AddCoreTextLine("Nowhere!", ColorFactory.Instance.GetBaseColor(color++));
            }

            _bannerholder.AddCoreTextLine("My genome is " + Genome.ToString());
            _bannerholder.AddCoreText("Fitness " + Genome.m_fitness);
        }
    }

	public static Yosei InstantiateYosei(Vector3 p_position, Quaternion p_rotation, Genome<decimal> p_genome)
	{
        // Creates a Yosei and adds it to the population
		GameObject go = new GameObject("Yosei");
		go.transform.position = p_position + Vector3.up;
		go.transform.rotation = p_rotation;
		go.transform.parent = ReferenceHelper.Instance.Object_population.transform;

		go.AddComponent<Seeker>();
		go.AddComponent<CharacterController>();

		Yosei yosei = go.AddComponent<Yosei>();

		yosei.Genome = p_genome;

		return yosei;
	}
}
