using UnityEngine;
using System.Collections;

public class GroundlingMonoped : MonoBehaviour
{
    public Tile Tile_under { get; private set; }

    public void FixedUpdate()
    {
        if (transform.hasChanged)
        {
            transform.hasChanged = false;
            UpdateGroundUnder();
        }
    }

    private void UpdateGroundUnder()
    {
        RaycastHit hit_info;
        Physics.Raycast(transform.position, -Vector3.up, out hit_info, 100f, LayerHelper.Instance.Ground_mask);

        if (hit_info.distance != 0f)
        {
            // Found a Tile!
            Tile_under = hit_info.collider.gameObject.GetComponent<Tile>();
        }
        else
        {
            Tile_under = null;
        }
    }
}
