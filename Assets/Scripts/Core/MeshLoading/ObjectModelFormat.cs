//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//

using System;
using UnityEngine;
using System.IO;
using System.IO.Compression;

public class ObjectModelFormat : Singleton<ObjectModelFormat>
{
    private static readonly string FileExt = ".omf";

    private void Start()
    {
        LoadObjectFile("Test.omf");
    }

    public void LoadObjectFile(string objectFile)
    {
        if (!objectFile.EndsWith(FileExt))
        {
            objectFile += FileExt;
        }

        string filePath = Path.Combine(Application.streamingAssetsPath, "Models", objectFile);
        if(!File.Exists(filePath))
        {
            Debug.LogError($"Object Model Format file {objectFile} does not exist on the local disk.");
            return;
        }

        using (ZipArchive zipArchive = ZipFile.OpenRead(filePath))
        {
            foreach (var zip in zipArchive.Entries)
            {
                Debug.Log($"Fullname: {zip.FullName}");
            }
        }
    }
}
