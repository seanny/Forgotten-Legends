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
    luaName = "Dialogue",
    description = "Dialogue API")]
public class DialogueAPI : LuaAPIBase
{
    public DialogueAPI()
        : base("Dialogue")
    {
    }

    protected override void InitialiseAPITable()
    {
        m_ApiTable["ShowDialogueChoices"] = (Func<bool, int>)Lua_ShowDialogueChoices;
        m_ApiTable["AddDialogueChoice"] = (Func<string, string, int>)Lua_AddDialogueChoice;
        m_ApiTable["InitiateDialogue"] = (Func<string, string, int>)Lua_InitiateDialogue;
        m_ApiTable["ExitDialogue"] = (Func<int>)Lua_ExitDialogue;
        m_ApiTable["ClearDialogueChoices"] = (Func<int>)Lua_ClearDialogueChoices;
        m_ApiTable["ChangeDialogueDiscussion"] = (Func<string, int>)Lua_ChangeDialogueDiscussion;
    }

    [LuaApiFunction(
        name = "ShowDialogueChoices",
        description = "Show the dialogue choices menu whilst engaged in dialogue. AddDialogueChoice must be called at least once before using this.")]
    private int Lua_ShowDialogueChoices(bool toggle)
    {
        DialogueManager.Instance.ShowDialogueChoices(toggle);
        return 1;
    }

    [LuaApiFunction(
        name = "AddDialogueChoice",
        description = "Add an option to the dialogue choice menu. Must be called before ShowDialogueChoices!")]
    private int Lua_AddDialogueChoice(string file, string key)
    {
        DialogueManager.Instance.AddOption(file, key);
        return 1;
    }

    [LuaApiFunction(
        name = "InitiateDialogue",
        description = "Initates dialogue with an NPC. They must be at least within 2 meters of the player")]
    private int Lua_InitiateDialogue(string file, string actorID)
    {
        NPC _npc = NPC.FindNPC(actorID);
        if (_npc != null)
        {
            DialogueManager.Instance.StartDialogue(_npc, file);
            return 0;
        }
        return 1;
    }

    [LuaApiFunction(
        name = "ExitDialogue",
        description = "Exits the dialogue menu.")]
    private int Lua_ExitDialogue()
    {
        DialogueManager.Instance.ExitDialogue(false);
        return 1;
    }

    [LuaApiFunction(
        name = "ClearDialogueChoices",
        description = "Clears all dialogue choices.")]
    private int Lua_ClearDialogueChoices()
    {
        DialogueManager.Instance.ClearDialogueChoices();
        return 1;
    }

    [LuaApiFunction(
        name = "ChangeDialogueDiscussion",
        description = "Changes current dialogue discussion.")]
    private int Lua_ChangeDialogueDiscussion(string file)
    {
        DialogueManager.Instance.TriggerAnotherDialogue(file);
        return 1;
    }
}
