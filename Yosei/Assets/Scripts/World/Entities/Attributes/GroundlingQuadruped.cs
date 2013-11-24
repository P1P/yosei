using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider))]

public class GroundlingQuadruped : MonoBehaviour
{
    public List<Vector3> m_lst_extremities;
    public Bounds m_bounds;
    public BoxCollider m_box_collider;

    public List<Tile> m_lst_tiles_under_proportion_type = new List<Tile>();
    public List<float> m_lst_tiles_under_proportion_freq = new List<float>();

	void Awake()
    {
        m_box_collider = GetComponent<BoxCollider>();
        m_box_collider.size = Vector3.one;
        m_lst_extremities = new List<Vector3>(new Vector3[] { Vector3.one, Vector3.one, Vector3.one, Vector3.one });
        UpdateBounding();
	}

    public void FixedUpdate()
    {
        if (transform.hasChanged)
        {
            transform.hasChanged = false;
            UpdateBounding();

            UpdateGroundUnderProportions();
        }
    }

    private void UpdateBounding()
    {
        m_bounds = m_box_collider.bounds;

        for (int i = 0; i < m_lst_extremities.Count; ++i)
        {
            m_lst_extremities[i] = new Vector3( // max max; min max; max min; min min
                i % 2 == 0 ? m_bounds.max.x : m_bounds.min.x,
                m_bounds.center.y,
                i < m_lst_extremities.Count / 2 ? m_bounds.max.z : m_bounds.min.z);
        }
    }

    // The groundling may be, at a given point, walking over several types of ground at the same time
    // This updates a List of Tuples<Tile, proportion percentage>, with only one Tuple per type of Tile
    public void UpdateGroundUnderProportions()
    {
        m_lst_tiles_under_proportion_freq.Clear();
        m_lst_tiles_under_proportion_type.Clear();

        RaycastHit hit_info;
        int nb_tiles = 0;

        // Probing for ground tiles under
        foreach (Vector3 extremity in m_lst_extremities)
        {
            Physics.Raycast(extremity, -Vector3.up, out hit_info, 100f, Game.Inst.m_layer_helper.m_ground_mask);

            if (hit_info.distance != 0f)
            {
                // Found a Tile!
                nb_tiles++;
                Tile tile = hit_info.collider.gameObject.GetComponent<Tile>();

                bool found = false;
                // Either adding a +1 to the flagship Tile of this type
                for (int i = 0; i < m_lst_tiles_under_proportion_type.Count; ++i)
                {
                    if (tile.GetType() == m_lst_tiles_under_proportion_type[i].GetType())
                    {
                        found = true;
                        m_lst_tiles_under_proportion_freq[i] += 1f;
                        break;
                    }
                }

                // Or becoming the flagship for this type of Tile
                if (found == false)
                {
                    m_lst_tiles_under_proportion_type.Add(tile);
                    m_lst_tiles_under_proportion_freq.Add(1f);
                }
            }

            hit_info.distance = 0f; // Resetting the RaycastHit distance to easily detect a no-collision next loop
        }

        // Normalizing so that frequency is in percentage
        for (int i = 0; i < m_lst_tiles_under_proportion_freq.Count; ++i)
        {
            m_lst_tiles_under_proportion_freq[i] /= nb_tiles;
        }
    }
}
