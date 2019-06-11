//
//  Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
//  This document is the property of Outlaw Games Studio.
//  It is considered confidential and proprietary.
//
//  This document may not be reproduced or transmitted in any form
//  without the consent of Outlaw Games Studio.
//

/// <summary>
/// Get and set values of type ValueT to and from PlayerPrefs, with the key being stored.
/// </summary>
public class GamePrefsKeyAccessor<ValueT>
{
    private GamePrefsAccessor<ValueT> accessor;
    private string prefKey;

    /// <summary>
    /// Create a GamePrefsKeyAccessor which can get and set values of type ValueT to and from PlayerPrefs, with the 
    /// key being stored.
    /// </summary>
    /// <param name="accessor">Accessor for type ValueT.</param>
    /// <param name="prefKey">The key to get and set values for.</param>
    public GamePrefsKeyAccessor(GamePrefsAccessor<ValueT> accessor, string prefKey) 
    {
        this.accessor = accessor;
        this.prefKey = prefKey;
    }

    /// <summary>
    /// Get the value from PlayerPrefs.
    /// </summary>
    /// <param name="defaultValue">
    /// The default value to return if the key doesn't exist. If not specified it will equal the default value
    /// for the generic type ValueT.
    /// </param>
    /// <returns>The value stored for the key, or if not present then defaultValue.</returns>
    public ValueT Get(ValueT defaultValue = default(ValueT)) 
    {
        return accessor.Get(prefKey, defaultValue);
    }

    /// <summary>
    /// Set a value into PlayerPrefs.
    /// </summary>
    /// <param name="prefValue">The value to set.</param>
    public void Set(ValueT prefValue) 
    {
        accessor.Set(prefKey, prefValue);
    }
}