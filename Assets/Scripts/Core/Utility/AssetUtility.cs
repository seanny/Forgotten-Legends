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
using UnityEngine;

namespace Core.Utility
{
    public static class AssetUtility
    {
        public static string ReadAsset(string folder, string fileName)
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, folder, fileName);

            if (!File.Exists(filePath))
            {
                Logging.LogError($"Cannot load asset data for {filePath}.");
                return null;
            }
            return File.ReadAllText(filePath);
        }
    }
}
