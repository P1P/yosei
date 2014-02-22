using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Teacup.Genetic;

public abstract class Competition : MonoBehaviour
{
    protected List<RaceChallenge> _lst_challenges;

    public bool IsRunning { get; protected set; }

    abstract public void Initialize(Population<decimal> p_population);
}
