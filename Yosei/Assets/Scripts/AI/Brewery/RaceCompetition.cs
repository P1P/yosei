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

    override public void Initialize(Population<decimal> p_population)
    {
        IsRunning = true;
        _current_position = 0;
        _stopwatch = new Stopwatch();
        _stopwatch.Start();
        _population = p_population;

        // Initializing the challenges
        for (int i = 0; i < p_population.GetGenomeCount(); ++i)
        {
            RaceChallenge race_challenge = new RaceChallenge(
                Vector3.zero + Vector3.forward * i * 3f,
                _population.GetGenome(i),
                3);

            race_challenge.SubscribeComplete(ReachedGoal);
            _lst_challenges.Add(race_challenge);
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
            EndCompetition();
        }
    }

    /// <summary>
    /// Returns the fitness value for the current position
    /// </summary>
    /// <returns>The fitness for the current position</returns>
    private decimal GetCurrentFitnessReward()
    {
        return _population.GetGenomeCount() - _current_position;
    }
}
