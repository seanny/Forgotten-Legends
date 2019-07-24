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
using System.Collections.Generic;
using UnityEngine;
using Core.Interactable;

namespace Core.Inventory
{
    public class EntityInventory : MonoBehaviour
    {
        public List<InteractableData> inventoryItems { get; private set; }

        private void Start()
        {
            inventoryItems = new List<InteractableData>();
        }

        public void ShowInventory(bool show)
        {
            InventoryUI.Instance.interactableDataList.Clear();
            InventoryUI.Instance.interactableDataList = inventoryItems;
            
            // List all items in inventory and then show it inside of a GUI
            InventoryUI.Instance.ToggleUI(show);
        }

        public void AddItem(Interactable.Interactable interactable)
        {
            InteractableData interactableData = interactable.InteractableData;
            inventoryItems.Add(interactableData);
        }

        public void Clear()
        {
            inventoryItems.Clear();
        }

        public void RemoveItem(int index)
        {
            inventoryItems.RemoveAt(index);
        }
    }
}