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
        currentHealth += health;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
