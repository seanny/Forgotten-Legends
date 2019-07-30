//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//

using System.Runtime.CompilerServices;
using UnityEngine;

namespace Core.Interactable
{
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

        private Interactable FindNearestInteractable()
        {
            Interactable[] interactables = FindObjectsOfType<Interactable>();
            for (int i = 0; i < interactables.Length; i++)
            {
                if(interactables[i].IsClose())
                {
                    Debug.Log($"Found item: {interactables[i].interactableData.name}");
                    return interactables[i];
                }
            }
            return null;
        }
    }
}
