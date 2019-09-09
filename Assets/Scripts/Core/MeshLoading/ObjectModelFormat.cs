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
using Core.Interactable;
using Core.Utility;
using Core.World;
using Dummiesman;
using UnityEngine;

namespace Core.MeshLoading
{
    public class ObjectModelFormat : Singleton<ObjectModelFormat>
    {
        private static readonly string FileExt = ".obj";
        private static readonly string TextureExt = ".png";
    
        public Transform parentObject;

        public GameObject LoadObjectFile(string objectMeta)
        {
            string objMetaFile = objectMeta;
            if (Path.GetExtension(objectMeta) != "json")
            {
                objMetaFile = Path.ChangeExtension(objectMeta, "json");
            }
            string json = AssetUtility.ReadAsset("Models", Path.Combine(objectMeta, objMetaFile));
            ObjectMetaFile objectMetaFile = JsonUtility.FromJson<ObjectMetaFile>(json);

            string objectMesh = objectMetaFile.meshObj;
            string objectTexture = objectMetaFile.texturePng;
            string objectNormals = objectMetaFile.normalPng;
            string objectAO = objectMetaFile.ambientOcclusionPng;

            string filePath = Path.Combine(Application.streamingAssetsPath, "Models", objectMesh);
            if(!File.Exists(filePath))
            {
                Debug.LogError($"Object Model Format file {objectMesh} does not exist on the local disk.");
                return null;
            }

            string texturePath = Path.Combine(Application.streamingAssetsPath, "Models", objectTexture);
            if(!File.Exists(filePath))
            {
                Debug.LogError($"Diffuse (Colour) Texture file {texturePath} does not exist on the local disk.");
                return null;
            }
            
            string normalPath = string.Empty;
            if (objectNormals.Length > 0)
            {
                normalPath = Path.Combine(Application.streamingAssetsPath, "Models", objectNormals);
                if(!File.Exists(filePath))
                {
                    Debug.LogError($"Normal Map Texture file {objectNormals} does not exist on the local disk.");
                    return null;
                }
                
            }
            
            string occlusionPath = string.Empty;
            if (objectAO.Length > 0)
            {
                occlusionPath = Path.Combine(Application.streamingAssetsPath, "Models", objectAO);
                if(!File.Exists(filePath))
                {
                    Debug.LogError($"Ambient Occlusion Texture file {objectAO} does not exist on the local disk.");
                    return null;
                }
            }

            GameObject _gameObject = new OBJLoader().Load(filePath);

            _gameObject.transform.parent = parentObject;

            Material material = _gameObject.GetComponentInChildren<Renderer>().material;
            
            if (texturePath.Length > 0)
            {
                material.SetTexture("_BaseMap", Utility.ImageUtils.LoadPNG(texturePath));
            }
            
            if (normalPath.Length > 0)
            {
                material.SetTexture("_BumpMap", Utility.ImageUtils.LoadPNG(normalPath));
            }
            
            if (occlusionPath.Length > 0)
            {
                material.SetTexture("_OcclusionMap", Utility.ImageUtils.LoadPNG(occlusionPath));
            }

            AddTimedIfPossible(objectMetaFile, _gameObject);
            AddNavMeshSourceIfPossible(objectMetaFile, _gameObject);
            AddInteractionIfPossible(objectMetaFile, _gameObject);
            AddLightIfPossible(objectMetaFile, _gameObject);
            AddRigidbodyIfPossible(objectMetaFile, _gameObject);
            AddColliderIfPossible(objectMetaFile, _gameObject);

            return _gameObject;
        }

        private void AddTimedIfPossible(ObjectMetaFile objectItem, GameObject gameObject)
        {
            // FIXME: TimedObject script does not seem to work at the moment.
            if (objectItem.timed.isEnabled == true)
            {
//                gameObject.AddComponent<TimedObject>();
//                gameObject.GetComponent<TimedObject>().activationTime = objectItem.timed.activationHour;
//                gameObject.GetComponent<TimedObject>().deactivationTime = objectItem.timed.deactivationHour;
            }
        }

        private void AddNavMeshSourceIfPossible(ObjectMetaFile objectItem, GameObject gameObject)
        {
            if (objectItem.isWalkable == true)
            {
                gameObject.AddComponent<NavMeshSourceTag>();
            }
        }
        
        private void AddInteractionIfPossible(ObjectMetaFile objectItem, GameObject gameObject)
        {
            if (objectItem.interactable.isInteractable == true)
            {
                gameObject.AddComponent<Interactable.Interactable>();
                gameObject.GetComponent<Interactable.Interactable>().SetInteractableName(objectItem.objectName);
                switch (objectItem.interactable.interactableType)
                {
                    case 0:
                        gameObject.GetComponent<Interactable.Interactable>().interactableData.category = InteractableData.InteractableCategory.Weapon;
                        break;
                    case 1:
                        gameObject.GetComponent<Interactable.Interactable>().interactableData.category = InteractableData.InteractableCategory.Armour;
                        break;
                    case 2:
                        gameObject.GetComponent<Interactable.Interactable>().interactableData.category = InteractableData.InteractableCategory.Book;
                        break;
                    case 3:
                        gameObject.GetComponent<Interactable.Interactable>().interactableData.category = InteractableData.InteractableCategory.Food;
                        break;
                    case 4:
                        gameObject.GetComponent<Interactable.Interactable>().interactableData.category = InteractableData.InteractableCategory.Key;
                        break;
                    case 5:
                        gameObject.GetComponent<Interactable.Interactable>().interactableData.category = InteractableData.InteractableCategory.Magic;
                        break;
                    case 6:
                        gameObject.GetComponent<Interactable.Interactable>().interactableData.category = InteractableData.InteractableCategory.Other;
                        break;
                    case 7:
                        gameObject.GetComponent<Interactable.Interactable>().interactableData.category = InteractableData.InteractableCategory.Potion;
                        break;
                    case 8:
                        gameObject.GetComponent<Interactable.Interactable>().interactableData.category = InteractableData.InteractableCategory.Other;
                        break;
                }
            }
        }
        
        private void AddColliderIfPossible(ObjectMetaFile objectItem, GameObject gameObject)
        {
            if (objectItem.collider.isEnabled == true)
            {
                gameObject.AddComponent<MeshCollider>();
                gameObject.GetComponent<MeshCollider>().isTrigger = objectItem.collider.isTrigger;
            }
        }
        
        private void AddLightIfPossible(ObjectMetaFile objectItem, GameObject gameObject)
        {
            if (objectItem.light.isEnabled == true)
            {
                gameObject.AddComponent<Light>();
                if (objectItem.light.isSpotlight == true)
                {
                    gameObject.GetComponent<Light>().type = LightType.Spot;
                }
                else
                {
                    gameObject.GetComponent<Light>().type = LightType.Point;
                }

                gameObject.GetComponent<Light>().range = objectItem.light.range;
                gameObject.GetComponent<Light>().spotAngle = objectItem.light.angle;
                gameObject.GetComponent<Light>().intensity = objectItem.light.intensity;
                gameObject.GetComponent<Light>().color = new Color(
                    objectItem.light.colour.r,
                    objectItem.light.colour.g,
                    objectItem.light.colour.b
                );
            }
        }
        
        private void AddRigidbodyIfPossible(ObjectMetaFile objectItem, GameObject gameObject)
        {
            if (objectItem.rigidbody.isEnabled == true)
            {
                gameObject.AddComponent<Rigidbody>();
                gameObject.GetComponent<Rigidbody>().drag = objectItem.rigidbody.drag;
                gameObject.GetComponent<Rigidbody>().mass = objectItem.rigidbody.mass;
                gameObject.GetComponent<Rigidbody>().position = new Vector3(
                    objectItem.rigidbody.position.x,
                    objectItem.rigidbody.position.y,
                    objectItem.rigidbody.position.z
                );
                gameObject.GetComponent<Rigidbody>().rotation = new Quaternion(
                    objectItem.rigidbody.rotation.x,
                    objectItem.rigidbody.rotation.y,
                    objectItem.rigidbody.rotation.z,
                    objectItem.rigidbody.rotation.w
                );
            }
        }
    }
}
