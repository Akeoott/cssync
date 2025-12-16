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
        Log.Info("Adding new key '{key}' to {section} section", key, section);
        var config = await Json.Deserialize();

        try
        {
            if (config.Variables.ContainsKey(key) || config.Timer.ContainsKey(key))
            {
                Log.Info("Key '{key}' already exists. Use AppendValue to modify or EditValue to replace", key);
                return;
            }

            if (section == "Variables")
            {
                if (value is List<string> stringList)
                {
                    config.Variables.Add(key, stringList);
                    Log.Info("Added key '{key}' with {count} string value(s) to Variables", key, stringList.Count);
                }
                else
                {
                    Log.Error("Invalid value type for Variables section. Expected List<string>");
                    return;
                }
            }
            else if (section == "Timer")
            {
                if (value is List<int> intList)
                {
                    config.Timer.Add(key, intList);
                    Log.Info("Added key '{key}' with {count} integer value(s) to Timer", key, intList.Count);
                }
                else
                {
                    Log.Error("Invalid value type for Timer section. Expected List<int>");
                    return;
                }
            }
            else
            {
                Log.Error("Invalid section '{section}'. Must be 'Variables' or 'Timer'", section);
                return;
            }

            await Json.WriteConfig(config);
            Log.Info("Configuration saved successfully");
        }
        catch (ArgumentException ex)
        {
            Log.Warn("Key '{key}' already exists in {section} section: {message}", key, section, ex.Message);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to add value to configuration");
        }
    }

    /// <summary>
    /// Appends values to an existing list in the configuration
    /// </summary>
    public static async Task AppendValue(string section, string key, object value)
    {
        Log.Info("Appending value(s) to key '{key}' in {section} section", key, section);
        var config = await Json.Deserialize();

        try
        {
            if (section == "Variables")
            {
                if (!config.Variables.TryGetValue(key, out var list))
                {
                    Log.Error("Key '{key}' not found in Variables section", key);
                    return;
                }

                if (value is string s)
                {
                    list.Add(s);
                    await Json.WriteConfig(config);
                    Log.Info("Appended string value to key '{key}', list now contains {count} items", key, list.Count);
                }
                else if (value is List<string> strings)
                {
                    list.AddRange(strings);
                    await Json.WriteConfig(config);
                    Log.Info("Appended {count} string value(s) to key '{key}', list now contains {total} items", strings.Count, key, list.Count);
                }
                else
                {
                    Log.Error("Invalid value type for Variables section. Expected string or List<string>");
                }
            }
            else if (section == "Timer")
            {
                if (!config.Timer.TryGetValue(key, out var list))
                {
                    Log.Error("Key '{key}' not found in Timer section", key);
                    return;
                }

                if (value is int i)
                {
                    list.Add(i);
                    await Json.WriteConfig(config);
                    Log.Info("Appended integer value to key '{key}', list now contains {count} items", key, list.Count);
                }
                else if (value is List<int> ints)
                {
                    list.AddRange(ints);
                    await Json.WriteConfig(config);
                    Log.Info("Appended {count} integer value(s) to key '{key}', list now contains {total} items", ints.Count, key, list.Count);
                }
                else
                {
                    Log.Error("Invalid value type for Timer section. Expected int or List<int>");
                }
            }
            else
            {
                Log.Error("Invalid section '{section}'. Must be 'Variables' or 'Timer'", section);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to append value to configuration");
        }
    }

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
    /// <param name="newValue">
    /// The new value to assign. Must be a string for Variables or an int for Timer.
    /// </param>
    public static async Task EditValue(string section, string key, int location, object newValue)
    {
        Log.Info("Editing config at: {section}, {key}", section, key);

        var config = await Json.Deserialize();

        try
        {
            if (section == "Variables")
            {
                if (!config.Variables.TryGetValue(key, out List<string>? values))
                {
                    Log.Error("Value does not exist");
                    return;
                }
                else if (location < 0 || location >= values.Count)
                {
                    Log.Error("Selected value does not exist at location");
                    return;
                }
                else if (newValue is not string newString)
                {
                    Log.Error("New value is an incorrect data type");
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
                    Log.Error("Selected value does not exist at location");
                    return;
                }
                else if (newValue is not int newInt)
                {
                    Log.Error("New value is an incorrect data type");
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
                    return;
                }
            }
            Log.Info("Section not found");
        }
        catch (Exception ex)
        {
            Log.Error("Something went wrong. {ex}: {ex.Message}", ex, ex.Message);
        }
    }

    /// <summary>
    /// Removes specific value(s) from a list in the configuration
    /// </summary>
    public static async Task RemoveValue(string section, string key, object valueToRemove)
    {
        Log.Info("Removing value(s) from key '{key}' in {section} section", key, section);
        var config = await Json.Deserialize();

        try
        {
            if (section == "Variables")
            {
                if (!config.Variables.TryGetValue(key, out var list))
                {
                    Log.Error("Key '{key}' not found in Variables section", key);
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
                    Log.Error("Invalid value type for Variables section. Expected string or List<string>");
                    return;
                }

                if (removed > 0)
                {
                    await Json.WriteConfig(config);
                    Log.Info("Removed {removed} string value(s) from key '{key}'", removed, key);
                }
                else
                {
                    Log.Warn("No string values found to remove from key '{key}'", key);
                }
            }
            else if (section == "Timer")
            {
                if (!config.Timer.TryGetValue(key, out var list))
                {
                    Log.Error("Key '{key}' not found in Timer section", key);
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
                    Log.Error("Invalid value type for Timer section. Expected int or List<int>");
                    return;
                }

                if (removed > 0)
                {
                    await Json.WriteConfig(config);
                    Log.Info("Removed {removed} integer value(s) from key '{key}'", removed, key);
                }
                else
                {
                    Log.Warn("No integer values found to remove from key '{key}'", key);
                }
            }
            else
            {
                Log.Error("Invalid section '{section}'. Must be 'Variables' or 'Timer'", section);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to remove value from configuration");
        }
    }

    /// <summary>
    /// Removes an entire key-value pair from the configuration
    /// </summary>
    public static async Task RemoveKey(string section, string key)
    {
        Log.Info("Removing key '{key}' from {section} section", key, section);
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
                Log.Info("Successfully removed key '{key}' from {section} section", key, section);
            }
            else
            {
                Log.Error("Key '{key}' not found in {section} section", key, section);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to remove key from configuration");
        }
    }
}
