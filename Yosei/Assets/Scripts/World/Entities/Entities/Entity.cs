using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Lookable))]
[RequireComponent(typeof(Gaugeable))]
[RequireComponent(typeof(Groundling))]

public abstract class Entity : MonoBehaviour
{
	protected Lookable m_lookable;
	protected Gaugeable m_gaugeable;
    protected Groundling m_groundling;

	public void Awake()
	{
		m_lookable = GetComponent<Lookable>();
		m_gaugeable = GetComponent<Gaugeable>();
        m_groundling = GetComponent<Groundling>();
	}

    public string ToString()
    {
        return m_lookable.m_appearance;
    }
}
