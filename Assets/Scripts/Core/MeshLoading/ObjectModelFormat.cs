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
using System.IO;
using Core.Utility;
using Dummiesman;
using UnityEngine;

namespace Core.MeshLoading
{
    public class ObjectModelFormat : Singleton<ObjectModelFormat>
    {
        private static readonly string FileExt = ".obj";
        private static readonly string TextureExt = ".png";
    
        public Transform parentObject;
        public Shader shader;
    
        private string error = String.Empty;

        public GameObject LoadObjectFile(string objectMeta)
        {
            string json = AssetUtility.ReadAsset("Models", objectMeta);
            ObjectMetaFile objectMetaFile = JsonUtility.FromJson<ObjectMetaFile>(json);

            string objectMesh = objectMetaFile.meshObj;
            string objectTexture = objectMetaFile.texturePng;
        
            string filePath = Path.Combine(Application.streamingAssetsPath, "Models", objectMesh);
            if(!File.Exists(filePath))
            {
                Debug.LogError($"Object Model Format file {objectMesh} does not exist on the local disk.");
                return null;
            }
        
            string texturePath = Path.Combine(Application.streamingAssetsPath, "Models", objectTexture);
            if(!File.Exists(filePath))
            {
                Debug.LogError($"Object Model Format file {objectMesh} does not exist on the local disk.");
                return null;
            }

            GameObject _gameObject = new OBJLoader().Load(filePath);
            if (!string.IsNullOrWhiteSpace(error))
            {
                Debug.LogError($"ObjectModelFormat Error: {error}");
                return null;
            }
        
            _gameObject.transform.parent = parentObject;

            return _gameObject;
        }
    }
}
