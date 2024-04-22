using UnityEngine;

/// <summary>
/// Utility class to simplify access to data saved in PlayerPrefs.
/// </summary>
public static class PlayerPrefsUtils
{
    /// <summary>
    /// Gets a float value saved in PlayerPrefs with the specified key.
    /// </summary>
    /// <param name="key">The key associated with the value saved in PlayerPrefs.</param>
    /// <returns>The saved float value, or 0 if the key does not exist.</returns>
    public static float GetSavedFloat(string key)
    {
        if (PlayerPrefs.HasKey(key)) return PlayerPrefs.GetFloat(key);
        return 0f;
    }

    /// <summary>
    /// Gets an int value saved in PlayerPrefs with the specified key.
    /// </summary>
    /// <param name="key">The key associated with the value saved in PlayerPrefs.</param>
    /// <returns>The saved int value, or 0 if the key does not exist.</returns>
    public static int GetSavedInt(string key)
    {
        if (PlayerPrefs.HasKey(key)) return PlayerPrefs.GetInt(key);
        return 0;
    }
}
