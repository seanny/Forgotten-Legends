//
//  Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
//  This document is the property of Outlaw Games Studio.
//  It is considered confidential and proprietary.
//
//  This document may not be reproduced or transmitted in any form
//  without the consent of Outlaw Games Studio.
//
using System;
using System.IO;
using System.Diagnostics;
using UnityEngine;
using UnityEditor;

public class OpenTerminal : ScriptableObject
{
    [MenuItem("Tools/Open Terminal")]
    static void OpenTerminalPrompt()
    {
        string appsFolder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
        string terminalApp;

        Process process = new Process();
#if UNITY_EDITOR_OSX
        terminalApp = Path.Combine(appsFolder, "Utilities", "Terminal.app");

        // Configure the process using the StartInfo properties.
        process.StartInfo.FileName = "open";
        process.StartInfo.Arguments = $"-a \"{terminalApp}\" .";
#endif
        process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
        process.Start();

        UnityEngine.Debug.Log($"Opening terminal at \"{terminalApp}\".\nCmd: {process.StartInfo.FileName} {process.StartInfo.Arguments}");
    }
}