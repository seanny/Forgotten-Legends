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

public class NPCEnemy : MonoBehaviour
{
    private NPC m_ParentScript;

    private void Start()
    {
        m_ParentScript = GetComponent<NPC>();
    }

    public bool IsEnemyInEnemyFaction(Actor actor)
    {
        // Check if any actor faction ID is in the NPC faction enemy list
        // We will do this by looking up the NPC factions and then comparing the faction enemy list with actor factions
        // FIXME: Some enemies are not detected as enemies
        for (int i = 0; i < m_ParentScript.m_ActorStats.factions.Count; i++)
        {
            for (int x = 0; x < actor.m_ActorStats.factions.Count; x++)
            {
                for (int y = 0; y < CoreFactions.Instance.factions[i].enemyFactions.Count; y++)
                {
                    // Check if NPC faction contains actor faction listed    inside of enemyFactions
                    if (CoreFactions.Instance.factions[i].enemyFactions.Contains(actor.m_ActorStats.factions[x]))
                    {
                        return true;
                    }

                    //if (CoreFactions.Instance.factions[y].id == actor.m_ActorStats.factions[x])
                }
            }
        }
        return false;
    }

    public bool IsEnemy(Actor actor)
    {
        // Determine if the specified actor is an enemy by: 
        // a. checking the faction affiliation of the actor
        // If the NPC faction enemyFactions property contains the actors faction then return true
        if(IsEnemyInEnemyFaction(actor))
        {
            return true;
        }
        // or b. by checking if the NPC has the aggression level of frenzied (not yet implemented)
        return false;
    }
}
