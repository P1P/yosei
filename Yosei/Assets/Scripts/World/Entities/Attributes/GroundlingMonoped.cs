using UnityEngine;
using System.Collections;

public class GroundlingMonoped : MonoBehaviour
{
    public Tile m_tile_under;

    void Awake()
    {

    }

    public void FixedUpdate()
    {
        if (transform.hasChanged)
        {
            transform.hasChanged = false;
            UpdateGroundUnder();
        }
    }

    public void UpdateGroundUnder()
    {
        RaycastHit hit_info;
        Physics.Raycast(transform.position, -Vector3.up, out hit_info, 100f, Game.Inst.m_layer_helper.m_ground_mask);

        if (hit_info.distance != 0f)
        {
            // Found a Tile!
            m_tile_under = hit_info.collider.gameObject.GetComponent<Tile>();
        }
        else
        {
            m_tile_under = null;
        }
    }

    public Tile GetTileUnder()
    {
        return m_tile_under;
    }
}
