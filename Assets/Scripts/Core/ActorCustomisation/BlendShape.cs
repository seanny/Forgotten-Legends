//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//

public class BlendShape
{
    public int positiveIndex { get; set; }
    public int negativeIndex { get; set; }

    public BlendShape(int positive, int negative)
    {
        positiveIndex = positive;
        negativeIndex = negative;
    }
}
