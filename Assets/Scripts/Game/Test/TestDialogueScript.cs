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

namespace ForgottenLegends.Game.Testing
{
    public class TestDialogueScript : IDialogue, IInteract, IGame
    {
        Interactable m_NPC;

        public TestDialogueScript()
        {
            Debug.Log("TestDialogueScript ctor");
        }

        public void OnGameLoad()
        {
            m_NPC = GameObject.Instantiate(Resources.Load<GameObject>("NPC")).GetComponent<Interactable>();
            Debug.Log($"{m_NPC} spawned");
        }

        public void OnGameSave()
        {

        }

        public void OnLook(Interactable interactable)
        {

        }

        public void OnInteract(Interactable interactable)
        {
            Debug.Log($"is {m_NPC} same as {interactable}?");
            if (interactable == m_NPC)
            {
                Debug.Log($"OnInteract({interactable.gameObject.name}) and StartDialogue");
                DialogueManager.Instance.StartDialogue(m_NPC.GetComponent<NPC>());
            }
            else
            {
                Debug.Log($"OnInteract({interactable.gameObject.name})");
            }
        }

        public void OnDialogueOption(string key)
        {
            Debug.Log($"OnDialogueOption({key})");
        }
    }
}
