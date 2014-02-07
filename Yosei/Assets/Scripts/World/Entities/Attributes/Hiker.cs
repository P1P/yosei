using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Seeker))]

public class Hiker : MonoBehaviour
{
    private CharacterController _character_controller;

	public void Awake()
    {
        _character_controller = GetComponent<CharacterController>();
	}
}
