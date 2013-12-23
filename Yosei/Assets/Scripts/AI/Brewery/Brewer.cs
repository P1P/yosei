using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Brewer : MonoBehaviour
{
    public bool m_start_simulation;

    public List<TileSpawn> m_lst_spawns;
    public List<TileGoal> m_lst_goals;
	public List<Tuple<TileSpawn, TileGoal>> m_lst_tuples_spawn_goal;

    public void Update()
    {
		if (m_start_simulation)
		{
			m_start_simulation = false;

			m_lst_spawns = new List<TileSpawn>();
			m_lst_goals = new List<TileGoal>();

			foreach (Tile tile in Game.Inst.m_map.m_matrix_tiles.SelectMany<List<Tile>, Tile>(k => k))
			{
				if (tile is TileSpawn)
				{
					m_lst_spawns.Add(tile as TileSpawn);
				}
				else if (tile is TileGoal)
				{
					m_lst_goals.Add(tile as TileGoal);
				}
			}

			// Associating every spawn to a goal
			// Assumes that every spawn are away from their goal by a constant distance

			// Getting shortest distance between a goal and a spawn

			float min_distance = float.MaxValue;

			if (m_lst_spawns.Count > 0 && m_lst_goals.Count > 0)
			{
				foreach (TileGoal goal in m_lst_goals)
				{
					float distance = Vector3.Distance(m_lst_spawns[0].transform.position, goal.transform.position);
					if (distance < min_distance)
					{
						min_distance = distance;
					}
				}
			}

			m_lst_tuples_spawn_goal = new List<Tuple<TileSpawn, TileGoal>>();

			// Finding couples
			foreach (TileSpawn spawn in m_lst_spawns)
			{
				foreach (TileGoal goal in m_lst_goals)
				{
					if (Vector3.Distance(spawn.transform.position, goal.transform.position) == min_distance)
					{
						m_lst_tuples_spawn_goal.Add(new Tuple<TileSpawn, TileGoal>(spawn, goal));
						break;
					}
				}
			}

			// Instantiating Yosei at spawns and asking them to reach their respective goals
			foreach (Tuple<TileSpawn, TileGoal> tuple in m_lst_tuples_spawn_goal)
			{
				Yosei.InstantiateYosei(tuple.Item1.transform.position).m_pathfinder.GoTo(tuple.Item2.transform.position);
			}
		}
    }
}
