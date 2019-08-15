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
            string objectNormals = objectMetaFile.normalPng;
            string objectAO = objectMetaFile.ambientOcclusionPng;
            string objectHM = objectMetaFile.heightMapPng;
        
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
            
            string normalPath = string.Empty;
            if (objectNormals.Length > 0)
            {
                normalPath = Path.Combine(Application.streamingAssetsPath, "Models", objectNormals);
                if(!File.Exists(filePath))
                {
                    Debug.LogError($"Normal Map file {objectNormals}");
                    return null;
                }
                
            }
            
            string occlusionPath = string.Empty;
            if (objectAO.Length > 0)
            {
                occlusionPath = Path.Combine(Application.streamingAssetsPath, "Models", objectAO);
                if(!File.Exists(filePath))
                {
                    Debug.LogError($"Normal Map file {objectAO}");
                    return null;
                }
            }

            GameObject _gameObject = new OBJLoader().Load(filePath);
            if (!string.IsNullOrWhiteSpace(error))
            {
                Debug.LogError($"ObjectModelFormat Error: {error}");
                return null;
            }
        
            _gameObject.transform.parent = parentObject;
            if (normalPath.Length > 0)
            {
                _gameObject.GetComponentInChildren<Renderer>().material.SetTexture("_BumpMap", Utility.ImageUtils.LoadPNG(normalPath));
            }
            
            if (occlusionPath.Length > 0)
            {
                _gameObject.GetComponentInChildren<Renderer>().material.SetTexture("_OcclusionMap", Utility.ImageUtils.LoadPNG(occlusionPath));
            }

            return _gameObject;
        }
    }
}
