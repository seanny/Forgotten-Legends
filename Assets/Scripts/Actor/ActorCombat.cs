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
        if(currentTime > 0)
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
        float attackStrength = ((Random.Range(0, m_ParentScript.m_ActorStats.luck) / 2) + (m_ParentScript.m_ActorStats.strength * m_ParentScript.m_ActorStats.currentLevel) - m_ParentScript.m_ActorStats.endurance) / 2;
        if (attackStrength < 1f)
        {
            attackStrength = 1f;
        }
        Debug.Log($"Attack Strength: {attackStrength}");
        currentTime = waitTime;

        // Calculation: ((0 <-> Luck) / 2) + (Strength * Level) - (Endurance / 2)
        // Example:     (10 / 2) + (10 + 2) - (10 / 2) = 12dmg
        return true;
    }
}
