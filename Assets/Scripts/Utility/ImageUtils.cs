//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//
using UnityEngine.UI;

public static class ImageUtils
{
    public static void SetAlpha(Image image, float alpha)
    {
        image.canvasRenderer.SetAlpha(alpha);
    }

    public static void FadeAlpha(Image image, float alpha, float duration)
    {
        image.CrossFadeAlpha(alpha, duration, false);
    }
}
