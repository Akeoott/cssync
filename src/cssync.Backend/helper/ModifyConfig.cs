// Copyright (c) Ame (Akeoott) <ame@akeoot.org>. Licensed under the GPLv3 License.
// See the LICENSE file in the repository root for full license text.

using Newtonsoft.Json.Linq;

namespace cssync.Backend.helper;

public static class ModifyConfig
{
    private static async Task<(bool success, JToken? section)> GetSection(Config config, string section)
    {
        if (!config.Sections.TryGetValue(section, out JToken? jSection))
        {
            Log.Error("Section '{section}' not found", section);
            return (false, null);
        }
        return (true, jSection);
    }

    public static async Task EditValue(string section, string key, int index, object value)
    {
        var config = await Json.Deserialize();
        var (success, jSection) = await GetSection(config, section);
        if (!success || jSection is null) return;

        if (value is null)
        {
            Log.Error("Entered value is null");
            return;
        }

        if (jSection[key] is JArray arr)
        {
            if (index < 0 || index >= arr.Count)
            {
                Log.Error("Index out of range: {index}", index);
                return;
            }
            arr[index] = JToken.FromObject(value);
        }
        else
        {
            Log.Error("Key '{key}' does not exist or is not an array", key);
            return;
        }

        await Json.WriteConfig(config);
        Log.Info("Edited value successfully");
    }

    public static async Task AppendValue(string section, string key, object value)
    {
        var config = await Json.Deserialize();
        var (success, jSection) = await GetSection(config, section);
        if (!success || jSection is null) return;

        if (value is null)
        {
            Log.Error("Entered value is null");
            return;
        }

        if (jSection[key] == null)
        {
            jSection[key] = new JArray();
        }

        if (jSection[key] is JArray arr)
        {
            switch (value)
            {
                case IEnumerable<string> strList:
                    foreach (var s in strList) arr.Add(s);
                    break;
                case IEnumerable<int> intList:
                    foreach (var i in intList) arr.Add(i);
                    break;
                default:
                    arr.Add(JToken.FromObject(value));
                    break;
            }
        }
        else
        {
            Log.Error("Key '{key}' is not an array", key);
            return;
        }

        await Json.WriteConfig(config);
        Log.Info("Appended value successfully");
    }

    public static async Task RemoveValue(string section, string key, int index)
    {
        var config = await Json.Deserialize();
        var (success, jSection) = await GetSection(config, section);
        if (!success || jSection is null) return;

        if (jSection[key] is JArray arr)
        {
            if (index < 0 || index >= arr.Count)
            {
                Log.Error("Index out of range: {index}", index);
                return;
            }
            arr.RemoveAt(index);
        }
        else
        {
            Log.Error("Key '{key}' does not exist or is not an array", key);
            return;
        }

        await Json.WriteConfig(config);
        Log.Info("Removed value successfully");
    }

    public static async Task AppendKey(string section, string key)
    {
        var config = await Json.Deserialize();
        var (success, jSection) = await GetSection(config, section);
        if (!success || jSection is null) return;

        if (jSection[key] != null)
        {
            Log.Error("Key '{key}' already exists", key);
            return;
        }

        jSection[key] = new JArray();
        await Json.WriteConfig(config);
        Log.Info("Appended key successfully");
    }

    public static async Task RemoveKey(string section, string key)
    {
        var config = await Json.Deserialize();
        var (success, jSection) = await GetSection(config, section);
        if (!success || jSection is null) return;

        if (!((JObject)jSection).Remove(key))
        {
            Log.Error("Key '{key}' does not exist", key);
            return;
        }

        await Json.WriteConfig(config);
        Log.Info("Removed key successfully");
    }

    public static async Task EnableDisableCssync(bool enable)
    {
        var config = await Json.Deserialize();
        config.Run = enable;
        await Json.WriteConfig(config);
        Log.Info(enable
            ? "Enabled cssync"
            : "Disabled cssync");
    }
}
