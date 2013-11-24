using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Lookable))]
[RequireComponent(typeof(Gaugeable))]
[RequireComponent(typeof(Walkability))]

public abstract class Tile : MonoBehaviour
{
    public Lookable m_lookable { get; protected set; }
    public Gaugeable m_gaugeable { get; protected set; }
    public Walkability m_walkability { get; protected set; }

    protected MeshFilter m_mesh_filter;

    public void Awake()
    {
        m_lookable = GetComponent<Lookable>();
        m_gaugeable = GetComponent<Gaugeable>();
        m_walkability = GetComponent<Walkability>();

        m_mesh_filter = GetComponent<MeshFilter>();
    }

    public string ToString()
    {
        return m_lookable.m_appearance;
    }
}
