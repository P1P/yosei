using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Seeker))]

public class Hiker : MonoBehaviour
{
    private CharacterController m_character_controller;

	void Start ()
    {
        m_character_controller = GetComponent<CharacterController>();
	}
	
	void Update ()
    {
	    
	}
}
