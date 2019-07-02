//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//
using UnityEngine;
using System.IO;

public class ObjectModelFormat : MonoBehaviour
{
    public static readonly string FILE_EXT = ".omf";

    public void LoadObjectFile(string objectFile)
    {
        if(!File.Exists(objectFile))
        {
            Debug.LogError($"Object Model Format file {objectFile} does not exist on the local disk.");
            return;
        }


    }
}
