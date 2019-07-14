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
using Dummiesman;

public class ObjectModelFormat : Singleton<ObjectModelFormat>
{
    private static readonly string FileExt = ".obj";
    private static readonly string TextureExt = ".png";
    
    public Transform parentObject;
    public Shader shader;
    
    private string error = String.Empty;

    private void Start()
    {
        LoadObjectFile("TestObject/TestObj.json");
    }

    public void LoadObjectFile(string objectMeta)
    {
        if (!objectMeta.EndsWith(FileExt))
        {
            //objectMeta += FileExt;
        }

        string json = AssetUtility.ReadAsset("Models", objectMeta);
        ObjectMetaFile objectMetaFile = JsonUtility.FromJson<ObjectMetaFile>(json);

        string objectMesh = objectMetaFile.meshObj;
        string objectTexture = objectMetaFile.texturePng;
        
        string filePath = Path.Combine(Application.streamingAssetsPath, "Models", objectMesh);
        if(!File.Exists(filePath))
        {
            Debug.LogError($"Object Model Format file {objectMesh} does not exist on the local disk.");
            return;
        }
        
        string texturePath = Path.Combine(Application.streamingAssetsPath, "Models", objectTexture);
        if(!File.Exists(filePath))
        {
            Debug.LogError($"Object Model Format file {objectMesh} does not exist on the local disk.");
            return;
        }

        GameObject _gameObject = new OBJLoader().Load(filePath);
        if (!string.IsNullOrWhiteSpace(error))
        {
            Debug.LogError($"ObjectModelFormat Error: {error}");
        }
    }
}
