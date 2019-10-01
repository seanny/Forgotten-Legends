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
using Core.Camera;
using Core.MathUtil;
using Core.Player;
using Core.Scripting;
using Core.Services;
using UnityEngine;

namespace Core.Interactable
{
    public class Interactable : WorldEntity
    {
        public InteractableData interactableData;

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
            if (interactableData == null)
            {
                interactableData = new InteractableData();
            }
        }

        protected override void Start()
        {
            InitData();
            StartCoroutine(OnUpdateInteractable());
            ServiceLocator.GetService<InteractableGUI>();
        }

        public void SetInteractableType(InteractableData.InteractType interactType)
        {
            InitData();
            interactableData.type = interactType;
        }
        
        public void SetInteractableCategory(InteractableData.InteractableCategory interactCat)
        {
            InitData();
            interactableData.category = interactCat;
        }

        public void SetInteractableName(string name)
        {
            InitData();
            interactableData.name = name;
        }
        
        public void SetInteractablePosition(Vector3 position)
        {
            InitData();
            interactableData.position = new Vec3(position);
        }
        
        public bool InSight()
        {
            if (CameraController.isReady == false)
            {
                return false;
            }
            Vector3 dir = (transform.position - ServiceLocator.GetService<CameraController>().transform.position).normalized;
            float dot = Vector3.Dot(dir, transform.forward);
            if (dot > 0.5f)
            {
                return true;
            }
            return false;
        }

        public bool IsClose()
        {
            float dist = Vector3.Distance(transform.position, ServiceLocator.GetService<PlayerManager>().GetPlayer().transform.position);
            if (dist <= 2.5f)
            {
                return true;
            }
            return false;
        }

        private void ShowInteractGUI()
        {
            InitData();
            if (ServiceLocator.GetService<InteractableGUI>().isShown == false)
            {
                switch (interactableData.type)
                {
                    case InteractableData.InteractType.Talk:
                        ServiceLocator.GetService<InteractableGUI>().ShowInteractString(interactableData.name, "Talk");
                        break;
                    case InteractableData.InteractType.Take:
                        // TODO: If an object is owned by another NPC, then "Take" becomes "Steal"
                        ServiceLocator.GetService<InteractableGUI>().ShowInteractString(interactableData.name, "Take");
                        break;
                    default:
                        ServiceLocator.GetService<InteractableGUI>().ShowInteractString(interactableData.name, "Interact");
                        break;
                }
            }
        }

        protected IEnumerator OnUpdateInteractable()
        {
            InitData();
            while (true)
            {
                if (InSight() == true && IsClose() == true)
                {
                    ShowInteractGUI();
                    OnLookAt();
                }
                if (InSight() == false)
                {
                    ServiceLocator.GetService<InteractableGUI>().HideInteractString();
                }
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
