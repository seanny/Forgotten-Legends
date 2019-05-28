//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void AddSentenes(List<string> sentences)
    {

    }

    public void AddOption(string option, string resultingDialogueID)
    {
        DialogueManager.Instance.AddOption(option, resultingDialogueID);
    }

    public void TriggerDialogue(string npcName, string fileName)
    {
        // First of all, freeze the camera rotation, we do not want any funky camera rotations during dialogue
        CameraRotation.Instance.ToggleRotation(false);

        // Start the dialogue via the DialogueManager
        dialogue = DialogueManager.Instance.StartDialogue(npcName, fileName, ref dialogue);
    }
}
