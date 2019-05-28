using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace LeafSQL.UI
{
    public static class RegistryHelper
    {
        static public void SetRegistryString(string subPath, string valueName, string value)
        {
            ResourceManager rm = new ResourceManager(typeof(string));
            RegistryKey regKey = Registry.LocalMachine;
            RegistryKey regSubKey = regKey.OpenSubKey(Constants.RegistryKey + "\\" + subPath, true);
            regSubKey.SetValue(valueName, value);
        }
        static public string GetRegistryString(string subPath, string valueName)
        {
            return GetRegistryString(subPath, valueName, "");
        }

        static public string GetRegistryString(string subPath, string valueName, string Default)
        {
            ResourceManager rm = new ResourceManager(typeof(string));
            RegistryKey regKey = Registry.LocalMachine;
            RegistryKey regSubKey = regKey.OpenSubKey(Constants.RegistryKey + "\\" + subPath);

            object value = regSubKey.GetValue(valueName);

            if (value != null)
            {
                string stringValue = value.ToString();

                if (stringValue == null)
                {
                    return Default;
                }

                return stringValue;
            }

            return null;
        }

        static public void SetRegistryInt(string subPath, string valueName, int value)
        {
            ResourceManager rm = new ResourceManager(typeof(string));
            RegistryKey regKey = Registry.LocalMachine;
            RegistryKey regSubKey = regKey.OpenSubKey(Constants.RegistryKey + "\\" + subPath, true);
            regSubKey.SetValue(valueName, value);
        }

        static public int GetRegistryInt(string subPath, string valueName)
        {
            return GetRegistryInt(subPath, valueName, 0);
        }

        static public int GetRegistryInt(string subPath, string valueName, int Default)
        {
            ResourceManager rm = new ResourceManager(typeof(string));
            RegistryKey regKey = Registry.LocalMachine;
            RegistryKey regSubKey = regKey.OpenSubKey(Constants.RegistryKey + "\\" + subPath);

            Object value = regSubKey.GetValue(valueName);

            if (value == null)
            {
                return Default;
            }

            return int.Parse(value.ToString());
        }

        static public void SetRegistryBool(string subPath, string valueName, bool value)
        {
            SetRegistryInt(subPath, valueName, Convert.ToInt32(value));
        }

        static public bool GetRegistryBool(string subPath, string valueName)
        {
            return GetRegistryInt(subPath, valueName) != 0;
        }

        static public bool GetRegistryBool(string subPath, string valueName, bool Default)
        {
            return GetRegistryInt(subPath, valueName, Convert.ToInt32(Default)) != 0;
        }
    }
}

