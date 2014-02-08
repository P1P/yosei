using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Console : MonoBehaviour
{
    #region SINGLETON
    private static Console _instance = null;
    public static Console Instance { get { return _instance; } }

    void Awake()
    {
        _instance = this;
    }
    #endregion

    // Inspector-set values
    public int Nb_lines = 10;
    public int Fixed_lines_timeout = 2;

    public float Console_height_pc = 0.1f;
    public float Console_height_margin_pc = 0.02f;
    public float Console_height_text_margin_pc = 0.005f;
    public float Console_width_separation_pc = 0.5f;
    public float Console_width_separation_margin_pc = 0.01f;
    public float Console_width_text_margin_pc = 0.01f;

    public bool Console_on = true;
    public bool Background_on;

    public GUIStyle Back_style;
	public GUIStyle Label_style;

    public Color Default_color = Color.white;

    private List<TextLine> _lst_lines = new List<TextLine>();
    private List<TextLine> _lst_fixed_lines = new List<TextLine>();
    private List<float> _lst_fixed_lines_time = new List<float>();

    private struct TextLine
    {
        public string _string;
        public Color _color;

        public TextLine(string p_str, Color p_clr)
        {
            _string = p_str;
            _color = p_clr;
        }
    }

    public void Start()
    {
        for (int i = 0; i < Nb_lines; ++i)
        {
            _lst_fixed_lines.Add(new TextLine("", Default_color));
            _lst_fixed_lines_time.Add(0);
        }
    }

    // Timing out the fixed console lines
    public void Update()
    {
        for (int i = 0; i < Nb_lines; ++i)
        {
            if (_lst_fixed_lines_time[i] != -1) // -1 means no timeout
            {
                _lst_fixed_lines_time[i] += Time.deltaTime;

                if (_lst_fixed_lines_time[i] > Fixed_lines_timeout)
                {
                    _lst_fixed_lines[i] = new TextLine("", Default_color);
                }
            }
        }
    }

    public void OnGUI()
    {
        if (Console_on)
        {
            // Left console (dynamic log)

            // Background
            if (Background_on)
            {
                GUI.Box(new Rect(
                            0,
                            0,
                            ScreenHelper.W_to_px(Console_width_separation_pc - Console_width_separation_margin_pc),
                            ScreenHelper.H_to_px(Console_height_pc + Console_height_margin_pc)),
                        "",
                        Back_style);
            }

            // Text
            for (int i = 0; i < _lst_lines.Count; ++i)
            {
                Label_style.normal.textColor = _lst_lines[i]._color;
                GUI.Label(new Rect(
                              ScreenHelper.W_to_px(Console_width_text_margin_pc),
                              i * (ScreenHelper.H_to_px(Console_height_pc + Console_height_text_margin_pc) / Nb_lines)
                              + (ScreenHelper.H_to_px(Console_height_text_margin_pc)),
                              ScreenHelper.W_to_px(Console_width_separation_pc - Console_width_separation_margin_pc),
                              ScreenHelper.H_to_px(Console_height_pc / Nb_lines)),
                          _lst_lines[i]._string,
                          Label_style);
            }

            // Right console (static)

            // Background
            if (Background_on)
            {
                GUI.Box(new Rect(
                            ScreenHelper.W_to_px((Console_width_separation_pc)),
                            0,
                            ScreenHelper.W_to_px(1 - Console_width_separation_pc),
                            ScreenHelper.H_to_px(Console_height_pc + Console_height_margin_pc)),
                        "",
                        Back_style);
            }

            // Text
            for (int i = 0; i < Nb_lines; ++i)
            {
                Label_style.normal.textColor = _lst_fixed_lines[i]._color;
                GUI.Label(new Rect(
                              ScreenHelper.W_to_px(Console_width_separation_pc + Console_width_text_margin_pc),
                              i * (ScreenHelper.H_to_px(Console_height_pc + Console_height_text_margin_pc) / Nb_lines)
                              + (ScreenHelper.H_to_px(Console_height_text_margin_pc)),
                              ScreenHelper.W_to_px(1 - Console_width_separation_pc),
                              ScreenHelper.H_to_px(Console_height_pc / Nb_lines)),
                          _lst_fixed_lines[i]._string,
                          Label_style);
            }
        }
    }

    // Write a line to the dynamic log
    private Console WriteLine<T>(T p_line, Color? p_color = null)
    {
        if (Console_on)
        {
            if (_lst_lines.Count >= Nb_lines)
            {
                for (int i = 0; i < _lst_lines.Count - 1; ++i)
                {
                    _lst_lines[i] = _lst_lines[i + 1];
                }

                _lst_lines[Nb_lines - 1] = new TextLine(p_line.ToString(), p_color.HasValue ? p_color.Value : Default_color);
            }
            else
            {
                _lst_lines.Add(new TextLine(p_line.ToString(), p_color.HasValue ? p_color.Value : Default_color));
            }
        }

        return this;
    }

    private Console WriteLine<T>(T p_line, int p_color_index)
    {
        return WriteLine(p_line, ColorFactory.Instance.GetBaseColor(p_color_index));
    }

    // Prints a line at the given position in the static console
    private Console WriteFixedLine<T>(T p_line, int p_id, Color? p_color = null, bool p_time_limit = true)
    {
        if (Console_on)
        {
            if (p_id < Nb_lines)
            {
                _lst_fixed_lines[p_id] = new TextLine(p_line.ToString(), p_color.HasValue ? p_color.Value : Default_color);
                _lst_fixed_lines_time[p_id] = p_time_limit ? 0 : -1;
            }
            else
            {
                WriteLine("Console.WriteFixedLine: Out of index fixed line write: " + p_id);
            }
        }

        return this;
    }

    private Console WriteFixedLine<T>(T p_line, int p_id, int p_color_index, bool p_time_limit = true)
    {
        return WriteFixedLine(p_line, p_id, ColorFactory.Instance.GetBaseColor(p_color_index), p_time_limit);
    }

    // Quick access static methods
    public static Console Line<T>(T p_line, Color? p_color = null)
    {
        return Instance.WriteLine(p_line, p_color);
    }
    public static Console Line<T>(T p_line, int p_color_index)
    {
        return Instance.WriteLine(p_line, p_color_index);
    }
    
    public static Console FixedLine<T>(T p_line, int p_id, Color? p_color = null, bool p_time_limit = true)
    {
        return Instance.WriteFixedLine(p_line, p_id, p_color, p_time_limit);
    }

    public static Console FixedLine<T>(T p_line, int p_id, int p_color_index, bool p_time_limit = true)
    {
        return Instance.WriteFixedLine(p_line, p_id, p_color_index, p_time_limit);
    }
}
