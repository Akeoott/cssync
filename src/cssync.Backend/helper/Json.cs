// Copyright (c) Ame (Akeoott) <ame@akeoot.org>. Licensed under the GPLv3 License.
// See the LICENSE file in the repository root for full license text.

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace cssync.Backend.helper;

internal class Config
{
    public bool Run { get; set; } = false;
    public JObject Sections { get; set; } = new JObject();
}

internal static class Json
{
    private static readonly string configPath = AppDomain.CurrentDomain.BaseDirectory + "config.json";
    private static DateTime _lastConfigWriteTime = DateTime.MinValue;

    internal static async Task<Config> Deserialize()
    {
        int attempts = 0;

        while (attempts < 3)
        {
            if (!File.Exists(configPath))
            {
                await GenConfig();
            }

            try
            {
                Log.Debug("Deserializing config");
                string json = await File.ReadAllTextAsync(configPath);
                return JsonConvert.DeserializeObject<Config>(json) ?? new Config();
            }
            catch (Exception ex) when (ex is FileNotFoundException || ex is JsonException)
            {
                attempts++;
                Log.Critical("Loading config failed (attempt {attempts}/3). {ex}: {ex.Message}", attempts, ex, ex.Message);
                await Task.Delay(100);
            }
        }

        throw new InvalidOperationException($"Failed to load config after {attempts} attempts");
    }

    internal static async Task WriteConfig(Config config)
    {
        try
        {
            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            await File.WriteAllTextAsync(configPath, json);
            Log.Debug("Wrote config to disk");
        }
        catch (Exception ex)
        {
            Log.Critical("Failed to write config. {ex}: {ex.Message}", ex, ex.Message);
        }
    }

    internal static async Task GenConfig()
    {
        if (File.Exists(configPath))
        {
            Log.Info("Config exists");
            return;
        }

        Log.Warn("Config doesn't exist. Generating config");
        var config = new Config
        {
            Sections = new JObject
            {
                ["Variables"] = new JObject(),
                ["Timers"] = new JObject()
            }
        };
        await WriteConfig(config);
        Log.Info("Successfully generated config");
    }

    internal static async Task<bool> GetConfigStatus()
    {
        DateTime currentWriteTime = File.GetLastWriteTimeUtc(configPath);
        if (currentWriteTime == _lastConfigWriteTime) return false;
        _lastConfigWriteTime = currentWriteTime;

        var config = await Deserialize();
        return config.Run;
    }
}
