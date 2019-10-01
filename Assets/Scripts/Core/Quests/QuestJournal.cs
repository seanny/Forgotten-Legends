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
using Core.Services;
using Core.Utility;
using TMPro;
using UnityEngine;

namespace Core.Quests
{
    public class QuestJournal : Singleton<QuestJournal>
    {
        private const float NAME_FADE = 0.5f;
        private const float NAME_FADE_OUT = 1.0f;
        private const float OBJECTIVE_FADE = 0.5f;
        private const float OBJECTIVE_FADE_OUT = 1.5f;

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

#if UNITY_DEVELOPMENT || UNITY_EDITOR
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
                CompleteQuest("Test01", false);
            }
            if (Input.GetKeyUp(KeyCode.Alpha4))
            {
                CompleteQuest("Test01", true);
            }
        }
#endif

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
                $"{ServiceLocator.GetService<LocalisationManager>().GetLocalisedString("QuestStarted")} {ServiceLocator.GetService<LocalisationManager>().GetLocalisedString(quest.questName)}";

            questObjective.text = 
                $"{ServiceLocator.GetService<LocalisationManager>().GetLocalisedString(quest.questDescription)}";

            FadeInName();
            FadeInObjective();
            m_QuestStartedSource.Play();
        }

        private void FadeInName()
        {
            ImageUtils.FadeAlpha(questName, 1.0f, NAME_FADE);
            StartCoroutine(FadeOutQuestName());
        }

        private void FadeInObjective()
        {
            ImageUtils.FadeAlpha(questObjective, 1.0f, OBJECTIVE_FADE);
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
                if (activeQuests.ElementAt(i).Key.questID == questID)
                {
                    for (int x = 0; x < activeQuests.ElementAt(i).Key.objectiveNames.Length; x++)
                    {
                        if(activeQuests.ElementAt(i).Key.objectiveNames[x] == questObjectiveID)
                        {
                            activeQuests[activeQuests.ElementAt(i).Key] = questObjectiveID;

                            questObjective.text =
                                $"{ServiceLocator.GetService<LocalisationManager>().GetLocalisedString(questObjectiveID)}";
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
            ImageUtils.FadeAlpha(questName, 0.0f, NAME_FADE_OUT);
        }

        IEnumerator FadeOutQuestObjective()
        {
            yield return new WaitForSeconds(OBJECTIVE_FADE);
            ImageUtils.FadeAlpha(questObjective, 0.0f, OBJECTIVE_FADE_OUT);
        }

        public void CompleteQuest(string questID, bool isFailed)
        {
            for(int i = 0; i < activeQuests.Count; i++)
            {
                if(activeQuests.ElementAt(i).Key.questID == questID)
                {
                    completedQuests.Add(activeQuests.ElementAt(i).Key);

                    if (isFailed)
                    {
                        // Show the quest completed UI
                        questName.text =
                            $"{ServiceLocator.GetService<LocalisationManager>().GetLocalisedString("QuestFailed")} ";
                    }
                    else
                    {
                        // Show the quest failed UI
                        questName.text =
                            $"{ServiceLocator.GetService<LocalisationManager>().GetLocalisedString("QuestCompleted")} ";
                    }

                    questName.text +=
                        $"{ServiceLocator.GetService<LocalisationManager>().GetLocalisedString(activeQuests.ElementAt(i).Key.questName)}";
                    
                    FadeInName();

                    m_QuestCompletedSource.Play();
                    activeQuests.Remove(activeQuests.ElementAt(i).Key);
                }
            }
        }
    }
}
