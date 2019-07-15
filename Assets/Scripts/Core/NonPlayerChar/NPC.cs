//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//

using Core.Actor;
using UnityEngine;

namespace Core.NonPlayerChar
{
    [RequireComponent(typeof(ActorCombat))]
    [RequireComponent(typeof(NPCMovement))]
    [RequireComponent(typeof(NPCSight))]
    [RequireComponent(typeof(ActorHealth))]
    public class NPC : Actor.Actor
    {
        public ActorCombat m_CombatScript;
        public NPCMovement m_MovementScript;
        public NPCSight m_SightScript;
        public NPCEnemy m_EnemyScript;
        public NPCDialogue NPCDialogueScript;
        public Interactable.Interactable NPCInteractionScript;

        protected override void Start()
        {
            base.Start();
            m_CombatScript = GetComponent<ActorCombat>();
            if (!m_CombatScript)
            {
                m_CombatScript = gameObject.AddComponent<ActorCombat>();
            }

            m_MovementScript = GetComponent<NPCMovement>();
            if (!m_MovementScript)
            {
                m_MovementScript = gameObject.AddComponent<NPCMovement>();
            }

            m_SightScript = GetComponent<NPCSight>();
            if (!m_SightScript)
            {
                m_SightScript = gameObject.AddComponent<NPCSight>();
            }

            m_EnemyScript = GetComponent<NPCEnemy>();
            if(!m_EnemyScript)
            {
                m_EnemyScript = gameObject.AddComponent<NPCEnemy>();
            }


            NPCDialogueScript = gameObject.AddComponent<NPCDialogue>();

            NPCInteractionScript = gameObject.AddComponent<Interactable.Interactable>();
            NPCInteractionScript.interactType = Interactable.Interactable.InteractType.Talk;
        }

        public static NPC FindNPC(string actorID)
        {
            NPC[] npcs = GameObject.FindObjectsOfType<NPC>();
            foreach (NPC npc in npcs)
            {
                if (npc.actorID == actorID)
                {
                    return npc;
                }
            }
            return null;
        }

        private void Update()
        {
            Actor.Actor actor = m_SightScript.NearestActor();
            if(actor != null && m_EnemyScript.IsEnemy(actor) == true)
            {
                m_CombatScript.Attack(actor);
            }
            else
            {
                if (m_MovementScript.IsAtDestionation())
                {
                    // Look for enemies, if an enemy is found, go to that enemy.
                    // If no enemy is found, go to a RandomDest().
                    Actor.Actor enemy = m_SightScript.LookForEnemy();
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
}
