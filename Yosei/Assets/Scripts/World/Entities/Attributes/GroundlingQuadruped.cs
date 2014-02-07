using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider))]

public class GroundlingQuadruped : MonoBehaviour
{
    private List<Vector3> _lst_extremities;
    private Bounds _bounds;
    private BoxCollider _box_collider;

    public List<Tile> Lst_tiles_under_proportion_type { get; private set; }
    public List<float> Lst_tiles_under_proportion_freq { get; private set; }

	public void Awake()
    {
        _box_collider = GetComponent<BoxCollider>();
        _box_collider.size = Vector3.one;

        _lst_extremities = new List<Vector3>(new Vector3[] { Vector3.one, Vector3.one, Vector3.one, Vector3.one });
        Lst_tiles_under_proportion_type = new List<Tile>();
        Lst_tiles_under_proportion_freq = new List<float>();

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
        _bounds = _box_collider.bounds;

        for (int i = 0; i < _lst_extremities.Count; ++i)
        {
            _lst_extremities[i] = new Vector3( // max max; min max; max min; min min
                i % 2 == 0 ? _bounds.max.x : _bounds.min.x,
                _bounds.center.y,
                i < _lst_extremities.Count / 2 ? _bounds.max.z : _bounds.min.z);
        }
    }

    // The groundling may be, at a given point, walking over several types of ground at the same time
    // This updates a List of Tuples<Tile, proportion percentage>, with only one Tuple per type of Tile
    private void UpdateGroundUnderProportions()
    {
        Lst_tiles_under_proportion_freq.Clear();
        Lst_tiles_under_proportion_type.Clear();

        RaycastHit hit_info;
        int nb_tiles = 0;

        // Probing for ground tiles under
        foreach (Vector3 extremity in _lst_extremities)
        {
            Physics.Raycast(extremity, -Vector3.up, out hit_info, 100f, LayerHelper.Instance.Ground_mask);

            if (hit_info.distance != 0f)
            {
                // Found a Tile!
                nb_tiles++;
                Tile tile = hit_info.collider.gameObject.GetComponent<Tile>();

                bool found = false;
                // Either adding a +1 to the flagship Tile of this type
                for (int i = 0; i < Lst_tiles_under_proportion_type.Count; ++i)
                {
                    if (tile.GetType() == Lst_tiles_under_proportion_type[i].GetType())
                    {
                        found = true;
                        Lst_tiles_under_proportion_freq[i] += 1f;
                        break;
                    }
                }

                // Or becoming the flagship for this type of Tile
                if (found == false)
                {
                    Lst_tiles_under_proportion_type.Add(tile);
                    Lst_tiles_under_proportion_freq.Add(1f);
                }
            }

            hit_info.distance = 0f; // Resetting the RaycastHit distance to easily detect a no-collision next loop
        }

        // Normalizing so that frequency is in percentage
        for (int i = 0; i < Lst_tiles_under_proportion_freq.Count; ++i)
        {
            Lst_tiles_under_proportion_freq[i] /= nb_tiles;
        }
    }
}
