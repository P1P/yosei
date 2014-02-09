using UnityEngine;
using System.Collections;
using Pathfinding;

public class Pathfinder : MonoBehaviour
{
    public Path Path { get; private set; }
    public float Speed { get; set; }

    private Seeker _seeker;
    private CharacterController _controller;

    private float _next_waypoint_distance = 0.4f;
    private int _current_waypoint = 0;

    public void Awake()
    {
        _seeker = GetComponent<Seeker>();
        _controller = GetComponent<CharacterController>();
    }

    public void GoTo(Vector3 p_target_position)
    {
        _seeker.StartPath(transform.position, p_target_position, OnPathComplete);
    }

    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            Path = p;

            //Reset the waypoint counter
            _current_waypoint = 0;
        }
    }

    public void FixedUpdate()
    {
        if (Path == null)
        {
            //We have no path to move after yet
            return;
        }
        if (_current_waypoint >= Path.vectorPath.Count)
        {
            return;
        }

        //Direction to the next waypoint
        Vector3 dir = (Path.vectorPath[_current_waypoint] - transform.position).normalized;
        dir *= Speed * Time.fixedDeltaTime;
        _controller.SimpleMove(dir);

        //Check if we are close enough to the next waypoint
        //If we are, proceed to follow the next waypoint
        if (Vector3.Distance(transform.position, Path.vectorPath[_current_waypoint]) < _next_waypoint_distance)
        {
            _current_waypoint++;
            return;
        }
    }
}
