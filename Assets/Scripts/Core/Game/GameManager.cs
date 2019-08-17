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
using Core.DataFormat;
using UnityEngine;

namespace Core.Game
{
    public class GameManager : Singleton<GameManager>
    {
        public List<OGSDFile> dataFiles;
        
        private void Start()
        {
            OGSDReader ogsdReader = new OGSDReader();
            ogsdReader.Open(Path.Combine(Application.streamingAssetsPath, "ForgottenLegends.ogsd"));
            for (int i = 0; i < ogsdReader.dataFile.npcBases.Count; i++)
            {
                Debug.Log($"{ogsdReader}");
            }
            //ScriptExec.Instance.RunMethod("OnGameLoad", new object[] { });
        }
    }
}
