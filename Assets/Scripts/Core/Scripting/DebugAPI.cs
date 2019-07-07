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

[LuaApi(
    luaName = "Debug",
    description = "Debugging API")]
public class DebugAPI : LuaAPIBase
{
    public DebugAPI()
        : base("Debug")
    {
    }

    protected override void InitialiseAPITable()
    {
        m_ApiTable["GetPlatformName"] = (Func<string>)Lua_GetPlatformName;
        m_ApiTable["GetConfigName"] = (Func<string>)Lua_GetConfigName;
        m_ApiTable["GetVersion"] = (Func<string>)Lua_GetVersion;
        m_ApiTable["QuitGame"] = (Func<string>)Lua_QuitGame;
    }

    [LuaApiFunction(
        name = "GetPlatformName",
        description = "Get the current platform name (Windows, MacOS or Linux)")]
    private string Lua_GetPlatformName()
    {
#if UNITY_EDITOR
        return "Editor";
#elif UNITY_STANDALONE_OSX
        return "MacOS";
#elif UNITY_STANDALONE_WIN
        return "Windows";
#elif UNITY_STANDALONE_LINUX
        return "Linux";
#endif
    }

    [LuaApiFunction(
        name = "GetConfigName",
        description = "Get the current build configuration (release or debug).")]
    private string Lua_GetConfigName()
    {
#if UNITY_EDITOR || UNITY_DEVELOPMENT
        return "Debug";
#else
        return "Release";
#endif
    }

    [LuaApiFunction(
        name = "GetVersion",
        description = "Get the current game version (Major.Minor.Patch).")]
    private string Lua_GetVersion()
    {
        return $"{Version.major}.{Version.minor}.{Version.patch}";
    }

    [LuaApiFunction(
        name = "QuitGame",
        description = "Quits the game.")]
    private string Lua_QuitGame()
    {
        return $"{Version.major}.{Version.minor}.{Version.patch}";
    }
}
