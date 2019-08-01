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
using Core.Player;
using UnityEngine;

namespace Core.Inventory
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        private KeyCode m_InventoryKey;
        private bool m_Enabled;

        private void Start()
        {
            m_InventoryKey = KeyCode.I;
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyUp(m_InventoryKey))
            {
                if (m_Enabled)
                {
                    m_Enabled = false;
                    HideInventory();
                }
                else
                {
                    m_Enabled = true;
                    ShowInventory(PlayerManager.Instance.Player.actorInventory);
                }
            }
        }

        public void ShowInventory(EntityInventory entityInventory)
        {
            // Populate the list with the inventory from the users inventory, then show the UI
            InventoryUI.Instance.ShowInventoryList(entityInventory.inventoryItems);
        }

        public void HideInventory()
        {
            // Hide the inventory screen when no longer in use
            InventoryUI.Instance.HideInventoryList();
        }
    }
}