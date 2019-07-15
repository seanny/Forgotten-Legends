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
using UnityEngine;

namespace Core.Actor
{
    public class ActorStamina : MonoBehaviour
    {
        public int maxStamina;
        public int currentStamina;
        public bool magicRegen;

        // Use this for initialization
        protected virtual void Start()
        {
            // Prevents a divide by 0 error
            if (maxStamina < 0)
            {
                maxStamina = 1;
            }
            StartCoroutine(MagicRegen());
            magicRegen = true;
        }

        IEnumerator MagicRegen()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                if (currentStamina < maxStamina && magicRegen == true)
                {
                    currentStamina++;
                }
            }
        }

        public void AddMagic(int magic)
        {
            currentStamina += magic;
            if (currentStamina > maxStamina)
            {
                currentStamina = maxStamina;
            }
        }
    }
}
