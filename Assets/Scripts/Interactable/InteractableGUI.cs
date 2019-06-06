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
using UnityEngine.UI;
using TMPro;

public class InteractableGUI : Singleton<InteractableGUI>
{
    public TextMeshProUGUI interactionText;
    public bool isShown { get; private set; }

    private void Start()
    {
        HideInteractString();
    }

    public void ShowInteractString(string objectName, string action)
    {
        string interactString = $"{objectName}{Environment.NewLine}F) {action}";
        interactionText.gameObject.SetActive(true);
        interactionText.text = interactString;
        isShown = true;
    }

    public void HideInteractString()
    {
        interactionText.gameObject.SetActive(false);
        isShown = false;
    }
}
