// Copyright (c) Ame (Akeoott) <ame@akeoot.org>. Licensed under the GPLv3 License.
// See the LICENSE file in the repository root for full license text.

namespace cssync.Backend.helper;

public class ModifyConfig
{
    /// <summary>
    /// Helper to validate section and key, returning the list and expected type
    /// </summary>
    private static async Task<(bool success, Type expectedType, object? list)> GetSectionList(Config config, string section, string key)
    {
        switch (section)
        {
            case "Variables":
                if (!config.Variables.TryGetValue(key, out List<string>? varList))
                {
                    Log.Error("Selected key does not exist in Variables");
                    return (false, typeof(string), null);
                }
                return (true, typeof(string), varList);

            case "Timers":
                if (!config.Timers.TryGetValue(key, out List<int>? timerList))
                {
                    Log.Error("Selected key does not exist in Timers");
                    return (false, typeof(int), null);
                }
                return (true, typeof(int), timerList);

            default:
                Log.Error("Section '{section}' not found", section);
                return (false, null!, null);
        }
    }

    public static async Task EditValue(string section, string key, int location, object value)
    {
        var config = await Json.Deserialize();
        Log.Info("Editing value in config at section: {section}, key: {key}, index: {location}", section, key, location);
        Log.Debug("value: {value}", value);

        try
        {
            if (value is null)
            {
                Log.Error("Entered value is null");
                return;
            }

            var (success, expectedType, listObj) = await GetSectionList(config, section, key);
            if (!success || listObj is null) return;

            if (expectedType == typeof(string))
            {
                if (value is not string newValueStr)
                {
                    Log.Error("New value is an incorrect data type: {value}", value.GetType());
                    return;
                }
                var list = (List<string>)listObj;
                if (location < 0 || location >= list.Count)
                {
                    Log.Error("Selected value does not exist at location: {location}", location);
                    return;
                }
                if (list[location] == newValueStr)
                {
                    Log.Error("New value is the same as existing one");
                    return;
                }
                list[location] = newValueStr;
            }
            else if (expectedType == typeof(int))
            {
                if (value is not int newValueInt)
                {
                    Log.Error("New value is an incorrect data type: {value}", value.GetType());
                    return;
                }
                var list = (List<int>)listObj;
                if (location < 0 || location >= list.Count)
                {
                    Log.Error("Selected value does not exist at location: {location}", location);
                    return;
                }
                if (list[location] == newValueInt)
                {
                    Log.Error("New value is the same as existing one");
                    return;
                }
                list[location] = newValueInt;
            }

            await Json.WriteConfig(config);
            Log.Info("Successfully edited value");
        }
        catch (Exception ex)
        {
            Log.Error("Something went wrong. {ex}: {ex.Message}", ex, ex.Message);
        }
    }

    public static async Task AppendValue(string section, string key, object value)
    {
        var config = await Json.Deserialize();
        Log.Info("Appending value in config at: {section}, {key}", section, key);
        Log.Debug("value: {value}", value);

        try
        {
            if (value is null)
            {
                Log.Error("Entered value is null");
                return;
            }

            var (success, expectedType, listObj) = await GetSectionList(config, section, key);
            if (!success || listObj is null) return;

            if (expectedType == typeof(string))
            {
                var list = (List<string>)listObj;
                if (value is string strVal) list.Add(strVal);
                else if (value is List<string> strList) list.AddRange(strList);
                else
                {
                    Log.Error("New value is an incorrect data type: {value}", value.GetType());
                    return;
                }
            }
            else if (expectedType == typeof(int))
            {
                var list = (List<int>)listObj;
                if (value is int intVal) list.Add(intVal);
                else if (value is List<int> intList) list.AddRange(intList);
                else
                {
                    Log.Error("New value is an incorrect data type: {value}", value.GetType());
                    return;
                }
            }

            await Json.WriteConfig(config);
            Log.Info("Successfully appended value");
        }
        catch (Exception ex)
        {
            Log.Error("Something went wrong. {ex}: {ex.Message}", ex, ex.Message);
        }
    }

    public static async Task RemoveValue(string section, string key, int location)
    {
        var config = await Json.Deserialize();
        Log.Info("Removing value in config at section: {section}, key: {key}, index: {location}", section, key, location);

        try
        {
            var (success, expectedType, listObj) = await GetSectionList(config, section, key);
            if (!success || listObj is null) return;

            if (expectedType == typeof(string))
            {
                var list = (List<string>)listObj;
                if (location < 0 || location >= list.Count)
                {
                    Log.Error("Selected value does not exist at location: {location}", location);
                    return;
                }
                list.RemoveAt(location);
            }
            else if (expectedType == typeof(int))
            {
                var list = (List<int>)listObj;
                if (location < 0 || location >= list.Count)
                {
                    Log.Error("Selected value does not exist at location: {location}", location);
                    return;
                }
                list.RemoveAt(location);
            }

            await Json.WriteConfig(config);
            Log.Info("Successfully removed value");
        }
        catch (Exception ex)
        {
            Log.Error("Something went wrong. {ex}: {ex.Message}", ex, ex.Message);
        }
    }

    public static async Task AppendKey(string section, string key)
    {
        var config = await Json.Deserialize();
        Log.Info("Appending key in config at section: {section}, key: {key}", section, key);

        try
        {
            switch (section)
            {
                case "Variables":
                    if (config.Variables.ContainsKey(key))
                    {
                        Log.Error("Selected key already exists");
                        return;
                    }
                    config.Variables[key] = new List<string>();
                    break;

                case "Timer":
                    if (config.Timers.ContainsKey(key))
                    {
                        Log.Error("Selected key already exists");
                        return;
                    }
                    config.Timers[key] = new List<int>();
                    break;

                default:
                    Log.Error("Section not found");
                    return;
            }

            await Json.WriteConfig(config);
            Log.Info("Successfully appended key");
        }
        catch (Exception ex)
        {
            Log.Error("Something went wrong. {ex}: {ex.Message}", ex, ex.Message);
        }
    }

    public static async Task RemoveKey(string section, string key)
    {
        var config = await Json.Deserialize();
        Log.Info("Removing key in config at section: {section}, key: {key}", section, key);

        try
        {
            bool removed = section switch
            {
                "Variables" => config.Variables.Remove(key),
                "Timer" => config.Timers.Remove(key),
                _ => throw new InvalidOperationException($"Section '{section}' not found")
            };

            if (!removed)
            {
                Log.Error("Selected key does not exist");
                return;
            }

            await Json.WriteConfig(config);
            Log.Info("Successfully removed key");
        }
        catch (Exception ex)
        {
            Log.Error("Something went wrong. {ex}: {ex.Message}", ex, ex.Message);
        }
    }

    /// <summary>
    /// Enable or disable cssync from performing rclone operations automatically
    /// </summary>
    /// <param name="shouldEnable">true to enable cssync and false to disable cssync</param>
    /// <returns></returns>
    public static async Task EnableDisableCssync(bool shouldEnable)
    {
        var config = await Json.Deserialize();

        try
        {
            if (shouldEnable)
            {
                Log.Debug("Enabling cssync");
                config.Run = true;
                await Json.WriteConfig(config);
                Log.Info("Successfully enabled cssync to perform rclone operations");
            }
            else
            {
                Log.Debug("Disabling cssync");
                config.Run = false;
                await Json.WriteConfig(config);
                Log.Info("Successfully disabled cssync from performing rclone operations");
            }
        }
        catch (Exception ex)
        {
            Log.Error("Something went wrong. {ex}: {ex.Message}", ex, ex.Message);
        }
    }

    public static async Task<bool> GetStatus()
    {
        var config = await Json.Deserialize();
        Log.Debug("Getting cssync status");

        try
        {
            return config.Run;
        }
        catch (Exception ex)
        {
            Log.Error("Something went wrong. {ex}: {ex.Message}", ex, ex.Message);
            return false;
        }
    }
}
