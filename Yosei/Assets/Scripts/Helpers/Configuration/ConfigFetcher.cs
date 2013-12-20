using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ConfigurationFetcher
{
    private IniFile m_ini;

    public ConfigurationFetcher(string filename)
    {
        m_ini = new IniFile(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\..\\..\\" + filename);
    }

    public void WriteValue(string section, string key, object value)
    {
        m_ini.IniWriteValue(section, key, value.ToString());
    }

    public int GetInt(string section, string key)
    {
        int res = 0;

        if (int.TryParse(m_ini.IniReadValue(section, key), out res))
        {
            return res;
        }
        else
        {
            return (int)float.Parse(m_ini.IniReadValue(section, key));
        }
    }

    public float GetFloat(string section, string key)
    {
        float res = 0;

        if (float.TryParse(m_ini.IniReadValue(section, key), out res))
        {
            return res;
        }
        else
        {
            return (float)int.Parse(m_ini.IniReadValue(section, key));
        }
    }

    public bool GetBool(string section, string key)
    {
        return bool.Parse(m_ini.IniReadValue(section, key));
    }

    public string GetString(string section, string key)
    {
        return m_ini.IniReadValue(section, key);
    }
}
