using UnityEngine;
using System.Collections.Generic;

public class TimeHelper : MonoBehaviour
{
    #region SINGLETON
    private static TimeHelper _instance = null;
    public static TimeHelper Instance { get { return _instance; } }
    #endregion

    public bool Quitting
    {
        get;
        private set;
    }

    private List<EventTimer> _lst_timers = new List<EventTimer>();

    void Awake()
    {
        _instance = this;
        Quitting = false;
    }

    public void AddTimer(EventTimer _timer)
    {
        _lst_timers.Add(_timer);
    }

    public void Update()
    {
        foreach (EventTimer timer in _lst_timers)
        {
            timer.Roll(Time.deltaTime);
        }
    }


    public void OnApplicationQuit()
    {
        Quitting = true;
    }
}
