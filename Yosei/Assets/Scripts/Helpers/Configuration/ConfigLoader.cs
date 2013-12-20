using UnityEngine;
using System.Collections;

public class ConfigLoader : MonoBehaviour
{
    public string m_filename;

    private ConfigurationFetcher m_fetcher;

    public void Start()
    {
        m_fetcher = new ConfigurationFetcher(m_filename);
    }
}
