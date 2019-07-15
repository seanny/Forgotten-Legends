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
using Core.CommandConsole;
using UnityEngine;

namespace Core.Utility
{
    public static class Version
    {
        public const string BUILD_FILE = "build.txt";

        public static int major = 0;
        public static int minor = 1;
        public static int patch = 0;
        public static int build = 0;

        [RegisterCommand(Help = "Get Game Version")]
        public static void CommandVersion(CommandArg[] args)
        {
            Debug.Log($"Current Version: {GetVersion()}");
        }

        public static string GetVersion()
        {
            string settingsPath = Path.GetDirectoryName(Application.dataPath);
            settingsPath = Path.Combine(settingsPath, BUILD_FILE);
            string buildStr = File.ReadAllText(settingsPath);
            if(!int.TryParse(buildStr, out build))
            {
                Debug.LogWarning($"Cannot get build, make sure that {BUILD_FILE} is a text file.");
            }

            string version = $"{major}.{minor}.{patch}.{build}";
#if UNITY_DEVELOPMENT || UNITY_EDITOR
            version += " (Debug)";
#endif
            return version;
        }
    }
}
