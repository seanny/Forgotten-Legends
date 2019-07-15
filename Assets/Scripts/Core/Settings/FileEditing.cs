//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//

using System.IO;
using UnityEngine;

namespace Core.Settings
{
    public static class FileEditing
    {
        public static void EditLine(int line, string text, string fileName)
        {
            if (!File.Exists(fileName))
            {
                Debug.LogWarning($"Ignoring {fileName} as it does not exist.");
                return;
            }
            string[] lineArray = File.ReadAllLines(fileName);
            lineArray[line] = text;
            File.WriteAllLines(fileName, lineArray);
        }

        public static void EditKey(string key, string value, string fileName)
        {
            if(!File.Exists(fileName))
            {
                Debug.LogWarning($"Ignoring {fileName} as it does not exist.");
                return;
            }
            string[] lineArray = File.ReadAllLines(fileName);
            for (int i = 0; i < lineArray.Length; i++)
            {
                if(lineArray[i].Contains("="))
                {
                    string propKey = lineArray[i].Substring(0, lineArray[i].LastIndexOf("=", System.StringComparison.OrdinalIgnoreCase));
                    if (propKey == key)
                    {
                        string lineString = $"{key}={value}";
                        EditLine(i, lineString, fileName);
                    }
                }
            }
        }

        public static string ReadKey(string key, string fileName)
        {
            if (!File.Exists(fileName))
            {
                Debug.LogWarning($"Ignoring {fileName} as it does not exist.");
                return null;
            }
            string[] lineArray = File.ReadAllLines(fileName);
            for (int i = 0; i < lineArray.Length; i++)
            {
                if (lineArray[i].Contains("="))
                {
                    string propKey = lineArray[i].Substring(0, lineArray[i].LastIndexOf("=", System.StringComparison.OrdinalIgnoreCase));
                    string propValue = lineArray[i].Substring(lineArray[i].LastIndexOf("=", System.StringComparison.OrdinalIgnoreCase));
                    if (propKey == key)
                    {
                        return propValue;
                    }
                }
            }
            return null;
        }
    }
}