//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//

using System.Collections.Generic;
using Core.Camera;
using Core.Localisation;
using Core.NonPlayerChar;
using Core.Player;
using Core.Scripting;
using Core.Services;
using Core.Utility;
using TMPro;
using UnityEngine;

namespace Core.Dialogue
{
    public class DialogueManager : Singleton<DialogueManager>
    {
        // Determines if the player is currently in dialogue.
        public bool InDialogue { get; private set; }

        // The NPC that is the player is currently having a chat with.
        public NPC m_CurrentNPC;

        // References the dialogue option key, which will be used to get a localised string and also handle it's script response.
        private List<string> m_DialogueOptions;
        private List<GameObject> m_DialogueObjects;

        // Current NPC discussion key with whatever is first in queue shown first, then 2nd, 3rd, etc
        private List<string> m_CurrentDiscussion;
        private List<bool> m_CurrentDiscussionComplete;

        // GUI objects
        [Header("Dialogue Settings")]
        public GameObject dialogueBoxHolder;
        public TextMeshProUGUI npcName;
        public TextMeshProUGUI npcDialogue;
        public GameObject dialogueOptionHolder;
        public bool choicesShown;

        [Header("Dialogue Option Prefab")]
        public GameObject dialogOptionPrefab;

        private void Start()
        {
            m_CurrentDiscussion = new List<string>();
            m_CurrentDiscussionComplete = new List<bool>();
            m_DialogueOptions = new List<string>();
            m_DialogueObjects = new List<GameObject>();
        }

        private void Update()
        {
            if(InDialogue == true && choicesShown == false)
            {
                if(Input.GetKeyUp(KeyCode.Space))
                {
                    ShowNextDiscussion();
                }
            }
        }

        /// <summary>
        /// Set the discussion conversation in order of chat.
        /// </summary>
        /// <param name="chatStrings">Chat strings.</param>
        public void SetDiscussion(string fileName)
        {
            m_CurrentDiscussion.Clear();
            m_CurrentDiscussionComplete.Clear();
            string dataAsJson = AssetUtility.ReadAsset("Dialogue", fileName);
            DialogueData dialogueData = JsonUtility.FromJson<DialogueData>(dataAsJson);
            for (int i = 0; i < dialogueData.discussion.Length; i++)
            {
                m_CurrentDiscussion.Add(dialogueData.discussion[i]);
                m_CurrentDiscussionComplete.Add(false);
            }
        }

        public void SetOptions(string fileName)
        {
            m_CurrentDiscussion.Clear();
            m_CurrentDiscussionComplete.Clear();
            string dataAsJson = AssetUtility.ReadAsset("Dialogue", fileName);
            DialogueData dialogueData = JsonUtility.FromJson<DialogueData>(dataAsJson);
            for (int i = 0; i < dialogueData.options.Length; i++)
            {
                m_DialogueOptions.Add(dialogueData.options[i]);
            }
        }

        /// <summary>
        /// Adds a dialogue option.
        /// </summary>
        /// <param name="optionKey">Option key.</param>
        public void AddOption(string fileName, string optionKey)
        {
            string dataAsJson = AssetUtility.ReadAsset("Dialogue", fileName);
            DialogueData dialogueData = JsonUtility.FromJson<DialogueData>(dataAsJson);
            for (int i = 0; i < dialogueData.options.Length; i++)
            {
                if (dialogueData.options[i] == optionKey)
                {
                    m_DialogueOptions.Add(dialogueData.options[i]);
                }
            }
        }

        private void ShowNextDiscussion()
        {
            for (int i = 0; i < m_CurrentDiscussion.Count; i++)
            {
                if(m_CurrentDiscussionComplete[i] == false)
                {
                    string nextKey = m_CurrentDiscussion[i];
                    m_CurrentDiscussionComplete[i] = true;
                    npcDialogue.text = StringUtility.EscapeString(ServiceLocator.GetService<LocalisationManager>().GetLocalisedString(nextKey));
                    ScriptManager.Instance.CallFunction("OnDialogueContinue", new object[] { nextKey });
                    break;
                }
            }
        }

        /// <summary>
        /// Starts the dialogue with the player.
        /// </summary>
        /// <param name="npc">NPC to chat with</param>
        public void StartDialogue(NPC npc, string dialogueFile)
        {
            if(InDialogue == true)
            {
                return;
            }

            // Set InDialogue to true
            InDialogue = true;

            choicesShown = false;

            // Set it to the gameobject name for now.
            npcName.text = npc.gameObject.name;
            m_CurrentNPC = npc;

            // Set it to the correct NPC chat string.
            // TODO: Reference Key to get correct string for dialogue option in correct language
            TriggerAnotherDialogue(dialogueFile);

            CameraScrolling.Instance.ToggleScrolling(true);
            CameraRotation.Instance.ToggleRotation(true);

            // Make sure that NPC is facing the player
            m_CurrentNPC.m_MovementScript.StopMovement();
            m_CurrentNPC.m_MovementScript.LookAtActor(ServiceLocator.GetService<PlayerManager>().GetPlayer());
            ServiceLocator.GetService<PlayerManager>().GetPlayer().transform.LookAt(m_CurrentNPC.transform);
        }

        private void ClearDialogueOptions()
        {
            for (int i = 0; i < m_DialogueObjects.Count; i++)
            {
                Destroy(m_DialogueObjects[i]);
            }
            m_DialogueObjects.Clear();
            m_DialogueOptions.Clear();
            ToggleDialogueUI(false);
            ToggleDialogueChoicesUI(false);
        }

        private void ToggleDialogueUI(bool toggle)
        {
            dialogueBoxHolder.SetActive(toggle);
        }

        private void ToggleDialogueChoicesUI(bool toggle)
        {
            dialogueOptionHolder.SetActive(toggle);
        }

        public void ExitDialogue(bool keepNPC)
        {
            // Set InDialogue to false
            InDialogue = false;
            m_CurrentNPC.m_MovementScript.StartMovement();
        
            ClearDialogueOptions();
            CameraScrolling.Instance.ToggleScrolling(false);
            CameraRotation.Instance.ToggleRotation(false);
        }

        public void ExecuteDialogOption(string dialogueFile, string key)
        {
            ScriptManager.Instance.CallFunction("OnDialogueOption", new object[] { dialogueFile, key });
        }
     
        public void TriggerAnotherDialogue(string file)
        {
            ClearDialogueChoices();
            SetOptions(file);
            SetDiscussion(file);
            ShowNextDiscussion();

            for (int i = 0; i < m_DialogueOptions.Count; i++)
            {
                GameObject _gameObject = Instantiate(dialogOptionPrefab);
                _gameObject.transform.SetParent(dialogueOptionHolder.transform);
                _gameObject.GetComponentInChildren<TextMeshProUGUI>().text = ServiceLocator.GetService<LocalisationManager>().GetLocalisedString(m_DialogueOptions[i]);
                _gameObject.GetComponent<DialogueOption>().optionKey = m_DialogueOptions[i];
                m_DialogueObjects.Add(_gameObject);
            }

            ToggleDialogueUI(true);
            ToggleDialogueChoicesUI(false);
        }

        public void ClearDialogueChoices()
        {
            ClearDialogueOptions();
            choicesShown = false;
        }

        public void ShowDialogueChoices(bool toggle)
        {
            choicesShown = toggle;
            if (choicesShown == true)
            {
                dialogueOptionHolder.SetActive(true);
            }
            else
            {
                dialogueOptionHolder.SetActive(false);
            }
        }
    }
}
