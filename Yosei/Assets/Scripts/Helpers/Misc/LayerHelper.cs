using UnityEngine;
using System.Collections;

public class LayerHelper : MonoBehaviour
{
    #region SINGLETON
    private static LayerHelper _instance = null;
    public static LayerHelper Instance { get { return _instance; } }

    void Awake()
    {
        _instance = this;
    }
    #endregion

    // Inspector-set values
    public int Ground_layer;
    public int Obstacle_layer;

    public LayerMask Ground_mask;
    public LayerMask Obstacle_mask;

    public LayerMask Tiles_mask;
}