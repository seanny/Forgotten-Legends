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
using Core.Utility;
using UnityEngine;

namespace Core.Actor
{
    public class ActorHealth : MonoBehaviour
    {
        public int maxHealth;
        public int currentHealth;
        public bool healthRegen;

        // Use this for initialization
        protected virtual void Start()
        {
            // Prevents a divide by 0 error
            if (maxHealth < 0)
            {
                maxHealth = 1;
            }
            StartCoroutine(HealthRegen());
            healthRegen = true;
        }

        IEnumerator HealthRegen()
        {
            while(true)
            {
                yield return new WaitForSeconds(1);
                if(currentHealth < maxHealth && healthRegen == true)
                {
                    currentHealth++;
                }
            }
        }

        public void AddHealth(int health)
        {
            Logging.Log($"1");
            currentHealth += health;
            Logging.Log($"2");
            if (currentHealth > maxHealth)
            {
                Logging.Log($"3");
                currentHealth = maxHealth;
            }
        }

        public void TakeHealth(int health)
        {
            currentHealth -= health;
            if (currentHealth < 0)
            {
                currentHealth = 0;
            }
        }
    }
}
