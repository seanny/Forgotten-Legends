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

[LuaApi(
    luaName = "Console",
    description = "Debugging API")]
public class DebugAPI : LuaAPIBase
{
    public DebugAPI()
        : base("Debug")
    {
    }

    protected override void InitialiseAPITable()
    {
        m_ApiTable["WriteLog"] = (Func<string, string>)Lua_WriteLog;
    }

    [LuaApiFunction(
        name = "WriteLog",
        description = "Write to the log console.")]
    private string Lua_WriteLog(string log)
    {
        Debug.Log(log);
        return "0";
    }
}
