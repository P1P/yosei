using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

using Teacup.Genetic;

public class Brewer : MonoBehaviour
{
	private Stopwatch m_stopwatch;
	private int m_current_position;
	private Population<decimal> m_population;

    private bool m_first = true;

	public void Awake()
	{
		m_stopwatch = new Stopwatch();
	}

    public void Update()
    {
        if (m_first)
        {
            m_first = false;
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
        m_stopwatch.Reset();
        m_stopwatch.Start();
        m_current_position = 0;

        List<Tuple<TileSpawn, TileGoal>> lst_tuples_spawn_goal = GetSpawnGoalTuples();

        if (m_population == null)
        {
            CreatePopulation(lst_tuples_spawn_goal.Count);
        }
        else
        {
            EvolvePopulation();
        }

        LaunchChallenge(lst_tuples_spawn_goal);
    }

    /// <summary>
    /// Instantiates Yosei at spawns and setting them to reach their respective goals
    /// </summary>
    /// <param name="p_lst_tuples_spawn_goal">The list of goal/spawn tuples</param>
    private void LaunchChallenge(List<Tuple<TileSpawn, TileGoal>> p_lst_tuples_spawn_goal)
    {
        // Resetting the goal triggers
        foreach (Tuple<TileSpawn, TileGoal> tuple in p_lst_tuples_spawn_goal)
        {
            tuple.Item2.ResetReached();
        }

        // Instantiating Yosei and launching them
        for (int i = 0; i < p_lst_tuples_spawn_goal.Count; ++i)
        {
            Yosei.InstantiateYosei(p_lst_tuples_spawn_goal[i].Item1.transform.position, Quaternion.identity,
                m_population.GetGenome(i)).m_pathfinder.GoTo(p_lst_tuples_spawn_goal[i].Item2.transform.position);
        }
    }

    /// <summary>
    /// Creates a new population of random individuals
    /// </summary>
    /// <param name="p_population_size">The number of individuals in the population</param>
    private void CreatePopulation(int p_population_size)
    {
			m_population = new Population<decimal>();

			GeneticOperatorRules rules = new GeneticOperatorRules(0.7, MUTATION_TYPE.DELTA, 0.1, 0.1m, 0m, 1m);

			for (int i = 0; i < p_population_size; ++i)
			{
				Chromosome<decimal> chr_1 = new Chromosome<decimal>("Movement", 1, rules);

				Genome<decimal> genome = new Genome<decimal>(chr_1);

				m_population.AddGenome(genome);
			}
    }

    /// <summary>
    /// Advances the current population by one lineage
    /// </summary>
	private void EvolvePopulation()
	{
		m_population = m_population.GetChildren(m_population.SelectRoulette(Fitness));
	}

    /// <summary>
    /// Upon a Yosei reaching its goal, assigns fitness and checks for the end of the current challenge
    /// </summary>
    /// <param name="p_yosei">The Yosei to reach the goal</param>
    public void ReachedGoal(Yosei p_yosei)
    {
        System.TimeSpan timespan = m_stopwatch.Elapsed;
        Game.Inst.m_console.WriteLine("Congratulations to " + p_yosei.ToString() + " for reaching position " + (m_current_position + 1) + " in " + timespan.ToString(), p_yosei.m_lookable.m_base_color);

        p_yosei.m_genome.m_fitness = GetCurrentFitnessReward();

        m_current_position++;

        if (m_current_position == m_population.GetGenomeCount())
        {
            foreach (Yosei yosei in Game.Inst.m_object_population.GetComponentsInChildren<Yosei>())
            {
                GameObject.Destroy(yosei.gameObject);
            }

            ChallengePopulation();
        }
    }

    /// <summary>
    /// Associates every spawn to a goal
    /// Assuming that every spawn are away from their goal by a constant distance (e.g. lane pattern)
    /// </summary>
    /// <returns></returns>
	private List<Tuple<TileSpawn, TileGoal>> GetSpawnGoalTuples()
	{
		List<TileSpawn> lst_spawns = new List<TileSpawn>();
		List<TileGoal> lst_goals = new List<TileGoal>();

        // Retrieving spawns and goals
		foreach (Tile tile in Game.Inst.m_map.m_matrix_tiles.SelectMany<List<Tile>, Tile>(k => k))
		{
			if (tile is TileSpawn)
			{
				lst_spawns.Add(tile as TileSpawn);
			}
			else if (tile is TileGoal)
			{
				lst_goals.Add(tile as TileGoal);
			}
		}

		// Getting shortest distance between a goal and a spawn
		float min_distance = float.MaxValue;

		if (lst_spawns.Count > 0 && lst_goals.Count > 0)
		{
			foreach (TileGoal goal in lst_goals)
			{
				float distance = Vector3.Distance(lst_spawns[0].transform.position, goal.transform.position);
				if (distance < min_distance)
				{
					min_distance = distance;
				}
			}
		}

		List<Tuple<TileSpawn, TileGoal>> lst_tuples_spawn_goal = new List<Tuple<TileSpawn, TileGoal>>();

		// Finding couples with said distance
		foreach (TileSpawn spawn in lst_spawns)
		{
			foreach (TileGoal goal in lst_goals)
			{
				if (Vector3.Distance(spawn.transform.position, goal.transform.position) == min_distance)
				{
					lst_tuples_spawn_goal.Add(new Tuple<TileSpawn, TileGoal>(spawn, goal));
					break;
				}
			}
		}

		return lst_tuples_spawn_goal;
	}

    /// <summary>
    /// Returns the fitness value for the current position
    /// </summary>
    /// <returns>The fitness for the current position</returns>
    private decimal GetCurrentFitnessReward()
    {
        return m_population.GetGenomeCount() - m_current_position + (m_current_position == 0 ? 10 : 0);
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
