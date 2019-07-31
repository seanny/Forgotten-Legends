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
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Utility
{
    public static class ImageUtils
    {
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
        
        public static bool GetImageSize(Texture2D asset, out int width, out int height)
        {
            if (asset != null) 
            {
                string assetPath = AssetDatabase.GetAssetPath(asset);
                TextureImporter importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;
 
                if (importer != null) 
                {
                    object[] args = new object[2] { 0, 0 };
                    MethodInfo mi = typeof(TextureImporter).GetMethod("GetWidthAndHeight", BindingFlags.NonPublic | BindingFlags.Instance);
                    mi.Invoke(importer, args);
 
                    width = (int)args[0];
                    height = (int)args[1];
                    return true;
                }
            }
 
            height = width = 0;
            return false;
        }
    }
}
