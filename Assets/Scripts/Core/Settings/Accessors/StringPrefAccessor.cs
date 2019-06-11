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
/// Get and set string values.
/// </summary>
public class StringPrefAccessor : PrefAccessor<string>
{
    /// <summary>
    /// Get a string value from PlayerPrefs.
    /// </summary>
    /// <param name="prefKey">The key to retrieve the value for.</param>
    /// <param name="defaultValue">
    /// The default value to return if the key doesn't exist. If not specified it will be the built-in default.
    /// </param>
    /// <returns>The string value stored at the key prefKey or if not present then the built-in default.</returns>
    public string Get(string prefKey, string defaultValue = default(string))
    {
        // TODO: Handle default value
        return FileEditing.ReadKey(prefKey, Settings.Instance.ConfigFile);
    }

    /// <summary>
    /// Set a string value into PlayerPrefs.
    /// </summary>
    /// <param name="prefKey">The key to set a value for.</param>
    /// <param name="prefValue">The value to set.</param>
    /// <returns>This accessor.</returns>
    public PrefAccessor<string> Set(string prefKey, string prefValue)
    {
        FileEditing.EditKey(prefKey, prefValue, Settings.Instance.ConfigFile);
        return this;
    }
}
