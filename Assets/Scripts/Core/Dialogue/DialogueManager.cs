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

    private string ReadAsset(string folder, string fileName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, folder, fileName);

        if (!File.Exists(filePath))
        {
            Debug.LogError($"Cannot load dialogue data for {fileName}.");
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
        m_CurrentDiscussionComplete.Clear();
        string dataAsJson = ReadAsset("Dialogue", fileName);
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
        string dataAsJson = ReadAsset("Dialogue", fileName);
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
        string dataAsJson = ReadAsset("Dialogue", fileName);
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
                npcDialogue.text = StringUtility.EscapeString(LocalisationManager.Instance.getStringForKey(nextKey));
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

        Time.timeScale = 0;
        // Set the dialogue box active
        dialogueBoxHolder.SetActive(true);

        // Make sure that the dialogue choices are hidden by default
        dialogueOptionHolder.SetActive(false);
        choicesShown = false;

        // Set it to the gameobject name for now.
        npcName.text = npc.gameObject.name;

        // Set it to the correct NPC chat string.
        // TODO: Reference Key to get correct string for dialogue option in correct language
        ClearDialogueChoices();

        SetOptions(dialogueFile);
        TriggerAnotherDialogue(dialogueFile);

        for (int i = 0; i < m_DialogueOptions.Count; i++)
        {
            GameObject _gameObject = Instantiate(dialogOptionPrefab);
            _gameObject.transform.SetParent(dialogueOptionHolder.transform);
            _gameObject.GetComponentInChildren<TextMeshProUGUI>().text = LocalisationManager.Instance.getStringForKey(m_DialogueOptions[i]);
            _gameObject.GetComponent<DialogueOption>().optionKey = m_DialogueOptions[i];
            m_DialogueObjects.Add(_gameObject);
        }
        CameraScrolling.Instance.ToggleScrolling(true);
    }

    public void ExitDialogue()
    {
        // Set InDialogue to false
        InDialogue = false;

        for (int i = 0; i < m_DialogueObjects.Count; i++)
        {
            Destroy(m_DialogueObjects[i]);
        }

        // Set the dialogue box active
        dialogueBoxHolder.SetActive(false);
        CameraScrolling.Instance.ToggleScrolling(false);
        Time.timeScale = 1;
    }

    public void ExecuteDialogOption(string dialogueFile, string key)
    {
        ScriptManager.Instance.CallFunction("OnDialogueOption", new object[] { dialogueFile, key });
    }
     
    public void TriggerAnotherDialogue(string file)
    {
        SetDiscussion(file);
        ShowNextDiscussion();
    }

    public void ClearDialogueChoices()
    {
        for (int i = 0; i < m_DialogueObjects.Count; i++)
        {
            Destroy(m_DialogueObjects[i]);
        }
        m_DialogueObjects.Clear();
        m_DialogueOptions.Clear();
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
