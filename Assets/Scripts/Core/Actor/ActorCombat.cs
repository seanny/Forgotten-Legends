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
    public class ActorCombat : MonoBehaviour
    {
        public Actor m_ParentScript;
        public int waitTime = 2;
        public int currentTime;
        public const float MELEE_COMBAT_RANGE = 2f;

        protected virtual void Start()
        {
            m_ParentScript = GetComponent<Actor>();
            StartCoroutine(AttackWait());

        }

        IEnumerator AttackWait()
        {
            while(true)
            {
                yield return new WaitForSeconds(1);
                if(currentTime > 0)
                {
                    currentTime--;
                }
            }
        }

        public bool CanAttack()
        {
            if (currentTime > 0)
            {
                return false;
            }
            return true;
        }

        public bool Attack(Actor enemy)
        {
            float dist = Vector3.Distance(enemy.transform.position, transform.position);
            if (dist > MELEE_COMBAT_RANGE || CanAttack() == false)
            {
                return false;
            }
            float attackStrength = ((Random.Range(0, m_ParentScript.actorStatController.level.statValue) / 2) + (m_ParentScript.actorStatController.GetStat("strength").statValue * m_ParentScript.actorStatController.level.statValue) - m_ParentScript.actorStatController.GetStat("endurance").statValue) / 2;
            attackStrength += ClassAttackModifier(ref attackStrength);
            if (attackStrength < 1f)
            {
                attackStrength = 1f;
            }
            currentTime = waitTime;

            // Calculation: ((0 <-> Luck) / 2) + (Strength * Level) - (Endurance / 2) + ClassAttackModifier
            // Example:     (10 / 2) + (10 + 2) - (10 / 2) + 1.5f = 13.5dmg
            return true;
        }

        private float ClassAttackModifier(ref float attackStrength)
        {
            switch(m_ParentScript.m_ActorClass.currentClass)
            {
                case ActorClass.Class.Fighter:
                    attackStrength += 1.5f;
                    break;
                case ActorClass.Class.Barbarian:
                    attackStrength += 0.5f;
                    break;
                case ActorClass.Class.Paladin:
                    attackStrength += 0.2f;
                    break;
            }
            return attackStrength;
        }
    }
}
