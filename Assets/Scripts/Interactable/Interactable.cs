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

public class Interactable : MonoBehaviour
{
    protected virtual void OnLookAt()
    {
        Debug.Log($"[Interactable.IsLookingAt]: Looking at {gameObject.name}");
    }

    public virtual void Interact()
    {
        Debug.Log($"[Interactable.Interact]: Interacting with {gameObject.name}");
    }

    public virtual void StopInteracting()
    {
        Debug.Log($"[Interactable.StopInteracting]: Stoping interacting with {gameObject.name}");
    }

    public bool InSight()
    {
        Vector3 dir = (transform.position - CameraController.Instance.transform.position).normalized;
        float dot = Vector3.Dot(dir, transform.forward);
        Debug.Log($"[Interactable.InSight]: In Sight Range for {gameObject.name}: {dir}");
        if (dot > 0.5f)
        {
            return true;
        }
        return false;
    }

    public bool IsClose()
    {
        float dist = Vector3.Distance(transform.position, PlayerManager.Instance.Player.transform.position);
        Debug.Log($"[Interactable.InSight]: IsClose distance for {gameObject.name}: {dist}");
        if (dist > 1.5f)
        {
            return true;
        }
        return false;
    }

    protected virtual void Update()
    {
        if (InSight() == true && IsClose() == true)
        {
            OnLookAt();
        }
    }
}
