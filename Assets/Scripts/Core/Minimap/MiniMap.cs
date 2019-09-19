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
using Core.Services;
using Core.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Minimap
{
    public class MiniMap : MonoBehaviour, IService
    {
        public Transform player;
        
        [SerializeField]
        private GameObject minimapUI;

        private void Start()
        {
            player = GameObject.FindWithTag("Player").GetComponent<Transform>();
            minimapUI = GameObject.FindWithTag("MiniMap");
            ServiceLocator.AddService(this);
        }

        private void LateUpdate()
        {
            Vector3 newPos = player.position;
            newPos.y = transform.position.y;
            transform.position = newPos;
        }

        public void ToggleMinimap(bool toggle)
        {
            minimapUI.SetActive(toggle);
        }

        public void OnStart()
        {
            
        }

        public void OnEnd()
        {
            
        }
    }
}
