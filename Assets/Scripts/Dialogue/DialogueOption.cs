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
using UnityEngine.UI;
using System.Collections;

public class DialogueOption : MonoBehaviour
{
    public string dialogueFile;
    public string resultingDialogue;
    public string optionText;

    public Text text;

    private void Start()
    {
        if(!text)
        {
            text = GetComponentInChildren<Text>();
        }
        text.text = optionText;
    }

    public void OnSelectOption()
    {

    }
}
