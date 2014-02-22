using UnityEngine;
using System.Collections.Generic;

public class RaceChallenge : Challenge
{
    private Goal _goal;

    /// <summary>
    /// Memorizes the team of Yosei assigned to this Challenge
    /// and gets ready to be notified of one of them reaching the Goal
    /// </summary>
    /// <param name="p_yosei_team">The Team of Yosei cooperating in this Challenge</param>
    /// <param name="p_goal">The Goal the Team has to reach</param>
    public void InitializeForTeam(List<Yosei> p_yosei_team, Goal p_goal)
    {
        _yosei_team = p_yosei_team;
        _goal = p_goal;

        p_goal.ActivateForTeam(_yosei_team, GoalReachedHandler);

        Invoke("OrderYosei", 0.25f);
    }

    /// <summary>
    /// Memorizes the Yosei assigned to this Challenge
    /// and gets ready to be notified of it reaching the Goal
    /// </summary>
    /// <param name="p_yosei">The Yosei taking part in this Challenge</param>
    /// <param name="p_goal">The Goal that it has to reach</param>
    public void InitializeForYosei(Yosei p_yosei, Goal p_goal)
    {
        List<Yosei> yosei_team = new List<Yosei>();
        yosei_team.Add(p_yosei);
        InitializeForTeam(yosei_team, p_goal);        
    }

    private void OrderYosei()
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
