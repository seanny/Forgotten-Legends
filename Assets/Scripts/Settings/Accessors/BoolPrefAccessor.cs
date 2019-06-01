//
//  Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
//  This document is the property of Outlaw Games Studio.
//  It is considered confidential and proprietary.
//
//  This document may not be reproduced or transmitted in any form
//  without the consent of Outlaw Games Studio.
//
using UnityEngine;

/// <summary>
/// Get and set bool values.
/// </summary>
public class BoolPrefAccessor : PrefAccessor<bool> 
{
    /// <summary>
    /// Get a bool value from PlayerPrefs.
    /// </summary>
    /// <param name="prefKey">The key to retrieve the value for.</param>
    /// <param name="defaultValue">
    /// The default value to return if the key doesn't exist. If not specified it will be the built-in default.
    /// </param>
    /// <returns>The bool value stored at the key prefKey or if not present then the built-in default.</returns>
    public bool Get(string prefKey, bool defaultValue = default(bool)) 
    {
        var prefValue = PlayerPrefs.GetInt(prefKey, boolToInt(defaultValue));
        return prefValue == 1 ? true : false;
    }

    /// <summary>
    /// Set a bool value into PlayerPrefs.
    /// </summary>
    /// <param name="prefKey">The key to set a value for.</param>
    /// <param name="prefValue">The value to set.</param>
    /// <returns>This accessor.</returns>
    public PrefAccessor<bool> Set(string prefKey, bool prefValue) 
    {
        PlayerPrefs.SetInt(prefKey, boolToInt(prefValue));
        return this;
    }

    private int boolToInt(bool prefValue) 
    {
        return prefValue ? 1 : 0;
    }
}
