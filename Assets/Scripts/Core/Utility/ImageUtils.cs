//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//

using TMPro;
using UnityEngine.UI;

namespace Core.Utility
{
    public static class ImageUtils
    {
        public static void SetAlpha(Image image, float alpha)
        {
            image.canvasRenderer.SetAlpha(alpha);
        }

        public static void SetAlpha(Button button, float alpha)
        {
            button.GetComponent<Image>().canvasRenderer.SetAlpha(alpha);
            button.GetComponentInChildren<TextMeshProUGUI>().canvasRenderer.SetAlpha(alpha);
        }

        public static void SetAlpha(TextMeshProUGUI text, float alpha)
        {
            text.GetComponent<TextMeshProUGUI>().canvasRenderer.SetAlpha(alpha);
        }

        public static void FadeAlpha(Image image, float alpha, float duration)
        {
            image.CrossFadeAlpha(alpha, duration, false);
        }

        public static void FadeAlpha(Button button, float alpha, float duration)
        {
            button.GetComponent<Image>().CrossFadeAlpha(alpha, duration, false);
            button.GetComponentInChildren<TextMeshProUGUI>().CrossFadeAlpha(alpha, duration, false);
        }

        public static void FadeAlpha(TextMeshProUGUI text, float alpha, float duration)
        {
            text.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(alpha, duration, false);
        }
    }
}
