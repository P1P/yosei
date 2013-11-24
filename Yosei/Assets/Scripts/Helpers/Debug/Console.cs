using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Console : MonoBehaviour
{
    public int m_nb_lines;
    public int m_fixed_lines_timeout;

    public float m_console_height_pc;
    public float m_console_height_margin_pc;
    public float m_console_height_text_margin_pc;
    public float m_console_width_separation_pc;
    public float m_console_width_separation_margin_pc;
    public float m_console_width_text_margin_pc;

    public bool m_console_on;
    public bool m_background_on;

    public GUIStyle m_back_style;
	public GUIStyle m_label_style;

    private List<Line> m_lst_lines = new List<Line>();
    private List<Line> m_lst_fixed_lines = new List<Line>();
    private List<float> m_lst_fixed_lines_time = new List<float>();

    struct Line
    {
        public Line(string str, Color clr)
        {
            m_string = str;
            m_color = clr;
        }

        public Line(string str)
        {
            m_string = str;
            m_color = Color.white;
        }

        public string m_string;
        public Color m_color;
    }

    void Start()
    {
        for (int i = 0; i < m_nb_lines; ++i)
        {
            m_lst_fixed_lines.Add(new Line(""));
            m_lst_fixed_lines_time.Add(0);
        }
    }

    // Timing out the fixed console lines
    void Update()
    {
        for (int i = 0; i < m_nb_lines; ++i)
        {
            if (m_lst_fixed_lines_time[i] != -1) // -1 means no timeout
            {
                m_lst_fixed_lines_time[i] += Game.Inst.m_time_helper.GetStep();

                if (m_lst_fixed_lines_time[i] > m_fixed_lines_timeout)
                {
                    m_lst_fixed_lines[i] = new Line("");
                }
            }
        }
    }

    void OnGUI()
    {
        if (m_console_on)
        {
            // Left console (dynamic log)

            // Background
            if (m_background_on)
            {
                GUI.Box(new Rect(
                            0,
                            0,
                            Game.Inst.m_screen_helper.W_to_px(m_console_width_separation_pc - m_console_width_separation_margin_pc),
                            Game.Inst.m_screen_helper.H_to_px(m_console_height_pc + m_console_height_margin_pc)),
                        "",
                        m_back_style);
            }

            // Text
            for (int i = 0; i < m_lst_lines.Count; ++i)
            {
                m_label_style.normal.textColor = m_lst_lines[i].m_color;
                GUI.Label(new Rect(
                              Game.Inst.m_screen_helper.W_to_px(m_console_width_text_margin_pc),
                              i * (Game.Inst.m_screen_helper.H_to_px(m_console_height_pc + m_console_height_text_margin_pc) / m_nb_lines)
                              + (Game.Inst.m_screen_helper.H_to_px(m_console_height_text_margin_pc)),
                              Game.Inst.m_screen_helper.W_to_px(m_console_width_separation_pc - m_console_width_separation_margin_pc),
                              Game.Inst.m_screen_helper.H_to_px(m_console_height_pc / m_nb_lines)),
                          m_lst_lines[i].m_string,
                          m_label_style);
            }

            // Right console (static)

            // Background
            if (m_background_on)
            {
                GUI.Box(new Rect(
                            Game.Inst.m_screen_helper.W_to_px((m_console_width_separation_pc)),
                            0,
                            Game.Inst.m_screen_helper.W_to_px(1 - m_console_width_separation_pc),
                            Game.Inst.m_screen_helper.H_to_px(m_console_height_pc + m_console_height_margin_pc)),
                        "",
                        m_back_style);
            }

            // Text
            for (int i = 0; i < m_nb_lines; ++i)
            {
                m_label_style.normal.textColor = m_lst_fixed_lines[i].m_color;
                GUI.Label(new Rect(
                              Game.Inst.m_screen_helper.W_to_px(m_console_width_separation_pc + m_console_width_text_margin_pc),
                              i * (Game.Inst.m_screen_helper.H_to_px(m_console_height_pc + m_console_height_text_margin_pc) / m_nb_lines)
                              + (Game.Inst.m_screen_helper.H_to_px(m_console_height_text_margin_pc)),
                              Game.Inst.m_screen_helper.W_to_px(1 - m_console_width_separation_pc),
                              Game.Inst.m_screen_helper.H_to_px(m_console_height_pc / m_nb_lines)),
                          m_lst_fixed_lines[i].m_string,
                          m_label_style);
            }
        }
    }

    public void WriteLine(int line)
    {
        WriteLine(line.ToString());
    }

    public void WriteLine(float line)
    {
        WriteLine(line.ToString());
    }

    // Write a line to the dynamic log
    public void WriteLine(string line, Color color)
    {
        if (m_console_on)
        {
            if (m_lst_lines.Count >= m_nb_lines)
            {
                for (int i = 0; i < m_lst_lines.Count - 1; ++i)
                {
                    m_lst_lines[i] = m_lst_lines[i + 1];
                }

                m_lst_lines[m_nb_lines - 1] = new Line(line, color);
            }
            else
            {
                m_lst_lines.Add(new Line(line, color));
            }
        }
    }

	public void WriteLine(string line)
	{
		WriteLine(line, Color.white);
	}

	public void WriteLine(int line, int color)
	{
		WriteLine(line.ToString(), Game.Inst.m_colors.GetBaseColor(color));
	}

	public void WriteLine(float line, int color)
	{
        WriteLine(line.ToString(), Game.Inst.m_colors.GetBaseColor(color));
	}

	public void WriteLine(string line, int color)
	{
        WriteLine(line, Game.Inst.m_colors.GetBaseColor(color));
	}
	
    public void WriteFixedLine(int line, int id, bool time_limit = true)
    {
        WriteFixedLine(line.ToString(), id, time_limit);
    }

    public void WriteFixedLine(float line, int id, bool time_limit = true)
    {
        WriteFixedLine(line.ToString(), id, time_limit);
    }

    // Prints a line at the given position in the static console
    public void WriteFixedLine(string line, int id, Color color, bool time_limit = true)
    {
        if (m_console_on)
        {
            if (id < m_nb_lines)
            {
                m_lst_fixed_lines[id] = new Line(line, color);
                m_lst_fixed_lines_time[id] = time_limit ? 0 : -1;
            }
            else
            {
                WriteLine("Console.WriteFixedLine: Out of index fixed line write: " + id);
            }
        }
    }

    public void WriteFixedLine(string line, int id, bool time_limit = true)
    {
        WriteFixedLine(line, id, Color.white, time_limit);
    }

	public void WriteFixedLine(int line, int id, int color, bool time_limit = true)
	{
        WriteFixedLine(line.ToString(), id, Game.Inst.m_colors.GetBaseColor(color), time_limit);
	}

	public void WriteFixedLine(float line, int id, int color, bool time_limit = true)
	{
        WriteFixedLine(line.ToString(), id, Game.Inst.m_colors.GetBaseColor(color), time_limit);
	}

	public void WriteFixedLine(string line, int id, int color, bool time_limit = true)
	{
        WriteFixedLine(line, id, Game.Inst.m_colors.GetBaseColor(color), time_limit);
	}
}
