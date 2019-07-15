//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//

using Core.Actor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : ActorHealth
{
    #region Singleton
    public PlayerHealth Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
    #endregion // Singleton

    public Scrollbar scrollbar;

    private void LateUpdate()
    {
        // Make sure that we cast currentHealth as a float otherwise C# will floor it for some reason.
        float healthPoints = (float)currentHealth / maxHealth;
        scrollbar.size = healthPoints;
    }
}
