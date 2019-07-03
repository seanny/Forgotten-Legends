//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//
using System;
using UnityEngine;
using UnityEditor;

public class Build : MonoBehaviour
{
    public static readonly BuildTarget[] BUILD_TARGETS =
    {
        BuildTarget.StandaloneWindows64, // Windows x64
        BuildTarget.StandaloneOSX, // macOS
        BuildTarget.StandaloneLinux64 // Linux
    };

    [MenuItem("Tools/Build All")]
    public static void Init()
    {
        bool debugBuild;
        string[] arguments = Environment.GetCommandLineArgs();
        switch (arguments[1])
        {
            case "-prod":
                debugBuild = false;
                break;
            default:
                debugBuild = true;
                break;
        }

        InitBuild(debugBuild);
    }

    private static void InitBuild(bool developmentBuild)
    {
        foreach (var target in BUILD_TARGETS)
        {
            var locationPathName = "builds/" + ("" + target).ToLower().Replace("standalone", "") + $"/ForgottenLegends";
            if (target == BuildTarget.StandaloneWindows64)
            {
                locationPathName += ".exe";
            }

            var options = new BuildPlayerOptions
            {
                locationPathName = locationPathName,
                scenes = new[]
                {
                    "Assets/Scenes/MainMenu.unity",
                    "Assets/Scenes/SampleScene.unity"
                },
                options = developmentBuild ? BuildOptions.Development : BuildOptions.None,
                target = target
            };

            Debug.Log($"Building {Application.productName} for {target} to {locationPathName}...");
            BuildPipeline.BuildPlayer(options);
        }
    }
}
