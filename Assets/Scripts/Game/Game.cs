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
using ForgottenLegends.Core;

namespace ForgottenLegends.Game
{
    public class Game : IMod
    {
        public string Name
        {
            get
            {
                return "Forsaken Tales: Forgotten Legends";
            }
        }

        public string Description
        {
            get
            {
                return "Core Game";
            }
        }
    }
}
