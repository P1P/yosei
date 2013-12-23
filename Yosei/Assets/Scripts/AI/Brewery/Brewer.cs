using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

using Teacup.Genetic;

public class Brewer : MonoBehaviour
{
    public bool m_start_simulation;

	private Stopwatch m_stopwatch;
	private int m_position;
	private Population<decimal> m_population;

	public void Awake()
	{
		m_stopwatch = new Stopwatch();
	}

    public void Update()
    {
		if (m_start_simulation)
		{
			m_start_simulation = false;
			m_stopwatch.Reset();
			m_stopwatch.Start();
			m_position = 0;

			List<Tuple<TileSpawn, TileGoal>> lst_tuples_spawn_goal = GetSpawnGoalTuples();

			EvolvePopulation(lst_tuples_spawn_goal.Count);

			LaunchSimulation(lst_tuples_spawn_goal);
		}
    }

	private void EvolvePopulation(int p_nb_genomes)
	{
		if (m_population == null)
		{
			m_population = new Population<decimal>();

			GeneticOperatorRules rules = new GeneticOperatorRules(0.7, MUTATION_TYPE.DELTA, 0.1, 0.1m, 0m, 1m);

			for (int i = 0; i < p_nb_genomes; ++i)
			{
				Chromosome<decimal> chr_1 = new Chromosome<decimal>("Movement", 1, rules);

				Genome<decimal> genome = new Genome<decimal>(chr_1);

				m_population.AddGenome(genome);
			}
		}
		else
		{
			m_population = m_population.GetChildren(m_population.SelectRoulette(Fitness));
		}
	}

	private decimal Fitness(Genome<decimal> p_genome)
	{
		return p_genome.m_fitness;
	}

	public void ReachedGoal(Yosei p_yosei)
	{
		System.TimeSpan timespan = m_stopwatch.Elapsed;
		Game.Inst.m_console.WriteLine("Congratulations to " + p_yosei.ToString() + " for reaching position " + (m_position + 1) + " in " + timespan.ToString(), p_yosei.m_lookable.m_base_color);
		p_yosei.m_genome.m_fitness = m_population.GetGenomeCount() - m_position + (m_position == 0 ? 10 : 0);
		m_position++;

		if (m_position == m_population.GetGenomeCount())
		{
			foreach (Yosei yosei in Game.Inst.m_object_population.GetComponentsInChildren<Yosei>())
			{
				GameObject.Destroy(yosei.gameObject);
			}

			m_start_simulation = true;
		}
	}

	private List<Tuple<TileSpawn, TileGoal>> GetSpawnGoalTuples()
	{
		// Associating every spawn to a goal
		// Assumes that every spawn are away from their goal by a constant distance

		List<TileSpawn> lst_spawns = new List<TileSpawn>();
		List<TileGoal> lst_goals = new List<TileGoal>();

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

	private void LaunchSimulation(List<Tuple<TileSpawn, TileGoal>> p_lst_tuples_spawn_goal)
	{
		foreach (Tuple<TileSpawn, TileGoal> tuple in p_lst_tuples_spawn_goal)
		{
			tuple.Item2.ResetReached();
		}

		// Instantiating Yosei at spawns and asking them to reach their respective goals
		for (int i = 0; i < p_lst_tuples_spawn_goal.Count; ++i)
		{
			Yosei.InstantiateYosei(p_lst_tuples_spawn_goal[i].Item1.transform.position, Quaternion.identity,
				m_population.GetGenome(i)).m_pathfinder.GoTo(p_lst_tuples_spawn_goal[i].Item2.transform.position);
		}
	}
}
