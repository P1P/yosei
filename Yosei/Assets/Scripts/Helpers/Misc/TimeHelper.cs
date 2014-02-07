using UnityEngine;
using System.Collections.Generic;

public class TimeHelper : MonoBehaviour
{
    #region SINGLETON
    private static TimeHelper _instance = null;
    public static TimeHelper Instance { get { return _instance; } }

    void Awake()
    {
        _instance = this;
    }
    #endregion

    private List<EventTimer> _lst_timers = new List<EventTimer>();

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
}
