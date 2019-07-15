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

namespace Core.World
{
    /// <summary>
    /// Defines a worldspace which 
    /// </summary>
    [Serializable]
    public class Worldspace
    {
        public string worldspaceID;
        public string worldspaceName;
        public Colour worldspaceSkyColour;
        public Colour worldspaceWaterColour;
    }

    [Serializable]
    public class Colour
    {
        public string r;
        public string g;
        public string b;
    }
}