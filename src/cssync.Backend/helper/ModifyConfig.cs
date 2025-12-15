// Copyright (c) Ame (Akeoott) <ame@akeoot.org>. Licensed under the GPLv3 License.
// See the LICENSE file in the repository root for full license text.

namespace cssync.Backend.helper;

/// <summary>
/// Provides methods to modify the cssync configuration file
/// </summary>
public class ModifyConfig
{
    /// <summary>
    /// Adds a new key-value pair to the configuration
    /// </summary>
    public static async Task AddValue(string section, string key, object value)
    {
        Log.BackendInfo("Adding new key '{key}' to {section} section", key, section);
        var config = await Json.Deserialize();

        try
        {
            if (config.Variables.ContainsKey(key) || config.Timer.ContainsKey(key))
            {
                Log.BackendWarn("Key '{key}' already exists. Use AppendValue to modify or EditValue to replace", key);
                return;
            }

            if (section == "Variables")
            {
                if (value is List<string> stringList)
                {
                    config.Variables.Add(key, stringList);
                    Log.BackendInfo("Added key '{key}' with {count} string value(s) to Variables", key, stringList.Count);
                }
                else
                {
                    Log.BackendError("Invalid value type for Variables section. Expected List<string>");
                    return;
                }
            }
            else if (section == "Timer")
            {
                if (value is List<int> intList)
                {
                    config.Timer.Add(key, intList);
                    Log.BackendInfo("Added key '{key}' with {count} integer value(s) to Timer", key, intList.Count);
                }
                else
                {
                    Log.BackendError("Invalid value type for Timer section. Expected List<int>");
                    return;
                }
            }
            else
            {
                Log.BackendError("Invalid section '{section}'. Must be 'Variables' or 'Timer'", section);
                return;
            }

            await Json.WriteConfig(config);
            Log.BackendInfo("Configuration saved successfully");
        }
        catch (ArgumentException ex)
        {
            Log.BackendWarn("Key '{key}' already exists in {section} section: {message}", key, section, ex.Message);
        }
        catch (Exception ex)
        {
            Log.BackendError(ex, "Failed to add value to configuration");
        }
    }

    /// <summary>
    /// Appends values to an existing list in the configuration
    /// </summary>
    public static async Task AppendValue(string section, string key, object value)
    {
        Log.BackendInfo("Appending value(s) to key '{key}' in {section} section", key, section);
        var config = await Json.Deserialize();

        try
        {
            if (section == "Variables")
            {
                if (!config.Variables.TryGetValue(key, out var list))
                {
                    Log.BackendError("Key '{key}' not found in Variables section", key);
                    return;
                }

                if (value is string s)
                {
                    list.Add(s);
                    await Json.WriteConfig(config);
                    Log.BackendInfo("Appended string value to key '{key}', list now contains {count} items", key, list.Count);
                }
                else if (value is List<string> strings)
                {
                    list.AddRange(strings);
                    await Json.WriteConfig(config);
                    Log.BackendInfo("Appended {count} string value(s) to key '{key}', list now contains {total} items", strings.Count, key, list.Count);
                }
                else
                {
                    Log.BackendError("Invalid value type for Variables section. Expected string or List<string>");
                }
            }
            else if (section == "Timer")
            {
                if (!config.Timer.TryGetValue(key, out var list))
                {
                    Log.BackendError("Key '{key}' not found in Timer section", key);
                    return;
                }

                if (value is int i)
                {
                    list.Add(i);
                    await Json.WriteConfig(config);
                    Log.BackendInfo("Appended integer value to key '{key}', list now contains {count} items", key, list.Count);
                }
                else if (value is List<int> ints)
                {
                    list.AddRange(ints);
                    await Json.WriteConfig(config);
                    Log.BackendInfo("Appended {count} integer value(s) to key '{key}', list now contains {total} items", ints.Count, key, list.Count);
                }
                else
                {
                    Log.BackendError("Invalid value type for Timer section. Expected int or List<int>");
                }
            }
            else
            {
                Log.BackendError("Invalid section '{section}'. Must be 'Variables' or 'Timer'", section);
            }
        }
        catch (Exception ex)
        {
            Log.BackendError(ex, "Failed to append value to configuration");
        }
    }

    /// <summary>
    /// Edits an existing value in the configuration
    /// </summary>
    public static async Task EditValue(string section, string key, object newValue)
    {
        Log.BackendInfo("Editing key '{key}' in {section} section", key, section);
        var config = await Json.Deserialize();

        try
        {
            if (section == "Variables" && config.Variables.TryGetValue(key, out List<string>? valueVar))
            {
                if (newValue is not List<string> strings)
                {
                    Log.BackendError("Invalid value type for Variables section. Expected List<string>");
                    return;
                }

                int oldCount = valueVar.Count;
                config.Variables[key] = strings;
                await Json.WriteConfig(config);

                Log.BackendInfo("Edited key '{key}' in Variables: {oldCount} => {newCount} items", key, oldCount, strings.Count);
            }
            else if (section == "Timer" && config.Timer.TryGetValue(key, out List<int>? valueTimer))
            {
                if (newValue is not List<int> ints)
                {
                    Log.BackendError("Invalid value type for Timer section. Expected List<int>");
                    return;
                }

                int oldCount = valueTimer.Count;
                config.Timer[key] = ints;
                await Json.WriteConfig(config);

                Log.BackendInfo("Edited key '{key}' in Timer: {oldCount} => {newCount} items", key, oldCount, ints.Count);
            }
            else
            {
                Log.BackendError("Key '{key}' not found in {section} section", key, section);
            }
        }
        catch (Exception ex)
        {
            Log.BackendError(ex, "Failed to edit value in configuration");
        }
    }

    /// <summary>
    /// Removes specific value(s) from a list in the configuration
    /// </summary>
    public static async Task RemoveValue(string section, string key, object valueToRemove)
    {
        Log.BackendInfo("Removing value(s) from key '{key}' in {section} section", key, section);
        var config = await Json.Deserialize();

        try
        {
            if (section == "Variables")
            {
                if (!config.Variables.TryGetValue(key, out var list))
                {
                    Log.BackendError("Key '{key}' not found in Variables section", key);
                    return;
                }

                int removed = valueToRemove switch
                {
                    string s => list.Remove(s) ? 1 : 0,
                    List<string> strings => strings.Count(v => list.Remove(v)),
                    _ => -1
                };

                if (removed < 0)
                {
                    Log.BackendError("Invalid value type for Variables section. Expected string or List<string>");
                    return;
                }

                if (removed > 0)
                {
                    await Json.WriteConfig(config);
                    Log.BackendInfo("Removed {removed} string value(s) from key '{key}'", removed, key);
                }
                else
                {
                    Log.BackendWarn("No string values found to remove from key '{key}'", key);
                }
            }
            else if (section == "Timer")
            {
                if (!config.Timer.TryGetValue(key, out var list))
                {
                    Log.BackendError("Key '{key}' not found in Timer section", key);
                    return;
                }

                int removed = valueToRemove switch
                {
                    int i => list.Remove(i) ? 1 : 0,
                    List<int> ints => ints.Count(v => list.Remove(v)),
                    _ => -1
                };

                if (removed < 0)
                {
                    Log.BackendError("Invalid value type for Timer section. Expected int or List<int>");
                    return;
                }

                if (removed > 0)
                {
                    await Json.WriteConfig(config);
                    Log.BackendInfo("Removed {removed} integer value(s) from key '{key}'", removed, key);
                }
                else
                {
                    Log.BackendWarn("No integer values found to remove from key '{key}'", key);
                }
            }
            else
            {
                Log.BackendError("Invalid section '{section}'. Must be 'Variables' or 'Timer'", section);
            }
        }
        catch (Exception ex)
        {
            Log.BackendError(ex, "Failed to remove value from configuration");
        }
    }

    /// <summary>
    /// Removes an entire key-value pair from the configuration
    /// </summary>
    public static async Task RemoveKey(string section, string key)
    {
        Log.BackendInfo("Removing key '{key}' from {section} section", key, section);
        var config = await Json.Deserialize();

        try
        {
            bool removed = section switch
            {
                "Variables" => config.Variables.Remove(key),
                "Timer" => config.Timer.Remove(key),
                _ => false
            };

            if (removed)
            {
                await Json.WriteConfig(config);
                Log.BackendInfo("Successfully removed key '{key}' from {section} section", key, section);
            }
            else
            {
                Log.BackendError("Key '{key}' not found in {section} section", key, section);
            }
        }
        catch (Exception ex)
        {
            Log.BackendError(ex, "Failed to remove key from configuration");
        }
    }
}
