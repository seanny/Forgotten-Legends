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
        //m_ApiTable["OnDialogueChoice"] = (Func<string, int>)Lua_OnDialogueChoice;
    }

    //int Lua_OnDialogueChoice(string dialogueChoiceID)
    //{
    //    return 0;
    //}
}
