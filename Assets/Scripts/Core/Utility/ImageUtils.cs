//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//

using System.Reflection;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Utility
{
    public static class ImageUtils
    {
        public static Texture2D LoadPNG(string filePath)
        {
            Texture2D tex = null;
            byte[] fileData;
 
            if (File.Exists(filePath))
            {
                fileData = File.ReadAllBytes(filePath);
                tex = new Texture2D(2, 2);
                tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
            }
            return tex;
        }
        
        public static void SetSize(RectTransform self, Vector2 size)
        {
            Vector2 oldSize = self.rect.size;
            Vector2 deltaSize = size - oldSize;
 
            self.offsetMin = self.offsetMin - new Vector2(
                                 deltaSize.x * self.pivot.x,
                                 deltaSize.y * self.pivot.y);
            self.offsetMax = self.offsetMax + new Vector2(
                                 deltaSize.x * (1f - self.pivot.x),
                                 deltaSize.y * (1f - self.pivot.y));
        }
 
        public static void SetWidth(RectTransform self, float size)
        {
            SetSize(self, new Vector2(size, self.rect.size.y));
        }
 
        public static void SetHeight(RectTransform self, float size)
        {
            SetSize(self, new Vector2(self.rect.size.x, size));
        }

        
        public static void SetAlpha(Image image, float alpha)
        {
            image.canvasRenderer.SetAlpha(alpha);
        }

        public static void SetAlpha(Button button, float alpha)
        {
            button.GetComponent<Image>().canvasRenderer.SetAlpha(alpha);
            button.GetComponentInChildren<TextMeshProUGUI>().canvasRenderer.SetAlpha(alpha);
        }
        
        public static void SetAlpha(Scrollbar scrollbar, float alpha)
        {
            //gameObject.GetComponent<Image>().canvasRenderer.SetAlpha(alpha);
            //button.GetComponentInChildren<TextMeshProUGUI>().canvasRenderer.SetAlpha(alpha);
            Image[] images = scrollbar.GetComponentsInChildren<Image>();
            TextMeshProUGUI[] texts = scrollbar.GetComponentsInChildren<TextMeshProUGUI>();

            foreach (var item in images)
            {
                scrollbar.GetComponent<Image>().canvasRenderer.SetAlpha(alpha);
            }
            
            foreach (var item in texts)
            {
                scrollbar.GetComponent<TextMeshProUGUI>().canvasRenderer.SetAlpha(alpha);
            }
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
        
        public static void FadeAlpha(Scrollbar scrollbar, float alpha, float duration)
        {
            Image[] images = scrollbar.GetComponentsInChildren<Image>();
            TextMeshProUGUI[] texts = scrollbar.GetComponentsInChildren<TextMeshProUGUI>();

            if (images.Length > 0)
            {
                foreach (var item in images)
                {
                    scrollbar.GetComponent<Image>().CrossFadeAlpha(alpha, duration, false);
                }
            }

            if (texts.Length > 0)
            {
                foreach (var item in texts)
                {
                    //scrollbar.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(alpha, duration, false);
                }
            }
        }
    }
}
