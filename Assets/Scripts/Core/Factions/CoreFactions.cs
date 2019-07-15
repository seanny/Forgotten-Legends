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
using System.IO;
using UnityEngine;

namespace Core.Factions
{
    public class CoreFactions : Singleton<CoreFactions>
    {
        public const string FACTION_EXT = ".fctn";
        public const string FACTION_DIR = "Factions";

        public List<global::Core.Factions.Faction> factions;

        // Use this for initialization
        void Start()
        {
            LoadFactions();
        }

        private void LoadFactions()
        {
            string path = Path.Combine(Application.streamingAssetsPath, FACTION_DIR);
            string[] files = Directory.GetFiles(path);
            for (int i = 0; i < files.Length; i++)
            {
                if(Path.GetExtension(files[i]) != FACTION_EXT)
                {
                    continue;
                }
                string dataAsJson = File.ReadAllText(Path.Combine(path, files[i]));
                global::Core.Factions.Faction _fac = JsonUtility.FromJson<global::Core.Factions.Faction>(dataAsJson);
                factions.Add(_fac);
            }
        }
    }
}
