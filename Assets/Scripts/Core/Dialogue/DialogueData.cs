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

namespace Core.Dialogue
{
    [Serializable]
    public class DialogueData
    {
        public string npcID;
        public string dialogueKey;
        public string[] discussion;
        public string[] options;
    }
}
