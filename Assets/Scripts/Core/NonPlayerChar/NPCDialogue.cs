//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//

using Core.Player;
using Core.Services;
using UnityEngine;

namespace Core.NonPlayerChar
{
    public class NPCDialogue : MonoBehaviour
    {
        private NPC m_ParentScript;
        private bool m_AttemptingDialogueWithPlayer = false;

        private void Start()
        {
            m_ParentScript = GetComponent<NPC>();
        }

        public void AttemptDialogueWithPlayer(string dialogueFile, string dialogueKey)
        {
            if(IsAttemptingDialogueAndCloseToPlayer() == true)
            {

            }
            else
            {
                m_ParentScript.m_MovementScript.MoveTowardsActor(ServiceLocator.GetService<PlayerManager>().GetPlayer());
            }
        }

        private bool IsAttemptingDialogueAndCloseToPlayer()
        {
            if (m_AttemptingDialogueWithPlayer == false)
            {
                if (m_ParentScript.m_MovementScript.IsAtActor(ServiceLocator.GetService<PlayerManager>().GetPlayer()))
                {
                    // Show dialogue Menu
                    return true;
                }
            }
            return false;
        }

        /*private void Update()
        {
            if(IsAttemptingDialogueAndCloseToPlayer() == true)
            {

            }
        }*/
    }
}
