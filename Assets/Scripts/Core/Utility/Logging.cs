using System;
using System.IO;
using Core.Settings;
using UnityEngine;

namespace Core.Utility
{
    public static class Logging
    {
        private static string m_LogFile;
        private static bool m_Enabled;

        private enum LogType
        {
            Debug,
            Warning,
            Error
        };

        private static void WriteToLog(LogType logType, string log)
        {
            if (GameSettings.Instance.GetProperty("bEngineLogEnabled") == "True")
            {
                m_Enabled = true;
            }
            DateTime dateTime = DateTime.Now;
            string logName = $"Engine-{dateTime.Day}.{dateTime.Month}.{dateTime.Year}";
            m_LogFile = Path.Combine(GameSettings.Instance.FolderName, logName + ".log");
            if (!File.Exists(m_LogFile))
            {
                File.Create(m_LogFile);
            }

            using (StreamWriter streamWriter = File.AppendText(m_LogFile))
            {
                streamWriter.WriteLine($"[{dateTime.ToShortDateString()} {dateTime.ToShortTimeString()}] {logType.ToString()}: {log}");
            }
        }
        
        public static void Log(string message)
        {
            if (m_Enabled)
            {
                Debug.Log(message);
                WriteToLog(LogType.Debug, message);
            }
        }
        
        public static void LogError(string message)
        {
            Debug.LogError(message);
            WriteToLog(LogType.Error, message);
        }
        
        public static void LogWarning(string message)
        {
            Debug.LogWarning(message);
            WriteToLog(LogType.Warning, message);
        }
    }
}