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

namespace Core.World
{
    [LuaApi(
        luaName = "World",
        description = "World API")]
    public class WorldAPI : LuaAPIBase
    {
        public WorldAPI() : base("World")
        {
        }
    
        protected override void InitialiseAPITable()
        {
            m_ApiTable["LoadAllWorldspaces"] = (Func<int>)Lua_LoadAllWorldspaces;
            m_ApiTable["LoadWorldspace"] = (Func<string, int>)Lua_LoadWorldspace;
        }

        [LuaApiFunction(
            name = "LoadAllWorldspaces",
            description = "Load all worldspaces from the file system into memory.")]
        private int Lua_LoadAllWorldspaces()
        {
            WorldspaceManager.Instance.LoadAllWorldspaces();
            return 0;
        }
    
        [LuaApiFunction(
            name = "LoadWorldspace",
            description = "Load a worldspace into memory.")]
        private int Lua_LoadWorldspace(string worldspaceID)
        {
            WorldspaceManager.Instance.LoadWorldspace(worldspaceID);
            return 0;
        }
    }
}