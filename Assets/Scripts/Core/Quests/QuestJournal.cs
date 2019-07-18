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
using System.Collections.Generic;
using System.Linq;
using Core.Localisation;
using Core.Utility;
using TMPro;
using UnityEngine;

namespace Core.Quests
{
    public class QuestJournal : Singleton<QuestJournal>
    {
        private const float NAME_FADE = 2.5f;
        private const float OBJECTIVE_FADE = 5f;

        public TextMeshProUGUI questName;
        public TextMeshProUGUI questObjective;

        public Dictionary<Quest, string> activeQuests;
        public List<Quest> completedQuests;

        public AudioSource m_QuestCompletedSource;
        public AudioSource m_QuestStartedSource;

        private void Start()
        {
            InitQuestJournal();
        }

        private void InitQuestJournal()
        {
            activeQuests = new Dictionary<Quest, string>();
            completedQuests = new List<Quest>();
            ImageUtils.SetAlpha(questName, 0.0f);
            ImageUtils.SetAlpha(questObjective, 0.0f);
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                GiveQuest("Test01");
            }
            if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                SetQuestObjective("Test01", "Test01_Obj01");
            }
            if (Input.GetKeyUp(KeyCode.Alpha3))
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
            activeQuests.Add(quest, quest.objectiveDescriptions[0]);

            // TODO: Show the quest added UI
            // Output: Started: Quest Name
            questName.text =
                $"{LocalisationManager.Instance.GetLocalisedString("QuestStarted")} {LocalisationManager.Instance.GetLocalisedString(quest.questName)}";

            questObjective.text = 
                $"{LocalisationManager.Instance.GetLocalisedString(quest.questDescription)}";

            FadeInName();
            FadeInObjective();
            m_QuestStartedSource.Play();
        }

        private void FadeInName()
        {
            ImageUtils.FadeAlpha(questName, 1.0f, 0.5f);
            StartCoroutine(FadeOutQuestName());
        }

        private void FadeInObjective()
        {
            ImageUtils.FadeAlpha(questObjective, 1.0f, 0.5f);
            StartCoroutine(FadeOutQuestObjective());
        }

        public void SetQuestObjective(string questID, string questObjectiveID)
        {
            if (activeQuests == null)
            {
                InitQuestJournal();
            }
            for (int i = 0; i < activeQuests.Count; i++)
            {
                Debug.Log($"Item = {activeQuests.ElementAt(i).Key.questID}, {activeQuests.ElementAt(i).Value}");
                if (activeQuests.ElementAt(i).Key.questID == questID)
                {
                    for (int x = 0; x < activeQuests.ElementAt(i).Key.objectiveNames.Length; x++)
                    {
                        if(activeQuests.ElementAt(i).Key.objectiveNames[x] == questObjectiveID)
                        {
                            activeQuests[activeQuests.ElementAt(i).Key] = questObjectiveID;

                            questObjective.text =
                                $"{LocalisationManager.Instance.GetLocalisedString(questObjectiveID)}";
                            FadeInObjective();
                            break;
                        }
                    }
                }
            }
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
            for(int i = 0; i < activeQuests.Count; i++)
            {
                if(activeQuests.ElementAt(i).Key.questID == questID)
                {
                    completedQuests.Add(activeQuests.ElementAt(i).Key);

                    // TODO: Show the quest completed UI
                    questName.text =
                        $"{LocalisationManager.Instance.GetLocalisedString("QuestCompleted")} {LocalisationManager.Instance.GetLocalisedString(activeQuests.ElementAt(i).Key.questName)}";

                    FadeInName();

                    m_QuestCompletedSource.Play();
                    activeQuests.Remove(activeQuests.ElementAt(i).Key);
                }
            }
        }
    }
}
