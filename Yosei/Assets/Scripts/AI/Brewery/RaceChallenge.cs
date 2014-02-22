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
    /// <param name="p_genome">The Genome of every member of the Team</param>
    /// <param name="p_team_size">The number of clones in the Team</param>
    /// <param name="p_start">The Start position</param>
    /// <param name="p_goal">The Goal the Team has to reach</param>
    public void Initialize(Genome<decimal> p_genome, int p_team_size, Vector3 p_start, Goal p_goal)
    {
        _yosei_team = new List<Yosei>();
        for (int i = 0; i < p_team_size; ++i)
        {
            Vector3 start_offset = Vector3.forward * (i - p_team_size / 2f) * 0.25f;

            Yosei yosei = Yosei.InstantiateYosei(p_start + start_offset, Quaternion.identity, p_genome);
            _yosei_team.Add(yosei);
        }

        _goal = p_goal;

        p_goal.ActivateForTeam(_yosei_team, GoalReachedHandler);

        Invoke("LaunchTeam", 0.25f);
    }

    private void LaunchTeam()
    {
        foreach (Yosei yosei in _yosei_team)
        {
            yosei.Pathfinder.GoTo(_goal.transform.position);
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
