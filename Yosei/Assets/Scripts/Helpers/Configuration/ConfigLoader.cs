using UnityEngine;
using System.Collections;

public class ConfigLoader : MonoBehaviour
{
    #region SINGLETON
    private static ConfigLoader _instance = null;
    public static ConfigLoader Instance { get { return _instance; } }

    void Awake()
    {
        _instance = this;
    }
    #endregion

    public string _filename;

    private ConfigurationFetcher _fetcher;

    public void Start()
    {
        _fetcher = new ConfigurationFetcher(_filename);
    }
}
