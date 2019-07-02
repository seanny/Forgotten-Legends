//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class BlendshapeSlider : MonoBehaviour
{
    [Header("Name w/o Suffix (e.g. Cheek)")]
    public string blendShapeName;
    private Slider m_Slider;

    private void Start()
    {
        blendShapeName = blendShapeName.Trim();
        m_Slider = GetComponent<Slider>();

        m_Slider.onValueChanged.AddListener(delegate {
        

        });
    }
}
