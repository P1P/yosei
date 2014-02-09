using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Teacup.Genetic;

public abstract class Challenge : MonoBehaviour
{
    public bool IsRunning { get; protected set; }

    abstract public void Initialize(Population<decimal> p_population);
}
