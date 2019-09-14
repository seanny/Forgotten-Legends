//
//  Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
//  This document is the property of Outlaw Games Studio.
//  It is considered confidential and proprietary.
//
//  This document may not be reproduced or transmitted in any form
//  without the consent of Outlaw Games Studio.
//

using Core.Actor;
using UnityEngine.UI;

namespace Core.Player
{
    public class PlayerStamina : ActorStamina
    {
        public Scrollbar scrollbar;

        private void LateUpdate()
        {
            // Make sure that we cast currentHealth as a float otherwise C# will floor it for some reason.
            float staminaPoints = (float)currentStamina / maxStamina;
            scrollbar.size = staminaPoints;
        }
    }
}
