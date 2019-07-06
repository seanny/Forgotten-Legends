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

public class InteractableManager : Singleton<InteractableManager>
{
    public bool isInteracting;
    public Interactable currentInteract;

    private void Update()
    {
        CheckInteracting();
    }

    public void CheckInteracting()
    {
        if (isInteracting == false)
        {
            Interactable interactable = FindNearestInteractable();
            if (interactable != null)
            {
                if (Input.GetKeyUp(KeyCode.F))
                {
                    Logger.Log(Channel.UI, $"Interacting with {interactable.gameObject.name}");
                    ForceInteract(interactable);
                }
            }
        }
    }

    public void ForceInteract(Interactable interactable)
    {
        if(currentInteract != null)
        {
            currentInteract.StopInteracting();
        }
        currentInteract = interactable;
        interactable.Interact();
    }

    protected virtual Interactable FindNearestInteractable()
    {
        Interactable[] interactables = FindObjectsOfType<Interactable>();
        for (int i = 0; i < interactables.Length; i++)
        {
            if(interactables[i].IsClose())
            {
                return interactables[i];
            }
        }
        return null;
    }
}
