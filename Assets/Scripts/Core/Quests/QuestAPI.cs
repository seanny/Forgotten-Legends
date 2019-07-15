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
            m_ApiTable["SetQuestObjective"] = (Func<string, string, int>)Lua_SetQuestObjective;
        }

        [LuaApiFunction(
            name = "GiveQuest",
            description = "Add a quest to the players quest journal.")]
        public int Lua_GiveQuest(string questID)
        {
            QuestJournal.Instance.GiveQuest(questID);
            return 0;
        }

        [LuaApiFunction(
            name = "SetQuestObjective",
            description = "Set the current objective in an active quest (quest must be in quest journal and not completed).")]
        public int Lua_SetQuestObjective(string questID, string questObjectiveID)
        {
            QuestJournal.Instance.SetQuestObjective(questID, questObjectiveID);
            return 0;
        }

        public int Lua_CompleteQuest(string questID)
        {
            QuestJournal.Instance.CompleteQuest(questID);
            return 0;
        }
    }
}
