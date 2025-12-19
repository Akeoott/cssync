// Copyright (c) Ame (Akeoott) <ame@akeoot.org>. Licensed under the GPLv3 License.
// See the LICENSE file in the repository root for full license text.

namespace cssync.Backend.helper;

/// <summary>
/// Provides methods to modify the cssync configuration file
/// </summary>
public class ModifyConfig
{
    /// <summary>
    /// Edits a single value inside a configuration section at a specific index.
    /// </summary>
    /// <param name="section">
    /// The configuration section to edit (e.g. "Variables" or "Timer").
    /// </param>
    /// <param name="key">
    /// The key within the section containing the value list.
    /// </param>
    /// <param name="location">
    /// Zero-based index of the value to edit within the key.
    /// </param>
    /// <param name="value">
    /// The new value to assign. Must be a string for Variables or an int for Timer.
    /// </param>
    public static async Task EditValue(string section, string key, int location, object value)
    {
        Log.Info("Editing value in config at section: {section}, key: {key}, index: {location}", section, key, location);
        Log.Debug("value: {value}", value);
        var config = await Json.Deserialize();

        try
        {
            if (value is null)
            {
                Log.Error("Entered value is null");
                return;
            }
            else if (section == "Variables")
            {
                if (!config.Variables.TryGetValue(key, out List<string>? values))
                {
                    Log.Error("Selected key does not exist");
                    return;
                }
                else if (location < 0 || location >= values.Count)
                {
                    Log.Error("Selected value does not exist at location: {location}", location);
                    return;
                }
                else if (value is not string newString)
                {
                    Log.Error("New value is an incorrect data type: {value}", value.GetType());
                    return;
                }
                else if (values[location] == newString)
                {
                    Log.Error("New value is the same as existing one");
                    return;
                }
                else
                {
                    values[location] = newString;
                    await Json.WriteConfig(config);
                    Log.Info("Successfully edited value");
                    return;
                }
            }
            else if (section == "Timer")
            {
                if (!config.Timer.TryGetValue(key, out List<int>? values))
                {
                    Log.Error("Selected key does not exist");
                    return;
                }
                else if (location < 0 || location >= values.Count)
                {
                    Log.Error("Selected value does not exist at location: {location}", location);
                    return;
                }
                else if (value is not int newInt)
                {
                    Log.Error("New value is an incorrect data type: {value}", value.GetType());
                    return;
                }
                else if (values[location] == newInt)
                {
                    Log.Error("New value is the same as existing one");
                    return;
                }
                else
                {
                    values[location] = newInt;
                    await Json.WriteConfig(config);
                    Log.Info("Successfully edited value");
                    return;
                }
            }
            Log.Error("Section not found");
        }
        catch (Exception ex)
        {
            Log.Error("Something went wrong. {ex}: {ex.Message}", ex, ex.Message);
        }
    }

    /// <summary>
    /// Appends one or more values to an existing list in the configuration.
    /// </summary>
    /// <param name="section">
    /// The configuration section to modify (e.g. "Variables" or "Timer").
    /// </param>
    /// <param name="key">
    /// The key within the section containing the value list.
    /// </param>
    /// <param name="value">
    /// The value(s) to append. Can be a single value or a list of values.
    /// Must be a string (or List&lt;string&gt;) for Variables or an int (or List&lt;int&gt;) for Timer.
    /// </param>
    public static async Task AppendValue(string section, string key, object value)
    {
        Log.Info("Appending value in config at: {section}, {key}", section, key);
        Log.Debug("value: {value}", value);
        var config = await Json.Deserialize();

        try
        {
            if (value is null)
            {
                Log.Error("Entered value is null");
                return;
            }
            else if (section == "Variables")
            {
                if (!config.Variables.TryGetValue(key, out List<string>? values))
                {
                    Log.Error("Selected key does not exist");
                    return;
                }
                else if (value is string newString)
                {
                    values.Add(newString);
                    await Json.WriteConfig(config);
                    Log.Info("Successfully appended value");
                    return;
                }
                else if (value is List<string> newStringList)
                {
                    values.AddRange(newStringList);
                    await Json.WriteConfig(config);
                    Log.Info("Successfully appended value");
                    return;
                }
                else
                {
                    Log.Error("New value is an incorrect data type: {value}", value.GetType());
                    return;
                }
            }
            else if (section == "Timer")
            {

                if (!config.Timer.TryGetValue(key, out List<int>? values))
                {
                    Log.Error("Selected key does not exist");
                    return;
                }
                else if (value is int newInt)
                {
                    values.Add(newInt);
                    await Json.WriteConfig(config);
                    Log.Info("Successfully appended value");
                    return;
                }
                else if (value is List<int> newIntList)
                {
                    values.AddRange(newIntList);
                    await Json.WriteConfig(config);
                    Log.Info("Successfully appended value");
                    return;
                }
                else
                {
                    Log.Error("New value is an incorrect data type: {value}", value.GetType());
                    return;
                }
            }
            Log.Error("Section not found");
        }
        catch (Exception ex)
        {
            Log.Error("Something went wrong. {ex}: {ex.Message}", ex, ex.Message);
        }
    }

    /// <summary>
    /// Removes a specific value from a list in the configuration at a given index.
    /// </summary>
    /// <param name="section">
    /// The configuration section to modify (e.g. "Variables" or "Timer").
    /// </param>
    /// <param name="key">
    /// The key within the section containing the value list.
    /// </param>
    /// <param name="location">
    /// Zero-based index of the value to remove within the key.
    /// </param>
    public static async Task RemoveValue(string section, string key, int location)
    {
        Log.Info("Removing value in config at section: {section}, key: {key}, index: {location}", section, key, location);
        var config = await Json.Deserialize();

        try
        {
            if (section == "Variables")
            {
                if (!config.Variables.TryGetValue(key, out List<string>? values))
                {
                    Log.Error("Selected key does not exist");
                    return;
                }
                else if (location < 0 || location >= values.Count)
                {
                    Log.Error("Selected value does not exist at location: {location}", location);
                    return;
                }
                else
                {
                    values.RemoveAt(location);
                    await Json.WriteConfig(config);
                    Log.Info("Successfully removed value");
                    return;
                }
            }
            else if (section == "Timer")
            {
                if (!config.Timer.TryGetValue(key, out List<int>? values))
                {
                    Log.Error("Selected key does not exist");
                    return;
                }
                else if (location < 0 || location >= values.Count)
                {
                    Log.Error("Selected value does not exist at location: {location}", location);
                    return;
                }
                else
                {
                    values.RemoveAt(location);
                    await Json.WriteConfig(config);
                    Log.Info("Successfully removed value");
                    return;
                }
            }
            Log.Error("Section not found");
        }
        catch (Exception ex)
        {
            Log.Error("Something went wrong. {ex}: {ex.Message}", ex, ex.Message);
        }
    }

    /// <summary>
    /// Appends a new key to the configuration, initializing it with an empty list.
    /// </summary>
    /// <param name="section">
    /// The configuration section where the key should be added (e.g. "Variables" or "Timer").
    /// </param>
    /// <param name="key">
    /// The new key to add to the section.
    /// </param>
    public static async Task AppendKey(string section, string key)
    {
        Log.Info("Appending key in config at section: {section}, key: {key}", section, key);
        var config = await Json.Deserialize();

        try
        {
            if (section == "Variables")
            {
                if (config.Variables.ContainsKey(key))
                {
                    Log.Error("Selected key already exists");
                    return;
                }
                else
                {
                    config.Variables[key] = [];
                    await Json.WriteConfig(config);
                    Log.Info("Successfully appended key");
                    return;
                }
            }
            else if (section == "Timer")
            {
                if (config.Timer.ContainsKey(key))
                {
                    Log.Error("Selected key already exists");
                    return;
                }
                else
                {
                    config.Timer[key] = [];
                    await Json.WriteConfig(config);
                    Log.Info("Successfully appended key");
                    return;
                }
            }
            Log.Error("Section not found");
        }
        catch (Exception ex)
        {
            Log.Error("Something went wrong. {ex}: {ex.Message}", ex, ex.Message);
        }
    }

    /// <summary>
    /// Removes an entire key-value pair from the configuration.
    /// </summary>
    /// <param name="section">
    /// The configuration section containing the key (e.g. "Variables" or "Timer").
    /// </param>
    /// <param name="key">
    /// The key to remove from the section.
    /// </param>
    public static async Task RemoveKey(string section, string key)
    {
        Log.Info("Removing key in config at section: {section}, key: {key}", section, key);
        var config = await Json.Deserialize();

        try
        {
            if (section == "Variables")
            {
                if (!config.Variables.Remove(key))
                {
                    Log.Error("Selected key does not exist");
                    return;
                }
                else
                {
                    await Json.WriteConfig(config);
                    Log.Info("Successfully removed key");
                    return;
                }
            }
            else if (section == "Timer")
            {
                if (!config.Timer.Remove(key))
                {
                    Log.Error("Selected key does not exist");
                    return;
                }
                else
                {
                    await Json.WriteConfig(config);
                    Log.Info("Successfully removed key");
                    return;
                }
            }
            Log.Error("Section not found");
        }
        catch (Exception ex)
        {
            Log.Error("Something went wrong. {ex}: {ex.Message}", ex, ex.Message);
        }
    }
}
