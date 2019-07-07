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
    public enum InteractType
    {
        Generic = 0,
        Talk,
        Take
    }

    public InteractType interactType;
    public string interactableName;

    protected virtual void OnLookAt()
    {
        ScriptManager.Instance.CallFunction("OnLook", new object[] { gameObject.name });
    }

    public virtual void Interact()
    {
        ScriptManager.Instance.CallFunction("OnStartInteract", new object[] { gameObject.name });
    }

    public virtual void StopInteracting()
    {
        ScriptManager.Instance.CallFunction("OnStopInteract", new object[] { gameObject.name });
    }

    public bool InSight()
    {
        Vector3 dir = (transform.position - CameraController.Instance.transform.position).normalized;
        float dot = Vector3.Dot(dir, transform.forward);
        if (dot > 0.5f)
        {
            return true;
        }
        return false;
    }

    public bool IsClose()
    {
        float dist = Vector3.Distance(transform.position, PlayerManager.Instance.Player.transform.position);
        if (dist <= 2.0f)
        {
            return true;
        }
        return false;
    }

    private void ShowInteractGUI()
    {
        if (InteractableGUI.Instance.isShown == false)
        {
            switch (interactType)
            {
                case InteractType.Generic:
                    InteractableGUI.Instance.ShowInteractString(interactableName, "Interact");
                    break;
                case InteractType.Talk:
                    InteractableGUI.Instance.ShowInteractString(interactableName, "Talk");
                    break;
                case InteractType.Take:
                    // TODO: If an object is owned by another NPC, then "Take" becomes "Steal"
                    InteractableGUI.Instance.ShowInteractString(interactableName, "Take");
                    break;
            }
        }
    }

    protected virtual void Update()
    {
        if (InSight() == true && IsClose() == true)
        {
            ShowInteractGUI();
            OnLookAt();
        }
        else
        {
            InteractableGUI.Instance.HideInteractString();
        }
    }
}
