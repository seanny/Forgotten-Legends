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
    luaName = "Player",
    description = "Player API")]
public class PlayerAPI : LuaAPIBase
{
    public PlayerAPI() : base("Player") { }

    protected override void InitialiseAPITable()
    {
        m_ApiTable["SetActorPos"] = (Func<string, float, float, float, int>)Lua_SetActorPos;
    }

    public int Lua_SetActorPos(string actorID, float x, float y, float z)
    {
        Actor[] actors = GameObject.FindObjectsOfType<Actor>();
        foreach(Actor actor in actors)
        {
            if(actor.actorID == actorID)
            {
                actor.transform.position = new Vector3(x, y, z);
                return 0;
            }
        }
        return 1;
    }
}
