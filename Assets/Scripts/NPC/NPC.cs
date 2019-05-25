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

[RequireComponent(typeof(ActorCombat))]
[RequireComponent(typeof(NPCMovement))]
[RequireComponent(typeof(NPCSight))]
public class NPC : Actor
{
    public ActorCombat m_CombatScript;
    public NPCMovement m_MovementScript;
    public NPCSight m_SightScript;

    private void Start()
    {
        m_CombatScript = GetComponent<ActorCombat>();
        m_MovementScript = GetComponent<NPCMovement>();
        m_SightScript = GetComponent<NPCSight>();
    }

    private void Update()
    {
        Actor actor = m_SightScript.NearestActor();
        if(actor != null)
        {
            m_CombatScript.Attack(actor);
        }
        else
        {
            if (m_MovementScript.IsAtDestionation())
            {
                // Look for enemies, if an enemy is found, go to that enemy.
                // If no enemy is found, go to a RandomDest().
                Actor enemy = m_SightScript.LookForEnemy();
                if (enemy != null)
                {
                    m_MovementScript.MoveTowards(enemy.transform.position);
                }
                else
                {
                    m_MovementScript.RandomDest();
                }
            }
        }
    }
}
