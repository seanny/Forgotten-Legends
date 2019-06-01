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
/// Read and write values of various different types to PlayerPrefs easily.
/// </summary>
public class GamePrefs
{
    private static GamePrefsAccessor<bool> boolAccessor;
    private static GamePrefsAccessor<float> floatAccessor;
    private static GamePrefsAccessor<int> intAccessor;
    private static GamePrefsAccessor<string> stringAccessor;
    private static GamePrefsAccessor<char> charAccessor;
    private static GamePrefsAccessor<double> doubleAccessor;
    private static GamePrefsAccessor<long> longAccessor;
    private static GamePrefsAccessor<DateTime> dateTimeAccessor;

    /// <summary>
    /// Create a PlayerPrefs accessor to read and write bool type values.
    /// </summary>
    /// <returns>Bool prefs accessor</returns>
    public static GamePrefsAccessor<bool> ForBool()
    {
        if (boolAccessor == null)
        {
            boolAccessor = new GamePrefsAccessor<bool>(new BoolPrefAccessor());
        }
        return boolAccessor;
    }

    /// <summary>
    /// Create a PlayerPrefs accessor to read and write float type values.
    /// </summary>
    /// <returns>Float prefs accessor</returns>
    public static GamePrefsAccessor<float> ForFloat()
    {
        if (floatAccessor == null)
        {
            floatAccessor = new GamePrefsAccessor<float>(new FloatPrefAccessor());
        }
        return floatAccessor;
    }

    /// <summary>
    /// Create a PlayerPrefs accessor to read and write int type values.
    /// </summary>
    /// <returns>Int prefs accessor</returns>
    public static GamePrefsAccessor<int> ForInt()
    {
        if (intAccessor == null)
        {
            intAccessor = new GamePrefsAccessor<int>(new IntPrefAccessor());
        }
        return intAccessor;
    }

    /// <summary>
    /// Create a PlayerPrefs accessor to read and write string type values.
    /// </summary>
    /// <returns>String prefs accessor</returns>
    public static GamePrefsAccessor<string> ForString()
    {
        if (stringAccessor == null)
        {
            stringAccessor = new GamePrefsAccessor<string>(new StringPrefAccessor());
        }
        return stringAccessor;
    }

    /// <summary>
    /// Create a PlayerPrefs accessor to read and write char type values.
    /// </summary>
    /// <returns>Char prefs accessor</returns>
    public static GamePrefsAccessor<char> ForChar()
    {
        if (charAccessor == null)
        {
            charAccessor = new GamePrefsAccessor<char>(new CharPrefAccessor());
        }
        return charAccessor;
    }

    /// <summary>
    /// Create a PlayerPrefs accessor to read and write double type values.
    /// </summary>
    /// <returns>Double prefs accessor</returns>
    public static GamePrefsAccessor<double> ForDouble()
    {
        if (doubleAccessor == null)
        {
            doubleAccessor = new GamePrefsAccessor<double>(new DoublePrefAccessor());
        }
        return doubleAccessor;
    }

    /// <summary>
    /// Create a PlayerPrefs accessor to read and write long type values.
    /// </summary>
    /// <returns>Long prefs accessor</returns>
    public static GamePrefsAccessor<long> ForLong()
    {
        if (longAccessor == null)
        {
            longAccessor = new GamePrefsAccessor<long>(new LongPrefAccessor());
        }
        return longAccessor;
    }

    /// <summary>
    /// Create a PlayerPrefs accessor to read and write DateTime type values.
    /// </summary>
    /// <returns>DateTime prefs accessor</returns>
    public static GamePrefsAccessor<DateTime> ForDateTime()
    {
        if (dateTimeAccessor == null)
        {
            dateTimeAccessor = new GamePrefsAccessor<DateTime>(new DateTimePrefAccessor());
        }
        return dateTimeAccessor;
    }
}