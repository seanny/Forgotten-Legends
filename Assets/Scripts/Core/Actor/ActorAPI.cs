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
using Core.World;
using UnityEngine;

namespace Core.Actor
{
    [LuaApi(
        luaName = "Actor",
        description = "Actor API")]
    public class ActorAPI : LuaAPIBase
    {
        public ActorAPI() : base("Actor")
        {
        }

        protected override void InitialiseAPITable()
        {
            m_ApiTable["GetActorID"] = (Func<string, string>)Lua_GetActorID;
            m_ApiTable["SetActorLevel"] = (Func<string, int, int>)Lua_SetActorLevel;
            m_ApiTable["GetActorLevel"] = (Func<string, int>)Lua_GetActorLevel;
            m_ApiTable["SetActorPos"] = (Func<string, float, float, float, int>)Lua_SetActorPos;
            m_ApiTable["GetActorX"] = (Func<string, float>)Lua_GetActorX;
            m_ApiTable["GetActorY"] = (Func<string, float>)Lua_GetActorY;
            m_ApiTable["GetActorZ"] = (Func<string, float>)Lua_GetActorZ;
            m_ApiTable["GetGender"] = (Func<string, int>)Lua_GetGender;
            m_ApiTable["GetClass"] = (Func<string, int>)Lua_GetClass;
            m_ApiTable["SetWorldspace"] = (Func<string, string, int>)Lua_SetWorldspace;
        }

        [LuaApiFunction(
            name = "GetActorID",
            description = "Returns the level for an actor.")]
        private string Lua_GetActorID(string gameObjectName)
        {
            Actor[] actors = GameObject.FindObjectsOfType<Actor>();
            for (int i = 0; i < actors.Length; i++)
            {
                if(actors[i].gameObject.name == gameObjectName)
                {
                    return actors[i].actorID;
                }
            }
            return "NULL";
        }

        [LuaApiFunction(
            name = "SetActorLevel",
            description = "Set the level for an actor.")]
        private int Lua_SetActorLevel(string actorID, int level)
        {
            Actor _actor = Actor.FindActor(actorID);
            if (_actor != null)
            {
                _actor.m_ActorStats.currentLevel = level;
                return 0;
            }
            return 1;
        }

        [LuaApiFunction(
            name = "GetActorLevel",
            description = "Returns the level for an actor.")]
        private int Lua_GetActorLevel(string actorID)
        {
            Actor _actor = Actor.FindActor(actorID);
            if (_actor != null)
            {
                return _actor.m_ActorStats.currentLevel;
            }
            return -1;
        }

        [LuaApiFunction(
            name = "SetActorPos",
            description = "Set the position for the actor.")]
        public int Lua_SetActorPos(string actorID, float x, float y, float z)
        {
            Actor _actor = Actor.FindActor(actorID);
            if (_actor != null)
            {
                _actor.SetPos(new Vector3(x, y, z));
                return 0;
            }
            return 1;
        }

        [LuaApiFunction(
            name = "GetActorX",
            description = "Returns the X-axis for the actor.")]
        public float Lua_GetActorX(string actorID)
        {
            Actor _actor = Actor.FindActor(actorID);
            if (_actor != null)
            {
                return _actor.transform.position.x;
            }
            return 0.0f;
        }

        [LuaApiFunction(
            name = "GetActorY",
            description = "Returns the Y-axis for the actor.")]
        public float Lua_GetActorY(string actorID)
        {
            Actor _actor = Actor.FindActor(actorID);
            if (_actor != null)
            {
                return _actor.transform.position.y;
            }
            return 0.0f;
        }

        [LuaApiFunction(
            name = "GetActorZ",
            description = "Returns the Z-axis for the actor.")]
        public float Lua_GetActorZ(string actorID)
        {
            Actor _actor = Actor.FindActor(actorID);
            if (_actor != null)
            {
                return _actor.transform.position.z;
            }
            return 0.0f;
        }

        [LuaApiFunction(
            name = "GetGender",
            description = "Returns the gender (sex) for the actor.")]
        public int Lua_GetGender(string actorID)
        {
            Actor _actor = Actor.FindActor(actorID);
            if (_actor != null)
            {
                if (_actor.m_ActorStats.isFemale)
                {
                    return 1;
                }
                return 0;
            }
            return -1;
        }

        [LuaApiFunction(
            name = "GetClass",
            description = "Returns the class for the actor.")]
        public int Lua_GetClass(string actorID)
        {
            Actor _actor = Actor.FindActor(actorID);
            if (_actor != null)
            {
                return (int)_actor.m_ActorClass.currentClass;
            }
            return -1;
        }
    
        [LuaApiFunction(
            name = "SetWorldspace",
            description = "Sets the current worldspace for the actor.")]
        public int Lua_SetWorldspace(string actorID, string worldspaceID)
        {
            Actor _actor = Actor.FindActor(actorID);
            if (_actor != null)
            {
                WorldspaceManager.Instance.SetActorWorldspace(_actor, worldspaceID);
                return 0;
            }
            return 1;
        }
    }
}
