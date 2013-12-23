using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Seeker))]

public class Hiker : MonoBehaviour
{
    public CharacterController m_character_controller;

	public void Awake()
    {
        m_character_controller = GetComponent<CharacterController>();
	}
}
