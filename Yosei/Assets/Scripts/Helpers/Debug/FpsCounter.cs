using UnityEngine;
using System.Collections;

public class FpsCounter : MonoBehaviour
{
    private float _update_interval = 0.5f;

    private float _accumulated_fps;
    private int _accumulated_frames;
    private float _time_left;

    public void Start()
    {
        _time_left = _update_interval;
    }

    public void Update()
    {
        _time_left -= Time.deltaTime;
        _accumulated_fps += Time.timeScale / Time.deltaTime;
        ++_accumulated_frames;

        // Interval ended - update GUI text and start new interval
        if (_time_left <= 0.0)
        {
            // display two fractional digits (f2 format)
            float fps = _accumulated_fps / _accumulated_frames;
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

            Console.FixedLine(format, 0, display_color, false);

            _time_left = _update_interval;
            _accumulated_fps = 0f;
            _accumulated_frames = 0;
        }
    }
}