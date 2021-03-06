﻿using System;
using System.Runtime.InteropServices;
using System.Text;

public class IniFile
{
    private string _path;

    [DllImport("kernel32")]
    private static extern long WritePrivateProfileString(string section,
            string key, string val, string filePath);
    [DllImport("kernel32")]
    private static extern int GetPrivateProfileString(string section,
            string key, string def, StringBuilder retVal,
            int size, string filePath);

    public IniFile(string INIPath)
    {
        _path = INIPath;
    }

    public void IniWriteValue(string section, string key, string value)
    {
        WritePrivateProfileString(section, key, value, this._path);
    }

    public string IniReadValue(string section, string key)
    {
        StringBuilder temp = new StringBuilder(255);
        GetPrivateProfileString(section, key, "", temp,
                                255, this._path);

        return temp.ToString();
    }
}