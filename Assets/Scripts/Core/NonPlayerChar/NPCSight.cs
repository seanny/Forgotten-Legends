//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//

using System.Collections.Generic;
using UnityEngine;

namespace Core.NonPlayerChar
{
    public class NPCSight : MonoBehaviour
    {
        public NPC m_ParentScript;
        public float fieldOfViewAngle = 110f;
        public float lineOfSight = 20f;
        public List<Actor.Actor> actorsInSight;

        private void Start()
        {
            m_ParentScript = GetComponent<NPC>();
        }

        public float CalculateLineOfSight()
        {
            return lineOfSight + m_ParentScript.actorStatController.GetStat("perception").statValue;
        }

        public Actor.Actor LookForEnemy()
        {
            for (int i = 0; i < actorsInSight.Count; i++)
            {
                // TODO: Check if actor is an actual enemy;
                if(m_ParentScript.m_EnemyScript.IsEnemy(actorsInSight[i]))
                {
                    return actorsInSight[i];
                }
            }
            return null;
        }

        public Actor.Actor NearestActor()
        {
            for (int i = 0; i < actorsInSight.Count; i++)
            {
                if(Vector3.Distance(actorsInSight[i].transform.position, transform.position) <= 1.5f)
                {
                    return actorsInSight[i];
                }
            }
            return null;
        }

        private void Update()
        {
            Actor.Actor[] actors = FindObjectsOfType<Actor.Actor>();
            for (int i = 0; i < actors.Length; i++)
            {
                if(actors[i] == m_ParentScript)
                {
                    continue;
                }
                if (actorsInSight.Contains(actors[i]) == true && Vector3.Distance(actors[i].transform.position, transform.position) > CalculateLineOfSight())
                {
                    actorsInSight.Remove(actors[i]);
                }
                if (actorsInSight.Contains(actors[i]) == false && Vector3.Distance(actors[i].transform.position, transform.position) <= CalculateLineOfSight())
                {
                    actorsInSight.Add(actors[i]);
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, lineOfSight);
        }
    }
}
