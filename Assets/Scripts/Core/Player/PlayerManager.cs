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
using Core.Services;

namespace Core.Player
{
    public class PlayerManager : IService
    {
        public Player Player { get; private set; }

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

        public void OnStart()
        {
            AddPlayer();
        }

        public void OnEnd()
        {
            RemovePlayer();
        }
    }
}
