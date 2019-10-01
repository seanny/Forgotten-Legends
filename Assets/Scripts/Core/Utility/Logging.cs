using UnityEngine;

namespace Core.Utility
{
    public static class Logging
    {
        public static void Log(string message)
        {
            // Strip debug.logs from the release build
#if UNITY_EDITOR
            Debug.Log(message);
#endif
        }
        
        public static void LogError(string message)
        {
            Debug.LogError(message);
        }
        
        public static void LogWarning(string message)
        {
            Debug.LogWarning(message);
        }
    }
}