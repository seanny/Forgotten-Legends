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
using System.Linq;
using UnityEngine;
using Core.Interactable;
using Core.Scripting;

namespace Core.Inventory
{
    public class EntityInventory : MonoBehaviour
    {
        public List<InteractableData> inventoryItems => m_InventoryItems;

        [SerializeField] private List<InteractableData> m_InventoryItems;

        private void Start()
        {
            InitInventoryItems();
        }

        /// <summary>
        /// Initialise the inventory items list if null.
        /// </summary>
        private void InitInventoryItems()
        {
            // Initialise the inventory list if null
            if (m_InventoryItems == null)
            {
                m_InventoryItems = new List<InteractableData>();
            }
        }
        
        /// <summary>
        /// Add the interactable item to the inventory.
        /// </summary>
        /// <param name="interactable"></param>
        public void AddItem(Interactable.Interactable interactable)
        {
            InitInventoryItems();
            
            // Create instance of interactableData with the data from interactable
            InteractableData interactableData = interactable.interactableData;
            
            // Add the instance to the list
            m_InventoryItems.Add(interactableData);
            
            // Call OnAddItem in Lua
            ScriptManager.Instance.CallFunction("OnInventoryAddItem", new object[] { interactableData.name });
        }

        /// <summary>
        /// Clear the inventory
        /// </summary>
        public void Clear()
        {
            InitInventoryItems();
            foreach (var item in m_InventoryItems)
            {
                // Call OnRemoveItem in Lua
                ScriptManager.Instance.CallFunction("OnInventoryRemoveItem", new object[] { item.name });
                
                // Clear the inventory items
                m_InventoryItems.Remove(item);
            }
        }

        /// <summary>
        /// Remove a single instance with the same name as interactableData.
        /// </summary>
        /// <param name="interactableData"></param>
        public void RemoveSingleItem(InteractableData interactableData)
        {
            InitInventoryItems();
            foreach (var item in m_InventoryItems)
            {
                if (item.name == interactableData.name)
                {
                    // Call OnRemoveItem in Lua
                    ScriptManager.Instance.CallFunction("OnInventoryRemoveItem", new object[] { item.name });
                    
                    // Remove item from inventory
                    m_InventoryItems.Remove(item);
                    
                    // Break the loop
                    break;
                }
            }
        }
        
        /// <summary>
        /// Remove all instances with the same name as interactableData.
        /// </summary>
        /// <param name="interactableData"></param>
        public void RemoveAllItem(InteractableData interactableData)
        {
            InitInventoryItems();
            foreach (var item in m_InventoryItems)
            {
                // Check if the item name is the same as interactableData name
                if (item.name == interactableData.name)
                {
                    // Call OnRemoveItem in Lua 
                    ScriptManager.Instance.CallFunction("OnInventoryRemoveItem", new object[] { item.name });
                    
                    // Remove item from inventory
                    m_InventoryItems.Remove(item);
                }
            }
        }
        
        /// <summary>
        /// Remove an item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        public void RemoveItemAt(int index)
        {
            InitInventoryItems();
            // Check if inventoryItems[index] is not null
            if (m_InventoryItems.ElementAtOrDefault(index) != null)
            {
                // Call OnRemoveItem in Lua 
                ScriptManager.Instance.CallFunction("OnInventoryRemoveItem", new object[] { m_InventoryItems[index].name });
                
                // Remove item from inventory
                m_InventoryItems.RemoveAt(index);
            }
        }
    }
}