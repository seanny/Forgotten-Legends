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

public static class MouseCursor
{
    public static void ToggleCursor(bool toggle)
    {
        Cursor.visible = toggle;
    }

    public static void LockCursor(bool toggle)
    {
        if(toggle)
        {
            Cursor.lockState = CursorLockMode.Locked;
            ToggleCursor(false);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            ToggleCursor(true);
        }
    }
}
