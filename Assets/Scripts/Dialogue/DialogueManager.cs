//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : Singleton<DialogueManager>
{
    public GameObject dialogueBox;
    public GameObject menu;
    public GameObject buttonPrefab;

    public Text nameText;
    public Text dialogueText;

    public List<GameObject> options;

    private Queue<string> sentences;
    private string dialogFileName;

    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>();
    }

    public Dialogue StartDialogue(string npcName, string dialogFileName, ref Dialogue dialogue)
    {
        Debug.Log($"Starting dialogue with {gameObject.name}");
        sentences.Clear();

        string path = Path.Combine(Application.streamingAssetsPath, "Dialogue", dialogFileName);
        if(!File.Exists(path))
        {
            Debug.LogError($"File {dialogFileName} does not exist.");
        }

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        nameText.text = npcName;
        dialogueBox.SetActive(true);
        return dialogue;
    }

    public void ShowSentence(string sentence)
    {
        dialogueText.text = sentence;
    }

    public void AddOption(string option, string resultingDialogue)
    {
        GameObject button = Instantiate(buttonPrefab);
        button.transform.parent = menu.transform;
        DialogueOption dialogueOption = button.GetComponent<DialogueOption>();
        dialogueOption.dialogueFile = dialogFileName;
        dialogueOption.resultingDialogue = resultingDialogue;
        dialogueOption.optionText = option;
        options.Add(button);
    }
}
