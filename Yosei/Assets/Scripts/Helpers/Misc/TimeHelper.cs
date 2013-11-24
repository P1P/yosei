using UnityEngine;
using System.Collections;

public class TimeHelper : MonoBehaviour
{
    public int m_target_fps;
    public float m_base_time_scale;

    private float m_time_since_last_mark;
    private float m_framecount_timescaled;
    private float m_step;
    private float m_big_step;

    void Start()
    {
        Application.targetFrameRate = m_target_fps;
        Time.timeScale = m_base_time_scale;
    }

    void Update()
    {
        m_step = Time.timeScale == 0 ? 0 : Time.deltaTime * 60f;
        m_framecount_timescaled += m_step;
        m_time_since_last_mark += Time.deltaTime;
    }

    public int GetScaledFrameCount()
    {
        return (int)m_framecount_timescaled;
    }

    public int GetNonScaledFrameCount()
    {
        return Time.frameCount;
    }

    public float GetStep()
    {
        return m_step;
    }

    public void SetSlowRate(float rate)
    {
        Time.timeScale = rate;
    }

    public void TogglePause()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = m_base_time_scale;
            Game.Inst.m_console.WriteLine("Unpausing");
        }
        else
        {
            Time.timeScale = 0f;
            Game.Inst.m_console.WriteLine("Pausing");
        }
    }
}
