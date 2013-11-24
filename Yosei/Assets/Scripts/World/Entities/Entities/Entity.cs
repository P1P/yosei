using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Lookable))]
[RequireComponent(typeof(Gaugeable))]

public abstract class Entity : MonoBehaviour
{
	public Lookable m_lookable;
    public Gaugeable m_gaugeable;
    public GroundlingMonoped m_groundling;

	public void Awake()
	{
		m_lookable = GetComponent<Lookable>();
		m_gaugeable = GetComponent<Gaugeable>();
        m_groundling = GetComponent<GroundlingMonoped>();
	}

    public string ToString()
    {
        return m_lookable.m_appearance;
    }
}
