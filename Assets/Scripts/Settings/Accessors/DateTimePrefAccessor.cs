//
//  Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
//  This document is the property of Outlaw Games Studio.
//  It is considered confidential and proprietary.
//
//  This document may not be reproduced or transmitted in any form
//  without the consent of Outlaw Games Studio.
//
using System;

/// <summary>
/// Get and set DateTime values.
/// </summary>
public class DateTimePrefAccessor : PrefAccessor<DateTime>
{
    private readonly LongPrefAccessor longAccessor = new LongPrefAccessor();

    /// <summary>
    /// Get a DateTime value from PlayerPrefs.
    /// </summary>
    /// <param name="prefKey">The key to retrieve the value for.</param>
    /// <param name="defaultValue">
    /// The default value to return if the key doesn't exist. If not specified it will be the built-in default
    /// </param>
    /// <returns>The DateTime value stored at the key prefKey or if not present then the built-in default.</returns>
    public DateTime Get(string prefKey, DateTime defaultValue = default(DateTime))
    {
        var storedValue = longAccessor.Get(prefKey, defaultValue.Ticks);
        return new DateTime(storedValue);
    }

    /// <summary>
    /// Set a DateTime value into PlayerPrefs.
    /// </summary>
    /// <param name="prefKey">The key to set a value for.</param>
    /// <param name="prefValue">The value to set.</param>
    /// <returns>This accessor.</returns>
    public PrefAccessor<DateTime> Set(string prefKey, DateTime prefValue)
    {
        longAccessor.Set(prefKey, prefValue.Ticks);
        return this;
    }
}
