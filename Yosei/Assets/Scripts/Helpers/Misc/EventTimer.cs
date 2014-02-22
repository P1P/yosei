using UnityEngine;
using System.Collections;
using System;

public class EventTimer
{
    public delegate void TickHandler(EventTimer p_timer, TickInfo p_tick_info);

    private event TickHandler _end_tick;
    private event TickHandler _start_tick;

    private float _goal_time;
    private float _current_time;
    private bool _enabled;

    public class TickInfo : EventArgs
    {
        public DateTime Time
        {
            get;
            private set;
        }

        public float Completion
        {
            get;
            private set;
        }

        public TickInfo(float p_completion)
        {
            Completion = p_completion;
            Time = DateTime.Now;
        }
    }

    /// <summary>
    /// Instantiates a Timer and starts updating it
    /// </summary>
    /// <param name="p_goal_time">The time in seconds ffor the timer to reach completion</param>
    /// <param name="p_start_completion">The starting completion ratio</param>
    /// <param name="p_enabled">Defines whether the timer starts updating or not</param>
    /// <returns>The Timer instance</returns>
    public static EventTimer CreateTimer(float p_goal_time, float p_start_completion = 0f, bool p_enabled = true)
    {
        EventTimer timer = new EventTimer(p_goal_time, p_start_completion, p_enabled);
        TimeHelper.Instance.AddTimer(timer);

        return timer;
    }

    private EventTimer(float p_goal_time, float p_start_completion, bool p_enabled)
    {
        _goal_time = p_goal_time;
        _current_time = _goal_time * p_start_completion;

        if (p_enabled)
        {
            Start(p_goal_time, p_start_completion);
        }
        else
        {
            _enabled = false;
        }
    }

    /// <summary>
    /// Enables the timer and fires a start event
    /// </summary>
    /// <param name="p_new_goal_time">The new goal time. If null or not specified, will not update previous goal time</param>
    /// <param name="p_start_ratio">The completion ratio the timer will start with</param>
    public void Start(float? p_new_goal_time = null, float p_start_ratio = 0f)
    {
        if (p_new_goal_time.HasValue)
        {
            _goal_time = p_new_goal_time.Value;
        }

        _enabled = true;
        _current_time = _goal_time * p_start_ratio;

        if (_start_tick != null)
        {
            TickInfo tick_info = new TickInfo(GetCompletion());
            _start_tick(this, tick_info);
        }
    }

    /// <summary>
    /// Updates the timer
    /// </summary>
    /// <param name="p_delta_time">The time increase</param>
    public void Roll(float p_delta_time)
    {
        if (_enabled)
        {
            _current_time = Mathf.Min(_goal_time, _current_time + p_delta_time);

            if (HasEnded() && _end_tick != null)
            {
                TickInfo tick_info = new TickInfo(GetCompletion());
                _end_tick(this, tick_info);
            }
        }
    }

    /// <summary>
    /// Returns true if the timer has reached completion
    /// </summary>
    /// <returns>True if the timer has reached completion, false otherwise</returns>
    public bool HasEnded()
    {
        return _current_time == _goal_time;
    }

    /// <summary>
    /// Returns the completion ratio of the timer
    /// </summary>
    /// <returns>The completion ratio</returns>
    public float GetCompletion()
    {
        return _current_time / _goal_time;
    }

    /// <summary>
    /// Adds a handler to the Timer Start event
    /// Triggered every time the Timer is started or restarted
    /// </summary>
    /// <param name="p_handler">The TickHandler handler to subscribe to the event</param>
    public void SubscribeStart(TickHandler p_handler)
    {
        _start_tick += p_handler;
    }

    /// <summary>
    /// Adds a handler to the Timer End event
    /// Triggered when the Timer reaches completion
    /// </summary>
    /// <param name="p_handler">The TickHandler handler to subscribe to the event</param>
    public void SubscribeEnd(TickHandler p_handler)
    {
        _end_tick += p_handler;
    }

    /// <summary>
    /// Unsubscribe from the Timer Start event
    /// </summary>
    /// <param name="p_handler">The TickHandler handler to unregister to the event</param>
    public void UnsubscribeStart(TickHandler p_handler)
    {
        _start_tick -= p_handler;
    }

    /// <summary>
    /// Unsubscribe from the Timer End event
    /// </summary>
    /// <param name="p_handler">The TickHandler handler to unregister to the event</param>
    public void UnsubscribeEnd(TickHandler p_handler)
    {
        _end_tick -= p_handler;
    }
}
