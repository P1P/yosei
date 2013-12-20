using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Brewer : MonoBehaviour
{
    public bool m_start_simulation;

    public List<TileSpawn> m_lst_spawns;
    public List<TileGoal> m_lst_goals;

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
        }
    }
}
