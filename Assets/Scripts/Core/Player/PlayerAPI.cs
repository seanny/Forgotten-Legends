//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//

namespace Core.Player
{
    [LuaApi(
        luaName = "Player",
        description = "Player API")]
    public class PlayerAPI : LuaAPIBase
    {
        public PlayerAPI() : base("Player") { }

        protected override void InitialiseAPITable()
        {

        }


    }
}
