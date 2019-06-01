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
    public virtual void Interact()
    {
        Debug.Log($"Interacting with {gameObject.name}");
    }

    public virtual void StopInteracting()
    {
        Debug.Log($"Stoping interacting with {gameObject.name}");
    }
}
