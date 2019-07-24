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
using Core.MathUtil;

namespace Core.Interactable
{
    [Serializable]
    public class InteractableData
    {
        public enum InteractType
        {
            Generic = 0,
            Talk,
            Take
        }

        public enum InteractableCategory
        {
            Other = 0,
            Weapon,
            Armour,
            Potion,
            Food,
            Magic,
            Book,
            Key
        };

        public InteractType type;
        public InteractableCategory category;
        public Vec3 position;
        public string name;
    }
}