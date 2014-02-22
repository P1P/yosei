using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Teacup.Genetic;

public class RaceCompetition : Competition
{
    private int _current_position;

    private Stopwatch _stopwatch;
    private Population<decimal> _population;
    private List<Tuple<TileSpawn, TileGoal>> _lst_tuples_spawn_goal;

    override public void Initialize(Population<decimal> p_population)
    {
        // Creating the competition land
        Land.Instance.InitializeLand(0, 9, 25, Land.Instance.LandgenCurveLanes);

        IsRunning = true;

        _lst_tuples_spawn_goal = GetSpawnGoalTuples();
        _population = p_population;

        _current_position = 0;
        _stopwatch = new Stopwatch();
        _stopwatch.Start();

        _lst_challenges = new List<RaceChallenge>();

        // Initializing the (one-man) teams
        for (int i = 0; i < _lst_tuples_spawn_goal.Count; ++i)
        {
            GameObject gameobject_race_challenge = new GameObject("Race challenge");
            gameobject_race_challenge.transform.parent = transform;
            gameobject_race_challenge.transform.position = transform.position;
            RaceChallenge race_challenge = gameobject_race_challenge.AddComponent<RaceChallenge>();

            race_challenge.Initialize(_population.GetGenome(i), 6, _lst_tuples_spawn_goal[i].Item_1.transform.position, _lst_tuples_spawn_goal[i].Item_2.Goal);
            race_challenge.SubscribeComplete(ReachedGoal);
        }
    }

    /// <summary>
    /// Upon a Yosei reaching its goal, assigns fitness and checks for the end of the current competition
    /// </summary>
    /// <param name="p_yosei">The Yosei to reach the goal</param>
    public void ReachedGoal(RaceChallenge.ChallengeCompleteInfo p_info)
    {
        System.TimeSpan timespan = _stopwatch.Elapsed;
        Console.Line(
            "Congratulations to " + p_info.Team[0].ToString() +
            " for reaching position " + (_current_position + 1) +
            " in " + timespan.ToString(), p_info.Team[0].Lookable.Base_color);

        // Evaluate the Yosei
        foreach (Yosei yosei in p_info.Team)
        {
            yosei.Genome.m_fitness = GetCurrentFitnessReward();
        }

        _current_position++;

        // Ends the competition when everyone finished
        if (_current_position == _population.GetGenomeCount())
        {
            foreach (Yosei yosei in ReferenceHelper.Instance.Object_population.GetComponentsInChildren<Yosei>())
            {
                // Wait for the next frame since NGUI doesn't like objets being destroyed during physics steps
                GameObject.Destroy(yosei.gameObject, 0.0000001f);
            }

            IsRunning = false;
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
        foreach (Tile tile in Land.Instance.Matrix_tiles.SelectMany<List<Tile>, Tile>(k => k))
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
}
