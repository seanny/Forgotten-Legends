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
        private const string CORE_OGSD = "ForgottenLegends.ogsd";
        public List<string> loadOrder;

        public List<OGSDFile> dataFiles;

        private void Start()
        {
            string[] files;
            files = Directory.GetFiles(Application.streamingAssetsPath);
            loadOrder.Add(CORE_OGSD);
            foreach (var file in files)
            {
                if (Path.GetExtension(file) == ".ogsd"
                    && file != CORE_OGSD)
                {
                    loadOrder.Add(file);
                }
            }
            
            foreach (var item in loadOrder)
            {
                OGSDReader ogsdReader = new OGSDReader();
                ogsdReader.Open(Path.Combine(Application.streamingAssetsPath, item));
                dataFiles.Add(ogsdReader.dataFile);
            }
            
            //ScriptExec.Instance.RunMethod("OnGameLoad", new object[] { });
        }
    }
}
