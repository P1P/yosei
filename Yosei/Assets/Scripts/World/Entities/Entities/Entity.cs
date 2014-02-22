using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Lookable))]
[RequireComponent(typeof(Gaugeable))]

public abstract class Entity : MonoBehaviour
{
    public Lookable Lookable { get; private set; }
    public Gaugeable Gaugeable { get; private set; }

	public void Awake()
	{
		Lookable = GetComponent<Lookable>();
		Gaugeable = GetComponent<Gaugeable>();
	}

    public string ToString()
    {
        return Lookable.Description;
    }
}
