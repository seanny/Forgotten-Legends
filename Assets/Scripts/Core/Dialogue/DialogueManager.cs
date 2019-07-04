//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//
using TMPro;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : Singleton<DialogueManager>
{
    // Determines if the player is currently in dialogue.
    public bool InDialogue { get; private set; }

    // The NPC that is the player is currently having a chat with.
    private NPC m_CurrentNPC;

    // References the dialogue option key, which will be used to get a localised string and also handle it's script response.
    private List<string> m_DialogueOptions;

    // Current NPC discussion key with whatever is first in queue shown first, then 2nd, 3rd, etc
    private Queue<string> m_CurrentDiscussion;

    // GUI objects
    [Header("Dialogue Settings")]
    public GameObject dialogueBoxHolder;
    public TextMeshProUGUI npcName;
    public TextMeshProUGUI npcDialogue;
    public GameObject dialogueOptionHolder;

    [Header("Dialogue Option Prefab")]
    public GameObject dialogOptionPrefab;

    private string ReadAsset(string folder, string fileName)
    {
        string langName = LocalisationManager.Instance.getCurrentLanguage().getLanguage().ToString();
        string filePath = Path.Combine(Application.streamingAssetsPath, folder, langName, fileName);

        if (!File.Exists(filePath))
        {
            Debug.LogError($"Cannot load dialogue data for {langName} language.");
            return null;
        }
        return File.ReadAllText(filePath);
    }

    /// <summary>
    /// Set the discussion conversation in order of chat.
    /// </summary>
    /// <param name="chatStrings">Chat strings.</param>
    public void SetDiscussion(string fileName)
    {
        m_CurrentDiscussion.Clear();
        string dataAsJson = ReadAsset("Dialogue", Path.Combine("Dialogue", fileName));
        DialogueData dialogueData = JsonUtility.FromJson<DialogueData>(dataAsJson);
        for (int i = 0; i < dialogueData.dialogueKey.Length; i++)
        {
            if(dialogueData.isOption[i] == false)
            {
                m_CurrentDiscussion.Enqueue(dialogueData.dialogueKey[i]);
            }
        }
    }

    /// <summary>
    /// Adds a dialogue option.
    /// </summary>
    /// <param name="optionKey">Option key.</param>
    public void AddOption(string fileName, string optionKey)
    {
        m_CurrentDiscussion.Clear();
        string dataAsJson = ReadAsset("Dialogue", Path.Combine("Dialogue", fileName));
        DialogueData dialogueData = JsonUtility.FromJson<DialogueData>(dataAsJson);
        for (int i = 0; i < dialogueData.dialogueKey.Length; i++)
        {
            if (dialogueData.dialogueKey[i] == optionKey && dialogueData.isOption[i] == false)
            {
                m_DialogueOptions.Add(dialogueData.dialogueKey[i]);
            }
        }
    }

    /// <summary>
    /// Starts the dialogue with the player.
    /// </summary>
    /// <param name="npc">NPC to chat with</param>
    public void StartDialogue(NPC npc)
    {
        // Set InDialogue to true
        InDialogue = true;

        // Set the dialogue box active
        dialogueBoxHolder.SetActive(true);

        // Set it to the gameobject name for now.
        npcName.text = npc.gameObject.name;

        // Set it to the correct NPC chat string.
        // TODO: Reference Key to get correct string for dialogue option in correct language
        npcDialogue.text = m_CurrentDiscussion.Peek();
        m_CurrentDiscussion.Dequeue();

        dialogueBoxHolder.SetActive(true);
        for(int i = 0; i < m_DialogueOptions.Count; i++)
        {
            GameObject _gameObject = Instantiate(dialogOptionPrefab);
            _gameObject.transform.SetParent(dialogueOptionHolder.transform);
        }
    }

    public void ExecuteDialogOption(string dialogueFile, string key)
    {
        ScriptManager.Instance.CallFunction("OnDialogueOption", new object[] { key });
    }
}
