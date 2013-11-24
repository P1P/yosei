using UnityEngine;
using System.Collections;

public class LayerHelper : MonoBehaviour
{
    public int m_ground_layer;
    public int m_obstacle_layer;

    public LayerMask m_ground_mask;
    public LayerMask m_obstacle_mask;
}