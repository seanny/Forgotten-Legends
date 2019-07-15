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
using Core.Utility;
using UnityEngine;

namespace Core.Localisation
{
    public class LocalisationManager : Singleton<LocalisationManager>
    {
        private Dictionary<string, string> m_LocalisationInfo;
        private SystemLanguage m_SystemLanguage;
        public bool isReady { get; private set; }

        private void Start()
        {
            InitIfNotAlready();
        }

        private void Initialise()
        {
            m_LocalisationInfo = new Dictionary<string, string>();
            m_SystemLanguage = Application.systemLanguage;
            AddLocalisedText(m_SystemLanguage.ToString() + ".json"); isReady = true;
        }

        public void AddLocalisedText(string fileName)
        {
            string jsonData = AssetUtility.ReadAsset("Locale", fileName);
            LocalisationData localisationData = JsonUtility.FromJson<LocalisationData>(jsonData);
            for (int i = 0; i < localisationData.items.Length; i++)
            {
                m_LocalisationInfo.Add(localisationData.items[i].key, localisationData.items[i].value);
            }
            isReady = true;
        }

        private void InitIfNotAlready()
        {
            if (m_LocalisationInfo == null)
            {
                Initialise();
            }
        }

        public string GetLocalisedString(string key)
        {
            string returnValue = key;
            InitIfNotAlready();
            foreach (var item in m_LocalisationInfo)
            {
                if (item.Key == key)
                {
                    returnValue = item.Value;
                }
            }
            return returnValue;
        }
    }
}

