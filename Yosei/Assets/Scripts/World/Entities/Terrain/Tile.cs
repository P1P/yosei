﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Lookable))]
[RequireComponent(typeof(Gaugeable))]
[RequireComponent(typeof(Walkability))]

public abstract class Tile : MonoBehaviour
{
    public Lookable Lookable { get; protected set; }
    public Gaugeable Gaugeable { get; protected set; }
    public Walkability Walkability { get; protected set; }

    protected MeshFilter _mesh_filter;

    public void Awake()
    {
        Lookable = GetComponent<Lookable>();
        Gaugeable = GetComponent<Gaugeable>();
        Walkability = GetComponent<Walkability>();

        _mesh_filter = GetComponent<MeshFilter>();
    }

    public string ToString()
    {
        return Lookable.Appearance;
    }
}
