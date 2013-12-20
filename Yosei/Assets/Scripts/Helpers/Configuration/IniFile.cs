﻿using System;
using System.Runtime.InteropServices;
using System.Text;

public class IniFile
{
    public string path;

    [DllImport("kernel32")]
    private static extern long WritePrivateProfileString(string section,
            string key, string val, string filePath);
    [DllImport("kernel32")]
    private static extern int GetPrivateProfileString(string section,
            string key, string def, StringBuilder retVal,
            int size, string filePath);

    public IniFile(string INIPath)
    {
        path = INIPath;
    }

    public void IniWriteValue(string section, string key, string value)
    {
        WritePrivateProfileString(section, key, value, this.path);
    }

    public string IniReadValue(string section, string key)
    {
        StringBuilder temp = new StringBuilder(255);
        GetPrivateProfileString(section, key, "", temp,
                                255, this.path);

        Game.Inst.m_console.WriteLine("[" + section + "] " + key + " " + temp.ToString());

        return temp.ToString();
    }
}