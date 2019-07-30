//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//
using System.Collections.Generic;
using Core.Dialogue;
using Core.Interactable;
using Core.Utility;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Core.Inventory
{
    public class InventoryUI : Singleton<InventoryUI>
    {
        public GameObject buttonPrefab;
        
        public GameObject inventoryUIGameObject;
        public GameObject scrollListParent;
        public List<InteractableData> interactableDataList;

        public Button buttonAllItems;
        public Button buttonWeapons;
        public Button buttonArmour;
        public Button buttonPotion;
        public Button buttonFood;
        public Button buttonMagic;
        public Button buttonBooks;
        public Button buttonKeys;
        public Button buttonMisc;

        [SerializeField] private List<GameObject> m_Buttons;

        public void ShowInventoryList(List<InteractableData> interactableData)
        {
            interactableDataList.Clear();
            interactableDataList = interactableData;
            ToggleUI(true);
        }

        public void HideInventoryList()
        {
            ToggleUI(false);
        }
        
        private void ClearButtons()
        {
            for (int i = 0; i < m_Buttons.Count; i++)
            {
                Destroy(m_Buttons[i]);
            }
            m_Buttons.Clear();
        }

        private void CreateButtons()
        {
            for (int i = 0; i < interactableDataList.Count; i++)
            {
                GameObject button = Instantiate(buttonPrefab, scrollListParent.transform, true);
                button.GetComponent<DialogueOption>().optionKey = interactableDataList[i].name;
                m_Buttons.Add(button);
            }
        }
        
        private void CreateButtons(InteractableData.InteractableCategory category)
        {
            for (int i = 0; i < interactableDataList.Count; i++)
            {
                if (interactableDataList[i].category == category)
                {
                    GameObject button = Instantiate(buttonPrefab, scrollListParent.transform, true);
                    button.GetComponent<TextMeshProUGUI>().text = interactableDataList[i].name;
                    ImageUtils.SetHeight(button.GetComponent<RectTransform>(), 100);
                    m_Buttons.Add(button);                    
                }
            }
        }

        private void ProcessOnClick()
        {
            ClearButtons();
            CreateButtons();
        }
        
        private void ProcessOnClick(InteractableData.InteractableCategory category)
        {
            ClearButtons();
            CreateButtons(category);
        }
        
        private void EnableUI(bool toggle)
        {
            inventoryUIGameObject.SetActive(toggle);
            PauseUtility.TogglePause(toggle);
        }

        public void ToggleUI(bool toggle)
        {
            ClearButtons();
            CreateButtons();
            EnableUI(toggle);
        }
        
        // Start is called before the first frame update
        private void Start()
        {
            m_Buttons = new List<GameObject>();
            buttonAllItems.onClick.AddListener(() => ProcessOnClick());
            buttonWeapons.onClick.AddListener(() => ProcessOnClick(InteractableData.InteractableCategory.Weapon));
            buttonArmour.onClick.AddListener(() => ProcessOnClick(InteractableData.InteractableCategory.Armour));
            buttonPotion.onClick.AddListener(() => ProcessOnClick(InteractableData.InteractableCategory.Potion));
            buttonFood.onClick.AddListener(() => ProcessOnClick(InteractableData.InteractableCategory.Food));
            buttonMagic.onClick.AddListener(() => ProcessOnClick(InteractableData.InteractableCategory.Magic));
            buttonBooks.onClick.AddListener(() => ProcessOnClick(InteractableData.InteractableCategory.Book));
            buttonKeys.onClick.AddListener(() => ProcessOnClick(InteractableData.InteractableCategory.Key));
            buttonMisc.onClick.AddListener(() => ProcessOnClick(InteractableData.InteractableCategory.Other));
        }
    }
}
