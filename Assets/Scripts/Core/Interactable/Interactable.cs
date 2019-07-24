//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//

using System;
using Core.Camera;
using Core.Player;
using Core.Scripting;
using UnityEngine;

namespace Core.Interactable
{
    public class Interactable : MonoBehaviour
    {
        public InteractableData InteractableData;

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

        private void InitData()
        {
            if (InteractableData == null)
            {
                InteractableData = new InteractableData();
            }
        }

        private void Start()
        {
            InitData();
        }

        public void SetInteractableType(InteractableData.InteractType interactType)
        {
            InitData();
            InteractableData.type = interactType;
        }
        
        public void SetInteractableCategory(InteractableData.InteractableCategory interactCat)
        {
            InitData();
            InteractableData.category = interactCat;
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
            InitData();
            if (InteractableGUI.Instance.isShown == false)
            {
                switch (InteractableData.type)
                {
                    case InteractableData.InteractType.Generic:
                        InteractableGUI.Instance.ShowInteractString(InteractableData.name, "Interact");
                        break;
                    case InteractableData.InteractType.Talk:
                        InteractableGUI.Instance.ShowInteractString(InteractableData.name, "Talk");
                        break;
                    case InteractableData.InteractType.Take:
                        // TODO: If an object is owned by another NPC, then "Take" becomes "Steal"
                        InteractableGUI.Instance.ShowInteractString(InteractableData.name, "Take");
                        break;
                }
            }
        }

        protected virtual void Update()
        {
            InitData();
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
}
