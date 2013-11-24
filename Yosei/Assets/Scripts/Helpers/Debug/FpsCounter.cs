using UnityEngine;
using System.Collections;

public class FpsCounter : MonoBehaviour
{
    public float m_update_interval = 0.5f;

    private float m_accum = 0; // FPS accumulated over the interval
    private int m_frames = 0;  // Frames drawn over the interval
    private float m_timeleft;  // Left time for current interval

    void Start()
    {
        m_timeleft = m_update_interval;
    }

    void Update()
    {
        m_timeleft -= Time.deltaTime;
        m_accum += Time.timeScale / Time.deltaTime;
        ++m_frames;

        // Interval ended - update GUI text and start new interval
        if (m_timeleft <= 0.0)
        {
            // display two fractional digits (f2 format)
            float fps = m_accum / m_frames;
            string format = System.String.Format("{0:F2} FPS", fps);

            int display_color;

            if (fps < 30)
            {
                display_color = 2;
            }
            else if (fps < 50)
            {
                display_color = 3;
            }
            else
            {
                display_color = 4;
            }

            Game.Inst.m_console.WriteFixedLine(format, 0, display_color, false);

            m_timeleft = m_update_interval;
            m_accum = 0.0F;
            m_frames = 0;
        }
    }
}