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

[DisallowMultipleComponent]
public class DialogueOption : MonoBehaviour
{
    public string dialogueFile;
    public string optionKey;
    private Button m_Button;

    private void Start()
    {
        m_Button = GetComponent<Button>();
        if (m_Button != null)
        {
            m_Button.onClick.AddListener(delegate
            {
                HandleDialogueOption();
            });
        }
    }

    public void HandleDialogueOption()
    {
        DialogueManager.Instance.ExecuteDialogOption(dialogueFile, optionKey);
    }
}
