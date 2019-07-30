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
using Core.Scripting;

namespace Core.Quests
{
    [LuaApi(
        luaName = "Quest",
        description = "Quest API")]
    public class QuestAPI : LuaAPIBase
    {
        public QuestAPI() : base("Quest")
        {

        }

        protected override void InitialiseAPITable()
        {
            m_ApiTable["GiveQuest"] = (Func<string, int>)Lua_GiveQuest;
            m_ApiTable["FailQuest"] = (Func<string, int>)Lua_FailQuest;
            m_ApiTable["CompleteQuest"] = (Func<string, int>)Lua_CompleteQuest;
            m_ApiTable["SetQuestObjective"] = (Func<string, string, int>)Lua_SetQuestObjective;
        }

        [LuaApiFunction(
            name = "GiveQuest",
            description = "Add a quest to the players quest journal.")]
        private int Lua_GiveQuest(string questID)
        {
            QuestJournal.Instance.GiveQuest(questID);
            ScriptManager.Instance.CallFunction("OnQuestStart", new object[] { questID });
            return 0;
        }
        
        [LuaApiFunction(
            name = "FailQuest",
            description = "Fail a quest with the specified questID.")]
        private int Lua_FailQuest(string questID)
        {
            QuestJournal.Instance.CompleteQuest(questID, true);
            ScriptManager.Instance.CallFunction("OnQuestEnd", new object[] { questID, true });
            return 0;
        }

        [LuaApiFunction(
            name = "SetQuestObjective",
            description = "Set the current objective in an active quest (quest must be in quest journal and not completed).")]
        private int Lua_SetQuestObjective(string questID, string questObjectiveID)
        {
            QuestJournal.Instance.SetQuestObjective(questID, questObjectiveID);
            ScriptManager.Instance.CallFunction("OnQuestStageStart", new object[] { questID, questObjectiveID });
            return 0;
        }

        [LuaApiFunction(
            name = "CompleteQuest",
            description = "Complete the specified questID.")]
        private int Lua_CompleteQuest(string questID)
        {
            QuestJournal.Instance.CompleteQuest(questID, false);
            ScriptManager.Instance.CallFunction("OnQuestEnd", new object[] { questID, false });
            return 0;
        }
    }
}
