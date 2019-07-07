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
using System.Collections;

public class SpriteFading : Singleton<SpriteFading>
{
    public bool isFading;
    public float alpha;

    public void FadeSprite(SpriteRenderer spriteRenderer, float value, float time)
    {
        isFading = true;
        StartCoroutine(OnFadeSprite(spriteRenderer, value, time));
    }

    private IEnumerator OnFadeSprite(SpriteRenderer spriteRenderer, float value, float time)
    {
        alpha = spriteRenderer.material.color.a;
        for (float i = 0.0f; i < 1.0f; i += Time.deltaTime / time)
        {
            Color color = new Color(spriteRenderer.material.color.r, spriteRenderer.material.color.g, spriteRenderer.material.color.b, Mathf.Lerp(alpha, value, i));
            spriteRenderer.material.color = color;
            yield return null;
        }
        isFading = false;
    }
}
