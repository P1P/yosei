using UnityEngine;
using System.Collections.Generic;

using Teacup.Genetic;

public class RaceChallenge : Challenge
{
    private Goal _goal;

    /// <summary>
    /// Creates a Team of clones from the Genome to be tested
    /// and gets ready to be notified of one of them reaching the Goal
    /// </summary>
    /// <param name="p_position">The world position where the challenge land shall be created</param>
    /// <param name="p_genome">The Genome of every member of the Team</param>
    /// <param name="p_team_size">The number of clones in the Team</param>
    public RaceChallenge(Vector3 p_position, Genome<decimal> p_genome, int p_team_size)
    {
        // Creating the competition land chunk
        _chunk = Land.Instance.CreateChunkAt(p_position, 0, 10, 3, Land.Instance.LandgenRaceChallenge);

        TileSpawn tile_spawn = _chunk.FindUniqueTile<TileSpawn>();
        TileGoal tile_goal = _chunk.FindUniqueTile<TileGoal>();

        _yosei_team = new List<Yosei>();
        for (int i = 0; i < p_team_size; ++i)
        {
            Vector3 start_offset = Vector3.forward * (i - p_team_size / 2f) * 0.5f;

            Yosei yosei = Yosei.InstantiateYosei(tile_spawn.transform.position + start_offset, Quaternion.identity, p_genome);
            _yosei_team.Add(yosei);
        }

        _goal = tile_goal.Goal;

        _goal.ActivateForTeam(_yosei_team, GoalReachedHandler);

        LaunchTeam();
    }

    private void LaunchTeam()
    {
        foreach (Yosei yosei in _yosei_team)
        {
            yosei.Pathfinder.OrderGoTo(_goal.transform.position, 1f);
        }
    }

    /// <summary>
    /// Upon a Yosei reaching the Goal, disable the Goal
    /// And notify the Competition of the Challenge completion
    /// </summary>
    private void GoalReachedHandler()
    {
        _goal.Disable();

        CompleteChallenge(1f);
    }
}
