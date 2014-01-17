using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Teacup.Genetic;

public class Challenge
{
    private int m_current_position;
    private Stopwatch m_stopwatch;
    private Population<decimal> m_population;
    private List<Tuple<TileSpawn, TileGoal>> m_lst_tuples_spawn_goal;
    private bool m_ended;

    public Challenge(Population<decimal> p_population)
    {
        m_lst_tuples_spawn_goal = GetSpawnGoalTuples();
        m_population = p_population;

        m_ended = false;
        m_stopwatch = new Stopwatch();
        m_stopwatch.Start();

        // Resetting the goal triggers
        foreach (Tuple<TileSpawn, TileGoal> tuple in m_lst_tuples_spawn_goal)
        {
            tuple.Item2.ResetReached();
        }

        // Instantiating Yosei and launching them
        for (int i = 0; i < m_lst_tuples_spawn_goal.Count; ++i)
        {
            Yosei.InstantiateYosei(m_lst_tuples_spawn_goal[i].Item1.transform.position, Quaternion.identity,
                p_population.GetGenome(i)).m_pathfinder.GoTo(m_lst_tuples_spawn_goal[i].Item2.transform.position);
        }
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

            m_ended = true;
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

    public bool HasEnded()
    {
        return m_ended;
    }
}
