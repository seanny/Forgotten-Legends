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
using Core.World;
using UnityEngine;
using UnityEngine.AI;

namespace Core.NonPlayerChar
{
    public class NPCMovement : MonoBehaviour
    {
        public NavMeshDest[] m_NavMeshDests;
        public Vector3 m_Destination;
        public NavMeshAgent m_Agent;
        public bool m_Moving;

        private void Start()
        {
            m_NavMeshDests = FindObjectsOfType<NavMeshDest>();
            m_Agent = GetComponent<NavMeshAgent>();
            StartCoroutine(SetDest(transform.position));
        }

        private IEnumerator SetDest(Vector3 pos)
        {
            while (MapManager.Instance.isUpdating)
            {
                yield return null;
            }

            MoveTowards(pos);
        }

        public void MoveTowardsActor(Actor.Actor actor)
        {
            MoveTowards(actor.transform.position);
        }

        public void LookAtActor(Actor.Actor actor)
        {
            transform.rotation = new Quaternion(transform.rotation.x, actor.transform.rotation.y - 180, transform.rotation.z, transform.rotation.w);
        }

        public void MoveTowards(Vector3 destination)
        {
            m_Destination = destination;
            if (m_Agent.isOnNavMesh)
            {
                m_Agent.destination = m_Destination;
            }
            m_Agent.autoRepath = true;
            m_Agent.autoBraking = true;
        }

        public void RandomDest()
        {
            int rand = Random.Range(0, m_NavMeshDests.Length - 1);
            MoveTowards(m_NavMeshDests[rand].transform.position);
        }

        public void StopMovement()
        {
            m_Agent.isStopped = true;
            m_Agent.velocity = Vector3.zero;
        }

        public void StartMovement()
        {
            m_Agent.isStopped = false;
        }

        public bool IsAtActor(Actor.Actor actor)
        {
            if (Vector3.Distance(actor.transform.position, transform.position) > 1.5f)
            {
                return false;
            }
            return true;
        }

        public bool IsAtDestionation()
        {
            //float dist = Vector3.Distance(m_Destination, transform.position);
            //if (dist > 1.5f)
            if(m_Agent.isOnNavMesh && m_Agent.remainingDistance > m_Agent.stoppingDistance)
            {
                return false;
            }
            return true;
        }
    }
}
