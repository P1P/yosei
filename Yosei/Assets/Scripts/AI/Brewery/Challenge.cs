using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Teacup.Genetic;

public class Challenge
{
    private int _current_position;
    private Stopwatch _stopwatch;
    private Population<decimal> _population;
    private List<Tuple<TileSpawn, TileGoal>> _lst_tuples_spawn_goal;
    private bool _ended;

    public Challenge(Population<decimal> p_population)
    {
        _lst_tuples_spawn_goal = GetSpawnGoalTuples();
        _population = p_population;

        _ended = false;
        _stopwatch = new Stopwatch();
        _stopwatch.Start();

        // Resetting the goal triggers
        foreach (Tuple<TileSpawn, TileGoal> tuple in _lst_tuples_spawn_goal)
        {
            tuple.Item_2.ResetReached();
        }

        // Instantiating Yosei and launching them
        for (int i = 0; i < _lst_tuples_spawn_goal.Count; ++i)
        {
            Yosei.InstantiateYosei(_lst_tuples_spawn_goal[i].Item_1.transform.position, Quaternion.identity,
                p_population.GetGenome(i)).Pathfinder.GoTo(_lst_tuples_spawn_goal[i].Item_2.transform.position);
        }
    }

    /// <summary>
    /// Upon a Yosei reaching its goal, assigns fitness and checks for the end of the current challenge
    /// </summary>
    /// <param name="p_yosei">The Yosei to reach the goal</param>
    public void ReachedGoal(Yosei p_yosei)
    {
        System.TimeSpan timespan = _stopwatch.Elapsed;
        Console.Instance.WriteLine("Congratulations to " + p_yosei.ToString() + " for reaching position " + (_current_position + 1) + " in " + timespan.ToString(), p_yosei.Lookable.Base_color);

        // Evaluate the Yosei
        p_yosei.Genome.m_fitness = GetCurrentFitnessReward();

        _current_position++;

        // Ends the challenge when everyone finished
        if (_current_position == _population.GetGenomeCount())
        {
            foreach (Yosei yosei in ReferenceHelper.Instance.Object_population.GetComponentsInChildren<Yosei>())
            {
                GameObject.Destroy(yosei.gameObject);
            }

            _ended = true;
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
        foreach (Tile tile in Map.Instance.m_matrix_tiles.SelectMany<List<Tile>, Tile>(k => k))
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
        return _population.GetGenomeCount() - _current_position + (_current_position == 0 ? 10 : 0);
    }

    public bool HasEnded()
    {
        return _ended;
    }
}
