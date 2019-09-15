//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//

using Core.Actor;using Core.Services;
using UnityEngine;

namespace Core.Player
{
    public class PlayerCombat : ActorCombat, IService
    {
        protected override void Start()
        {
            base.Start();
            ServiceLocator.AddService(this);
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyUp(KeyCode.Mouse0) && CanAttack() == true)
            {
            
            }
        }

        public void OnStart()
        {
            
        }

        public void OnEnd()
        {
            
        }
    }
}
