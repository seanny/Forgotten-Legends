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
using UnityEngine.AI;
using System.Collections;

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
        m_Agent.destination = transform.position;
        m_Destination = m_Agent.destination;
    }

    public void MoveTowards(Vector3 destination)
    {
        m_Destination = destination;
        m_Agent.destination = m_Destination;
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

    public bool IsAtDestionation()
    {
        //float dist = Vector3.Distance(m_Destination, transform.position);
        //if (dist > 1.5f)
        if(m_Agent.remainingDistance > m_Agent.stoppingDistance)
        {
            return false;
        }
        return true;
    }
}
