//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//

using System.Collections;
using Core.Inventory;
using Core.Player;
using UnityEngine;

namespace Core.Potion
{
    public class Potion : Interactable.Interactable
    {
        public AudioClip soundEffect;
        public PotionStats potionStats;
        private AudioSource m_AudioSource;
        private bool m_Used;
        public bool isShown { get; private set; } 

        private void Start()
        {
            interactableData.name = "Potion";
            m_AudioSource = gameObject.AddComponent<AudioSource>();
            m_AudioSource.playOnAwake = false;
            m_AudioSource.clip = soundEffect;
            isShown = true;
        }

        public override void Interact()
        {
            base.Interact();
            PlayerManager.Instance.Player.actorInventory.AddItem(this);
            GetComponent<Renderer>().enabled = false;
            m_AudioSource.Play();
            m_Used = true;
            isShown = false;
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

        protected void Update()
        {
            if(m_Used == true && !m_AudioSource.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }
}
