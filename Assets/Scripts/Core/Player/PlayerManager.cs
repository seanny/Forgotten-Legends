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

public class PlayerManager : Singleton<PlayerManager>
{
    public Player Player { get; private set; }

    private void Start()
    {
        AddPlayer();
    }

    public void AddPlayer()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    public Player GetPlayer()
    {
        if (!Player)
        {
            AddPlayer();
        }
        return Player;
    }
    
    public void RemovePlayer()
    {
        Player = null;
    }
}
