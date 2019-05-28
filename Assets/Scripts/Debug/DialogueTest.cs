//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//
using UnityEngine;
using System.Collections;

public class DialogueTest : MonoBehaviour
{
    DialogueTrigger dialogueTrigger;

    private void Start()
    {
        dialogueTrigger = gameObject.AddComponent<DialogueTrigger>();
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.F))
        {
            dialogueTrigger.AddOption("Test 1", "test1");
            dialogueTrigger.AddOption("Test 2", "test2");
            dialogueTrigger.TriggerDialogue("Test", "test.json");
        }
    }
}
