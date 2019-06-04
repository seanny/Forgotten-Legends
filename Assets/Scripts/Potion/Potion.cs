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

public class Potion : Interactable
{
    public AudioClip soundEffect;
    public PotionStats potionStats;
    private AudioSource m_AudioSource;
    private bool m_Used;

    private void Start()
    {
        m_AudioSource = gameObject.AddComponent<AudioSource>();
        m_AudioSource.playOnAwake = false;
        m_AudioSource.clip = soundEffect;
    }

    public override void Interact()
    {
        base.Interact();
        OnPotionUse();
    }

    public virtual void OnPotionUse()
    {
        if(m_Used == false)
        {
            m_AudioSource.Play();
            if(potionStats.healthRegen > 0)
            {
                PotionEffects.Instance.AddHealthEffect(potionStats.healthRegen, PlayerManager.Instance.Player);
            }
            if (potionStats.magicRegen > 0)
            {
                PotionEffects.Instance.AddMagicEffect(potionStats.magicRegen, PlayerManager.Instance.Player);
            }
            if (potionStats.staminaRegen > 0)
            {
                PotionEffects.Instance.AddStaminaEffect(potionStats.staminaRegen, PlayerManager.Instance.Player);
            }
            m_Used = true;
        }
    }

    private void Update()
    {
        if(m_Used == true && !m_AudioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
