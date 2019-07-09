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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestJournal : Singleton<QuestJournal>
{
    private const float NAME_FADE = 2.5f;
    private const float OBJECTIVE_FADE = 5f;

    public TextMeshProUGUI questName;
    public TextMeshProUGUI questObjective;

    public List<Quest> activeQuests;
    public List<Quest> completedQuests;

    public AudioSource m_QuestCompletedSource;
    public AudioSource m_QuestStartedSource;

    private void Start()
    {
        activeQuests = new List<Quest>();
        completedQuests = new List<Quest>();
        ImageUtils.SetAlpha(questName, 0.0f);
        ImageUtils.SetAlpha(questObjective, 0.0f);
        GiveQuest("Test01");
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            CompleteQuest("Test01");
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            CompleteQuest("Test01");
        }
    }

    public void GiveQuest(string questID)
    {
        if(!questID.EndsWith(".json", System.StringComparison.OrdinalIgnoreCase))
        {
            questID += ".json";
        }
        string dataAsJson = AssetUtility.ReadAsset("Quest", questID);
        Quest quest = JsonUtility.FromJson<Quest>(dataAsJson);
        activeQuests.Add(quest);

        // TODO: Show the quest added UI
        // Output: Started: Quest Name
        Debug.Log($"QuestStarted: {LocalisationManager.Instance.GetLocalisedString("QuestStarted")}");
        questName.text =
            $"{LocalisationManager.Instance.GetLocalisedString("QuestStarted")} {LocalisationManager.Instance.GetLocalisedString(quest.questName)}";

        questObjective.text = 
            $"{LocalisationManager.Instance.GetLocalisedString(quest.questDescription)}";

        ImageUtils.FadeAlpha(questName, 1.0f, 1.0f);
        ImageUtils.FadeAlpha(questObjective, 1.0f, 1.5f);
        StartCoroutine(FadeOutQuestName());
        StartCoroutine(FadeOutQuestObjective());
        m_QuestStartedSource.Play();
    }

    IEnumerator FadeOutQuestName()
    {
        yield return new WaitForSeconds(NAME_FADE);
        ImageUtils.FadeAlpha(questName, 0.0f, 1.0f);
    }

    IEnumerator FadeOutQuestObjective()
    {
        yield return new WaitForSeconds(OBJECTIVE_FADE);
        ImageUtils.FadeAlpha(questObjective, 0.0f, 1.5f);
    }

    public void CompleteQuest(string questID)
    {
        for (int i = 0; i < activeQuests.Count; i++)
        {
            if(activeQuests[i].questID == questID)
            {
                completedQuests.Add(activeQuests[i]);

                // TODO: Show the quest completed UI
                questName.text =
                    $"{LocalisationManager.Instance.GetLocalisedString("QuestCompleted")} {LocalisationManager.Instance.GetLocalisedString(activeQuests[i].questName)}";

                ImageUtils.FadeAlpha(questName, 1.0f, 1.0f);
                StartCoroutine(FadeOutQuestName());

                m_QuestCompletedSource.Play();
                activeQuests.RemoveAt(i);
            }
        }
    }
}
