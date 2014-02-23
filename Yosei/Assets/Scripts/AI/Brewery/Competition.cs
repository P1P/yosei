using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Teacup.Genetic;

public abstract class Competition : MonoBehaviour
{
    protected List<Challenge> _lst_challenges;

    public bool IsRunning { get; protected set; }

    public void Awake()
    {
        _lst_challenges = new List<Challenge>();
    }

    abstract public void Initialize(Population<decimal> p_population);

    protected void EndCompetition()
    {
        foreach (Challenge challenge in _lst_challenges)
        {
            challenge.Dispose();
        }

        _lst_challenges.Clear();

        IsRunning = false;
    }
}
