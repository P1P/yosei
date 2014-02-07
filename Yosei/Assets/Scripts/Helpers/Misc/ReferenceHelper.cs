using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReferenceHelper : MonoBehaviour
{
    #region SINGLETON
    private static ReferenceHelper _instance;
    public static ReferenceHelper Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("ReferenceHelper").AddComponent<ReferenceHelper>();
                _instance.transform.parent = GameObject.Find("Helper").transform;
                _instance.Initialize();
            }
            return _instance;
        }
    }

    public void OnApplicationQuit()
    {
        _instance = null;
    }

    public void DestroyInstance()
    {
        _instance = null;
    }
    #endregion SINGLETON

    public GameObject Object_population { get; private set; }
    public GameObject Object_observer { get; private set; }

    public void Initialize()
    {
        // Finding GameObjects
		Object_population = GameObject.Find("Population");
		Object_observer = GameObject.Find("Observer");
    }
}
