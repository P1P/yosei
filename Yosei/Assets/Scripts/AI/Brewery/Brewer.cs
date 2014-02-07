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

    public Challenge Current_challenge { get; private set; }

	private Population<decimal> _population;

    public void Update()
    {
        if (Current_challenge == null || Current_challenge.HasEnded())
        {
            ChallengePopulation();
        }
    }

    /// <summary>
    /// Advances the simulation by one step
    /// On the first step, creates the first population and starts a challenge
    /// On the next steps, evolves to a new lineage and starts a new challenge
    /// </summary>
    private void ChallengePopulation()
    {
        if (_population == null)
        {
            CreatePopulation(6);
        }
        else
        {
            EvolvePopulation();
        }

        Current_challenge = new Challenge(_population);
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

			Genome<decimal> genome = new Genome<decimal>(chr_1);

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
    /// The fitness delegate, based on the fitness on the genome
    /// </summary>
    /// <param name="p_genome"></param>
    /// <returns></returns>
    private decimal Fitness(Genome<decimal> p_genome)
    {
        return p_genome.m_fitness;
    }
}
