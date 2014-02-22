using UnityEngine;
using System.Collections.Generic;

using Teacup.Genetic;

public class Brewer : MonoBehaviour
{
    #region SINGLETON
    private static Brewer _instance = null;
    public static Brewer Instance { get { return _instance; } }

    void Awake()
    {
        _instance = this;
    }
    #endregion

    public RaceCompetition Current_challenge { get; private set; }

	private Population<decimal> _population;

    public void Start()
    {
        GameObject go = new GameObject("Challenge");
        go.transform.parent = transform;
        go.transform.localPosition = Vector3.zero;
        go.transform.rotation = Quaternion.identity;

        Current_challenge = go.AddComponent<RaceCompetition>();
    }

    public void Update()
    {
        if (!Current_challenge.IsRunning)
        {
            ChallengePopulation(Current_challenge);
        }
    }

    /// <summary>
    /// Creates a simulation or advances the current simulation by one step
    /// On the first step, creates the first population and starts a challenge
    /// On the next steps, evolves to a new lineage and starts a new challenge
    /// </summary>
    /// <param name="p_challenge">The challenge the population will undergo</param>
    private void ChallengePopulation(RaceCompetition p_challenge)
    {
        if (_population == null)
        {
            CreatePopulation(6);
        }
        else
        {
            EvolvePopulation();
        }

        p_challenge.Initialize(_population);
    }

    /// <summary>
    /// Creates a new population of random individuals
    /// </summary>
    /// <param name="p_population_size">The number of individuals in the population</param>
    private void CreatePopulation(int p_population_size)
    {
		_population = new Population<decimal>();

        // Defining common genetic rules
		GeneticOperatorRules rules = new GeneticOperatorRules(0.7, MUTATION_TYPE.DELTA, 0.1, 0.1m, 0m, 1m);

        // Creating the genomes
		for (int i = 0; i < p_population_size; ++i)
		{
            Chromosome<decimal> chr_1 = new Chromosome<decimal>("Movement", 1, rules);
            Chromosome<decimal> chr_2 = new Chromosome<decimal>("Size", 1, rules);
            Chromosome<decimal> chr_3 = new Chromosome<decimal>("Appearance", 3, rules);

			Genome<decimal> genome = new Genome<decimal>(chr_1, chr_2, chr_3);

			_population.AddGenome(genome);
		}
    }

    /// <summary>
    /// Advances the current population by one lineage
    /// </summary>
	private void EvolvePopulation()
	{
		_population = _population.GetChildren(_population.SelectRoulette(Fitness));
	}

    /// <summary>
    /// The fitness evaluation delegate; based on the fitness on the genome
    /// </summary>
    /// <param name="p_genome">The genome to evaluate</param>
    /// <returns>The effective fitness of the genome</returns>
    private decimal Fitness(Genome<decimal> p_genome)
    {
        return p_genome.m_fitness;
    }
}
